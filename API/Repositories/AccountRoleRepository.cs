    using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class AccountRoleRepository : GenericRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingManagementDbContext context) : base(context) { }

        public IEnumerable<AccountRole> GetByAccountGuid(Guid accountGuid)
        {
            return _context.Set<AccountRole>().Where(a => a.AccountGuid == accountGuid);
        }

        public IEnumerable<AccountRole> GetByRoleGuidd(Guid roleGuid)
        {
            return _context.Set<AccountRole>().Where(r => r.RoleGuid == roleGuid);
        }
    }
}
