using API.Contexts;
using API.Models;

namespace API.Contracts
{
    public interface IUniversityRepository : IGenericRepository<University>
    {
        // Kelompok 2
        University CreateWithValidate(University university);

    }
}
