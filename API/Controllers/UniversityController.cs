using API.Contracts;
using API.Models;
using API.ViewModels.Educations;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    public UniversityController(IUniversityRepository universityRepository, IEducationRepository educationRepository)
    {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
    }
    
    [HttpGet("WithEducation")]
    public IActionResult GetAllWithEducation()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any()) {
            return NotFound();
        }

        var results = new List<UniversityEducationVM>();
        foreach (var university in universities) {
            var education = _educationRepository.GetByUniversityId(university.Guid);
            var educationMapped = education.Select(EducationVM.ToVM);
            
            var result = new UniversityEducationVM {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name,
                Educations = educationMapped
            };
            
            results.Add(result);
        }

        return Ok(results);
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any()) {
            return NotFound();
        }

        /*var univeritiesConverted = new List<UniversityVM>();
        foreach (var university in universities) {
            var result = UniversityVM.ToVM(university);
            univeritiesConverted.Add(result);
        }*/
        
        var resultConverted = universities.Select(UniversityVM.ToVM);
        
        return Ok(resultConverted);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        if (university is null) {
            return NotFound();
        }
        
        return Ok(university);
    }

    [HttpPost]
    public IActionResult Create(UniversityVM universityVM)
    {
        var universityConverted = UniversityVM.ToModel(universityVM);
        
        var result = _universityRepository.Create(universityConverted);
        if (result is null) {
            return BadRequest();
        }
        
        return Ok(result);
    }
    
    [HttpPut]
    public IActionResult Update(University university)
    {
        var isUpdated = _universityRepository.Update(university);
        if (!isUpdated) {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _universityRepository.Delete(guid);
        if (!isDeleted) {
            return BadRequest();
        }
        
        return Ok();
    }
}
