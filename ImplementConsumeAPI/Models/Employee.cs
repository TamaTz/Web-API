using System.ComponentModel.DataAnnotations.Schema;

namespace ImplementConsumeAPI.Models
{
    public class Employee
    {
        public Guid Guid { get; set; }

        public string Nik { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderLevel Gender { get; set; }

        public DateTime HiringDate { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }

    public enum GenderLevel
    {
        Female,
        Male
    }
}
