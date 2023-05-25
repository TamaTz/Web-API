using API.Models;
using API.View_Models.Employees;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IEnumerable<EmpEdU> GetAllMasterEmp();

        public EmpEdU? GetMasterEmployeeByGuid(Guid guid);

        // Kelompok 2
        int CreateWithValidate(Employee employee);

        // Kelompok 5
        Guid? FindGuidByEmail(string email);
    }


}
