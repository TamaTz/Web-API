using API.View_Models.Other;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utility
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredTime(string token);
        ClaimVM ExtractClaimsFromJWT(string token);
    }
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClaimVM ExtractClaimsFromJWT(string token)
        {
            //Jika JWT token kosong makan return akan kosong
            if(token.IsNullOrEmpty()) return new ClaimVM();

            try
            {
                //Konfigurasi parameter token validasi
                var tokenValidationParameters = new TokenValidationParameters
                {   
                    ValidateIssuer = false, //ini akan true ketika kita create issuer
                    ValidateAudience = false, //ini akan true ketika kita create audience
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                                                        .GetBytes(_configuration["JWT:Key"]))
                };

                //parse dam validasi JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                //Extract claims dari JWT token
                if (securityToken != null && claimsPrincipal.Identity is ClaimsIdentity identity)
                {
                    var claims = new ClaimVM
                    {
                        NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                        Name = identity.FindFirst(ClaimTypes.Name)!.Value,
                        Email = identity.FindFirst(ClaimTypes.Email)!.Value
                    };

                    var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                                .Select(claim => claim.Value).ToList();
                                 claims.Roles = roles;

                    return claims;
                }
            }
            catch
            {
                return new ClaimVM();
            }

            return new ClaimVM();
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding
                                                     .UTF8
                                                     .GetBytes(_configuration["JWT:Key"]));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWT:Issuer"],
                                                    audience: _configuration["JWT:Audience"],
                                                    claims: claims,
                                                    expires: DateTime.Now.AddMinutes(10),
                                                    signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredTime(string token)
        {
            throw new NotImplementedException();
        }
    }
}
