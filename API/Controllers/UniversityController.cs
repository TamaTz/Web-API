using API.Contracts;
using API.Models;
using API.View_Models.Educations;
using API.View_Models.Other;
using API.View_Models.Universities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UniversityController : BaseController<University, UniversityVM>
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<University, UniversityVM> _mapper;
    private readonly IMapper<Education, EducationVM> _educationMapper;
    public UniversityController
        (IUniversityRepository universityRepository,
        IEducationRepository educationRepository,
        IMapper<University, UniversityVM> mapper,
        IMapper<Education, EducationVM> educationMapper) : base(universityRepository, mapper)
        {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _mapper = mapper;
        _educationMapper = educationMapper;
    }


}

    /*[HttpGet]
    public IActionResult GetAll()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any())
        {
            return NotFound(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Not Found Data University"
            });
        }

        var data = universities.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<UniversityVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Data University",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        if (university is null)
        {
            return NotFound(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status404NotFound
                    ,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid Not Found"
            });
        }

        var data = _mapper.Map(university);

        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Guid Found",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(UniversityVM universityVM)
    {
        var universityConverted = _mapper.Map(universityVM);

        var result = _universityRepository.Create(universityConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status400BadRequest
                    ,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create University Failed"
            });
        }

        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create University Success"
        });
    }

    [HttpPut]
    public IActionResult Update(University university)
    {
        var isUpdated = _universityRepository.Update(university);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update University Failed"
            });
        }

        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update University Success"
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _universityRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete University Failed"
            });
        }

        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update University Success"
        });
    }*/