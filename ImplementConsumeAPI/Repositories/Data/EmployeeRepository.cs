using ImplementConsumeAPI.Models;
using ImplementConsumeAPI.Repositories.Interface;
using ImplementConsumeAPI.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace ImplementConsumeAPI.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, Guid>, IEmployeeRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;
        public EmployeeRepository(string request = "Employee/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7065/api/")
            };
            this.request = request;
        }
        public async Task<ResponseListVM<GetAllEmployee>> GetAllEmployee()
        {
            ResponseListVM<GetAllEmployee> entityVM = null;
            using (var response = httpClient.GetAsync(request + "GetAllMasterEmployee").Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<GetAllEmployee>>(apiResponse);
            }
            return entityVM;
        }
    }
}
