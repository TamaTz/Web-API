using API.Contracts;
using API.Models;
using API.View_Models.Other;
using API.View_Models.Roles;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : BaseController<Role, RoleVM>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Role, RoleVM> _mapper;
        public RoleController(IRoleRepository roleRepository,
            IMapper<Role, RoleVM> mapper) : base(roleRepository, mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
/*
        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = roles.Select(_mapper.Map).ToList();
            return Ok(new ResponseVM<List<RoleVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Role",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return NotFound(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status404NotFound
                     ,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                }); 
            }
            var data = _mapper.Map(role);
            return NotFound(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status404NotFound
                    ,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }

        [HttpPost]
        public IActionResult Create(RoleVM roleVM)
        {
            var roleConverted = _mapper.Map(roleVM);
            var result = _roleRepository.Create(roleConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Create Role Failed"
                });
            }
            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create Role Success"
            });
        }

        [HttpPut]
        public IActionResult Update(RoleVM roleVM)
        {
            var roleConverted = _mapper.Map(roleVM);
            var isUpdated = _roleRepository.Update(roleConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Update Role Failed"
                });
            }
            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Role Success"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<RoleVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Delete Role Failed"
                });
            }
            return Ok(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Role Success"
            });
        }*/
    }
}
