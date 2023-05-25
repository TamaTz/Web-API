using API.Models;

namespace API.Contracts
{
    public interface IEducationRepository  : IGenericRepository<Education>
    {
        // Kelompok 2
        Education GetByEmployeeId(Guid employeeId);
    }
}
