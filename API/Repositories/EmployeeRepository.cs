using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class EmployeeRepository :  GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingManagementDbContext context) : base(context) { }
    }
}
