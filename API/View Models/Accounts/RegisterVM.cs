using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.View_Models.Accounts
{
    public class RegisterVM
    {
        //public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }

        public DateTime HiringDate { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Major { get; set; }

        public string Degree { get; set; }

        public float GPA { get; set; }

        //public Guid UniversityGuid { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        // public University? University { get; set; }
    }
}
