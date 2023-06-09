﻿using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.View_Models.Accounts;
using API.View_Models.Login;
using API.View_Models.Other;
using API.View_Models.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController<Account, AccountVM>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Account, AccountVM> _mapper;
        public AccountController(IAccountRepository accountRepository,
            IMapper<Account, AccountVM> mapper,
            IEmailService emailService,
            IEducationRepository educationRepository,
            IUniversityRepository universityRepository,
            IEmployeeRepository employeeRepository,
            ITokenService tokenServiceRepository,
            IRoleRepository roleRepository) : base(accountRepository, mapper)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _emailService = emailService;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _tokenService = tokenServiceRepository;
        }

        // Kelompok 2
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(RegisterVM registerVM)
        {

            var result = _accountRepository.Register(registerVM);
            switch (result)
            {
                case 0:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Registration Failed",
                        Data = null
                    });
                case 1:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Email already exists",
                        Data = null
                    });
                case 2:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Phone number already exists",
                        Data = null
                    });
                case 3:
                    return Ok(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Registration Success",
                        Data = null
                    });
            }

            return Ok(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Registration success"
            });

        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(LoginVM loginVM)
        {
            var account = _accountRepository.Login(loginVM);
            var employee = _employeeRepository.GetByEmail(loginVM.Email);

            if (account == null)
            {
                return NotFound(new ResponseVM<LoginVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account not found",
                    Data = null
                });
            }

            if (account.Password != loginVM.Password)
            {
                return BadRequest(new ResponseVM<LoginVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Password Invalid",
                    Data = null
                });
            }
            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, employee.Nik),
                    new(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"),
                    new(ClaimTypes.Email, employee.Email),
                };

            var roles = _accountRepository.GetRoles(employee.Guid);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            var token = _tokenService.GenerateToken(claims);

            return Ok(new ResponseVM<string>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Success",
                Data = token
            });

        }

        // Kelompok 6
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            // Cek apakah email dan OTP valid
            var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
            var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
            switch (changePass)
            {
                case 0:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Unable to Change Password",
                        Data = null
                    });
                case 1:
                    return Ok(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Password has been changed successfully",
                        Data = null
                    });
                case 2:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Invalid OTP",
                        Data = null
                    });
                case 3:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP has already been used",
                        Data = null
                    });
                case 4:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP expired",
                        Data = null
                    });
                case 5:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Wrong Password, Not the Same",
                        Data = null
                    });
                default:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Unable to Change Password",
                        Data = null
                    });
            }
        }

        // Kelompok 5
        [AllowAnonymous]
        [HttpPost("ForgotPassword/email")]
        public IActionResult UpdateResetPass(String email)
        {

            var getGuid = _employeeRepository.FindGuidByEmail(email);
            if (getGuid == null)
            {
                return NotFound(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not Found",
                    Data = null
                });
            }

            var isUpdated = _accountRepository.UpdateOTP(getGuid);

            switch (isUpdated)
            {
                case 0:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP Failed to Generate",
                        Data = null
                    });
                default:

                        
                    _emailService.SetEmail(email)
                                 .SetSubject("Forgot Password")
                                 .SetHtmlMessage($"Your OTP is {isUpdated}")
                                 .SendEmailAsync();

                    return Ok(new ResponseVM<AccountResetPasswordVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "OTP Successfully Sent to Email"
                    });

            }
        }

        [HttpGet("GetClaims/{token}")]
        public IActionResult GetByToken(string token)
        {
            var data = _tokenService.ExtractClaimsFromJWT(token);
            if (data is null)
            {
                return BadRequest(new ResponseVM<ClaimVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Token is invalid or expired",
                });
            }

            return Ok(new ResponseVM<ClaimVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Claim has been retrieved",
                Data = data
            });
        }
    }
}
