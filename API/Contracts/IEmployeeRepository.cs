using API.Models;
using API.View_Models.Employees;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IEnumerable<EmpEdU> GetAllMasterEmp();

        public EmpEdU? GetMasterEmployeeByGuid(Guid guid);

        bool CheckEmailAndPhoneAndNIK(string value);

        // Kelompok 5
        Guid? FindGuidByEmail(string email);

        public Employee GetByEmail(string email);
    }


}
