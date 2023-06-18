using ImplementConsumeAPI.Models;
using ImplementConsumeAPI.Repositories.Interface;

namespace ImplementConsumeAPI.Repositories.Data
{
    public class UniversityRepository : GeneralRepository<University,Guid>, IUniversityRepository
    {
        public UniversityRepository(string request = "University/") : base(request)
        {

        }
    }
}
