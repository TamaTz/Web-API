using API.Contexts;
using API.Contracts;
using System.Xml.Linq;

namespace API.Repositories
{
    public class GenericRepository<AllEntity> : IGenericRepository<AllEntity> where AllEntity : class
    {
        private readonly BookingManagementDbContext _context;
        public GenericRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public AllEntity Create(AllEntity entity)
        {
            try
            {
                _context.Set<AllEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var allentity = GetByGuid(guid);
                if (allentity == null)
                {
                    return false;
                }

                _context.Set<AllEntity>().Remove(allentity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<AllEntity> GetAll()
        {
            return _context.Set<AllEntity>().ToList();
        }

        public AllEntity GetByGuid(Guid guid)
        {
            return _context.Set<AllEntity>().Find(guid);
        }

        public bool Update(AllEntity entity)
        {
            try
            {
                _context.Set<AllEntity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
