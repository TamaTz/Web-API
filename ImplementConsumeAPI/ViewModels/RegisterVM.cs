using API.Utility;
using ImplementConsumeAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ImplementConsumeAPI.ViewModels
{
    public class RegisterVM
    {
        public string NIK { get; set; }

        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }

        public DateTime HiringDate { get; set; }

        [EmailAddress]
        /* [NIKEmailPhoneValidation(nameof(Email))]*/
        public string Email { get; set; }

        [Phone]
        /*  [NIKEmailPhoneValidation(nameof(PhoneNumber))]*/
        public string PhoneNumber { get; set; }

        public string Major { get; set; }

        public string Degree { get; set; }

        [Range(0, 4)]
        public float GPA { get; set; }

        public string UniversityCode { get; set; }

        public string UniversityName { get; set; }

        [PasswordValidation(ErrorMessage = "Password must contain at least 1 number, 1 uppercase, 1 lowercase, 1 symbol, 1 minimum chars")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
    public enum GenderLevel
    {
        Female,
        Male
    }
}
