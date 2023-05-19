using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly BookingManagementDbContext _context;
        public AccountRoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        public AccountRole Create(AccountRole accountRole)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccountRole? GetByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public bool Update(AccountRole accountRole)
        {
            throw new NotImplementedException();
        }
    }
}
