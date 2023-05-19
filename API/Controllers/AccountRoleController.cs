using API.Contracts;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountroleRepository;

        public AccountRoleController(IAccountRoleRepository accountroleRepository)
        {
            _accountroleRepository = accountroleRepository;
        }


    }
}
