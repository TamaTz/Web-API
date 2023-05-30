using API.Contexts;
using API.Contracts;
using API.Models;
using API.Utility;
using API.View_Models.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace API.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        public EmployeeRepository(BookingManagementDbContext context, 
            IEducationRepository educationRepository,
            IUniversityRepository universityRepository) : base(context) 
        {
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
        }
        public EmpEdU? GetAllGuid(Employee employee, Education education, University university)
        {
            var data = new EmpEdU
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                FullName = employee.FirstName + " " + employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender.ToString(),
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityName = university.Name
            };

            return data;
        }

        public IEnumerable<EmpEdU> GetAllMasterEmp()
        {
            var employees = GetAll();
            var educations = _educationRepository.GetAll();
            var universities = _universityRepository.GetAll();

            var emped = new List<EmpEdU>();

            foreach (var employee in employees)
            {
                var education = educations.FirstOrDefault(e => e.Guid == employee.Guid);
                var university = universities.FirstOrDefault(u => u.Guid == education.UniversityGuid);

                var empedu = new EmpEdU
                {
                    Guid = employee.Guid,   
                    Nik = employee.Nik,
                    FullName = employee.FirstName + " " + employee.LastName,
                    BirthDate = employee.BirthDate,
                    Gender = employee.Gender.ToString(),
                    HiringDate = employee.HiringDate,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Major = education.Major,
                    Degree = education.Degree,
                    Gpa = education.Gpa,
                    UniversityName = university.Name
                };
                emped.Add(empedu);
            }
            return emped;
        }

        public EmpEdU? GetMasterEmployeeByGuid(Guid guid)
        {
            var employee = GetByGuid(guid);
            var education = _educationRepository.GetByGuid(guid);
            var university = _universityRepository.GetByGuid(education.UniversityGuid);

            var data = new EmpEdU
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                FullName = employee.FirstName + " " + employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender.ToString(),
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityName = university.Name
            };

            return data;
        }

        // Kel 2
        public int CreateWithValidate(Employee employee)
        {
            try
            {
                bool ExistsByEmail = _context.Employees.Any(e => e.Email == employee.Email);
                if (ExistsByEmail)
                {
                    return 1;
                }

                bool ExistsByPhoneNumber = _context.Employees.Any(e => e.PhoneNumber == employee.PhoneNumber);
                if (ExistsByPhoneNumber)
                {
                    return 2;
                }

                Create(employee);
                return 3;

            }
            catch
            {
                return 0;
            }
        }

        // Kelompok 5 dan 6
        public Guid? FindGuidByEmail(string email)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
                if (employee == null)
                {
                    return null;
                }
                return employee.Guid;
            }
            catch
            {
                return null;
            }

        }

        public bool CheckEmailAndPhoneAndNIK(string value)
        {
            return _context.Employees
                           .Any(e => e.Email == value ||
                                     e.PhoneNumber == value ||
                                     e.Nik == value);
        }

        public Employee GetByEmail(string email)
        {
            var employee = _context.Set<Employee>().FirstOrDefault(e => e.Email == email);
            return employee;
        }
    }
}
