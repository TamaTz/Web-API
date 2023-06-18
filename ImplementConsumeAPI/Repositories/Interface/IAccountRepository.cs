using ImplementConsumeAPI.Models;
using ImplementConsumeAPI.ViewModels;

namespace ImplementConsumeAPI.Repositories.Interface
{
    public interface IAccountRepository : IRepository<Account, string>
    {
        public Task<ResponseViewModel<string>> Logins(LoginVM entity);
        public Task<ResponseMessageVM> Registers(RegisterVM entity);
    }
}
