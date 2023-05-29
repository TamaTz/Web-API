using API.Contracts;
using API.Models;
using API.View_Models.Educations;
using API.View_Models.Other;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : BaseController<Education, EducationVM>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<Education, EducationVM> _mapper;
        public EducationController(IEducationRepository educationRepository, 
            IMapper<Education, EducationVM> mapper) : base(educationRepository, mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

       /* [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return NotFound(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = educations.Select(_mapper.Map).ToList();
            return Ok(new ResponseVM<List<EducationVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Education",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var education = _educationRepository.GetByGuid(guid);
            if (education is null)
            {
                return NotFound(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            var data = _mapper.Map(education);

            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Education",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(EducationVM educationVM)
        {
            var educationConverted = _mapper.Map(educationVM);
            
            var result = _educationRepository.Create(educationConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status400BadRequest
                    ,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed Create Education"
                });
            }
            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Create Education"
            });
        }

        [HttpPut]
        public IActionResult Update(EducationVM educationVM)
        {
            var educationConverted = _mapper.Map(educationVM);
            var isUpdated = _educationRepository.Update(educationConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status400BadRequest
                   ,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed Update Education"
                });
            }
            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Update Education"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _educationRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<EducationVM>
                {
                    Code = StatusCodes.Status400BadRequest
                    ,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed Delete Education"
                });
            }
            return Ok(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Delete Education"
            });
        }*/

    }
}
