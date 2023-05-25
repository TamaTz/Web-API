using API.Contexts;
using API.Contracts;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EducationRepository : GenericRepository<Education>, IEducationRepository
    {
        public EducationRepository(BookingManagementDbContext context) : base(context)
        { }

        public Education GetByEmployeeId(Guid employeeId)
        {
            return _context.Set<Education>().Find(employeeId);
        }
    }
}
