using API.Contracts;
using API.Models;
using API.Repositories;
using API.View_Models.AccountRoles;
using API.View_Models.Other;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : BaseController<AccountRole, AccountRoleVM>
    {
        private readonly IAccountRoleRepository _accountroleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _mapper;

        public AccountRoleController(IAccountRoleRepository accountroleRepository,
            IMapper<AccountRole, AccountRoleVM> mapper) : base(accountroleRepository, mapper)
        {
           
        }

        /*[HttpGet]
        public IActionResult GetAll()
        {
            var accountroles = _accountroleRepository.GetAll();
            if (!accountroles.Any())
            {
                return NotFound(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Not Found"
                });
            }

            var data = accountroles.Select(_mapper.Map).ToList();
            return Ok(new ResponseVM<List<AccountRoleVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data AccountRole",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var accountrole = _accountroleRepository.GetByGuid(id);
            if (accountrole is null)
            {
                return NotFound(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Not Found Guid"
                });
            }
            var data = _mapper.Map(accountrole);
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Guid",
                Data = data
            });

        }

        [HttpPost]
        public IActionResult Create(AccountRoleVM accountroleVM)
        {
            var accountroleConverted = _mapper.Map(accountroleVM);
            var result = _accountroleRepository.Create(accountroleConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Create Account Failed"
                });
            }
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create Account Success"
            });
        }
        [HttpPut]
        public IActionResult Update(AccountRoleVM accountroleVM)
        {
            var accountroleConverted = _mapper.Map(accountroleVM);
            var IsUpdate = _accountroleRepository.Update(accountroleConverted);
            if (!IsUpdate)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Update Account Failed"
                });
            }
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create Account Success"
            });
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountroleRepository.Delete(guid);
            if (isDeleted)
            {
                return BadRequest(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Create Account Failed"
                });
            }
            return Ok(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create Account Create"
            });
        }
*/

    }
}
