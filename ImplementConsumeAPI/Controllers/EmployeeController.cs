using ImplementConsumeAPI.Models;
using ImplementConsumeAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ImplementConsumeAPI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var employees = new List<Employee>();

            if (result.Data != null)
            {
                employees = result.Data?.Select(e => new Employee
                {
                    Guid = e.Guid,
                    Nik = e.Nik,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    Gender = e.Gender,
                    HiringDate = e.HiringDate,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    CreatedDate = e.CreatedDate,
                    ModifiedDate = e.ModifiedDate
                }).ToList();
            }

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employeeResult = await repository.GetAllEmployee();
            var employees = new List<GetAllEmployee>();

            if (employeeResult.Data != null)
            {
                employees = employeeResult.Data?.Select(e => new GetAllEmployee
                {
                    Guid = e.Guid,
                    NIK = e.NIK,
                    FullName = e.FullName,
                    BirthDate = e.BirthDate,
                    Gender = e.Gender,
                    HiringDate = e.HiringDate,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Major = e.Major,
                    Degree = e.Degree,
                    GPA = e.GPA,
                    UniversityName = e.UniversityName

                }).ToList();
            }
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            /* if (ModelState.IsValid)
             {*/
            var result = await repository.Post(employee);
            if (result.StatusCode == "200")
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.StatusCode == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            /*}*/
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            /* if (ModelState.IsValid)
             {*/
            var result = await repository.Put(employee);
            if (result.StatusCode == "200")
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.StatusCode == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            /* }*/
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var employee = new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employee);
            }
            else
            {
                employee.Guid = result.Data.Guid;
                employee.Nik = result.Data.Nik;
                employee.FirstName = result.Data.FirstName;
                employee.LastName = result.Data.LastName;
                employee.BirthDate = result.Data.BirthDate;
                employee.Gender = result.Data.Gender;
                employee.HiringDate = result.Data.HiringDate;
                employee.Email = result.Data.Email;
                employee.PhoneNumber = result.Data.PhoneNumber;
                employee.CreatedDate = result.Data.CreatedDate;
                employee.ModifiedDate = result.Data.ModifiedDate;
            }

            return View(employee);
        }

        public async Task<IActionResult> Delete(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var employee = new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employee);
            }
            else
            {
                employee.Guid = result.Data.Guid;
                employee.Nik = result.Data.Nik;
                employee.FirstName = result.Data.FirstName;
                employee.LastName = result.Data.LastName;
                employee.BirthDate = result.Data.BirthDate;
                employee.Gender = result.Data.Gender;
                employee.HiringDate = result.Data.HiringDate;
                employee.Email = result.Data.Email;
                employee.PhoneNumber = result.Data.PhoneNumber;
            }
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid Guid)
        {
            var result = await repository.Delete(Guid);
            if (result.StatusCode == "200")
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
