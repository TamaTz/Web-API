using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class UniversityRepository  : GenericRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context) 
        {}
    }
}
