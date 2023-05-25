using API.Contracts;
using API.Models;
using API.View_Models.Employees;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Employee, EmployeeVM> _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper<Employee, EmployeeVM> mapper, IEducationRepository educationRepository, IUniversityRepository universityRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
        }
        /*
                [HttpGet]
                public IActionResult GetAll()
                {
                    var employees = _employeeRepository.GetAll();
                    if (!employees.Any())
                    {
                        return NotFound();
                    }
                    var resultConverted = employees.Select(_mapper.Map).ToList();
                    return Ok(resultConverted);
                }*/

        [HttpGet("GetAllMasterEmployee")]
        public IActionResult GetAll()
        {
            var masterEmployees = _employeeRepository.GetAllMasterEmp();
            if (!masterEmployees.Any())
            {
                return NotFound();
            }
            return Ok(masterEmployees);
        }

        [HttpGet("GetMasterEmployeeByGuid")]
        public IActionResult GetEmployeeById(Guid guid)
        {
            var employees = _employeeRepository.GetMasterEmployeeByGuid(guid);
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        /*[HttpGet("GetAllByGuid")]
        public IActionResult GetByGuidWithEducation(Guid guid)
        {
            try
            {
                var employee = _employeeRepository.GetByGuid(guid);
                if (employee is null)
                {
                    return NotFound();
                }

                var education = _educationRepository.GetByGuid(guid);
                if (education is null)
                {
                    return NotFound();
                }

                var university = _universityRepository.GetByGuid(education.UniversityGuid);
                if (university is null)
                {
                    return NotFound();
                }

                var employees = _employeeRepository.GetAllGuid(employee, education, university);
                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception)
            {
                throw;
            }
        }*/

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return NotFound();
            }
            var data = _mapper.Map(employee);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var result = _employeeRepository.Create(employee);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(EmployeeVM employeeVM)
        {
            var employeeConverted = _mapper.Map(employeeVM);
            var isUpdated = _employeeRepository.Update(employeeConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _employeeRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
