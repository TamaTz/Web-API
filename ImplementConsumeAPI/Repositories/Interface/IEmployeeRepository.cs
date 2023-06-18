using ImplementConsumeAPI.Models;
using ImplementConsumeAPI.ViewModels;

namespace ImplementConsumeAPI.Repositories.Interface
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        public Task<ResponseListVM<GetAllEmployee>> GetAllEmployee();
    }
}
