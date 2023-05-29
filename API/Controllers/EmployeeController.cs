using API.Contracts;
using API.Models;
using API.View_Models.Employees;
using API.View_Models.Other;
using API.View_Models.Rooms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : BaseController<Employee, EmployeeVM>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Employee, EmployeeVM> _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, 
            IMapper<Employee, EmployeeVM> mapper, 
            IEducationRepository educationRepository, 
            IUniversityRepository universityRepository) : base(employeeRepository, mapper)
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
                return NotFound(new ResponseVM<List<EmpEdU>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            return Ok(new ResponseVM<IEnumerable<EmpEdU>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Employee",
                Data = masterEmployees

            });
        }

        [HttpGet("GetMasterEmployeeByGuid")]
        public IActionResult GetEmployeeById(Guid guid)
        {
            var employees = _employeeRepository.GetMasterEmployeeByGuid(guid);
            if (employees == null)
            {
                return NotFound(new ResponseVM<EmpEdU>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            return Ok(new ResponseVM<EmpEdU>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Employee",
                Data = employees
            });
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

        /*[HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return NotFound(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "NotFound"
                });
            }
            var data = _mapper.Map(employee);
            return Ok(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found",
                Data = data
            });
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var result = _employeeRepository.Create(employee);
            if (result is null)
            {
                return BadRequest(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed Create Employee"
                });
            }
            return Ok(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Create Employee"
            });
        }

        [HttpPut]
        public IActionResult Update(EmployeeVM employeeVM)
        {
            var employeeConverted = _mapper.Map(employeeVM);
            var isUpdated = _employeeRepository.Update(employeeConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed Update Employee"
                });
            }
            return Ok(new ResponseVM<Employee>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Update Employee"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _employeeRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed Delete Employee"
                });
            }
            return Ok(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Delete Employee"
            });
        }*/
    }
}
