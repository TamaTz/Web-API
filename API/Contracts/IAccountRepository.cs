using API.Models;
using API.View_Models.Accounts;
using API.View_Models.Login;

namespace API.Contracts
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        LoginVM Login(LoginVM loginVM);

        //Kelompok 2
        int Register(RegisterVM registerVM);

        //Kemlompok 3
        int UpdateOTP(Guid? employeeGuid);

        //Kelompok 6
        int ChangePasswordAccount(Guid? employeeGuid, ChangePasswordVM changePasswordVM);
    }
}
