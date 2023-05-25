using API.Utility;
using Microsoft.AspNetCore.Routing.Constraints;

namespace API.View_Models.Employees
{
    public class EmpEdU
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public string UniversityName { get; set; }
    }
}
