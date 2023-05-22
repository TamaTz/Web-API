using API.Contracts;
using API.Models;
using API.Repositories;
using API.View_Models.AccountRoles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountroleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _mapper;

        public AccountRoleController(IAccountRoleRepository accountroleRepository, IMapper<AccountRole, AccountRoleVM> mapper)
        {
            _accountroleRepository = accountroleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accountroles = _accountroleRepository.GetAll();
            if (!accountroles.Any())
            {
                return NotFound();
            }

            var data = accountroles.Select(_mapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid id)
        {
            var accountrole = _accountroleRepository.GetByGuid(id);
            if (accountrole is null)
            {
                return NotFound();
            }
            var data = _mapper.Map(accountrole);
            return Ok(data);

        }

        [HttpPost]
        public IActionResult Create(AccountRoleVM accountroleVM)
        {
            var accountroleConverted = _mapper.Map(accountroleVM);
            var result = _accountroleRepository.Create(accountroleConverted);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPut]
        public IActionResult Update(AccountRoleVM accountroleVM)
        {
            var accountroleConverted = _mapper.Map(accountroleVM);
            var IsUpdate = _accountroleRepository.Update(accountroleConverted);
            if (!IsUpdate)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountroleRepository.Delete(guid);
            if (isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }


    }
}
