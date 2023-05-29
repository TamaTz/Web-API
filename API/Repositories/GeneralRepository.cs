using API.Contexts;
using API.Contracts;
using System.Xml.Linq;

namespace API.Repositories
{
    public class GeneralRepository<AllEntity> : IGenericRepository<AllEntity> where AllEntity : class
    {
        protected readonly BookingManagementDbContext _context;

        public GeneralRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public AllEntity? Create(AllEntity entity)
        {
            try
            {
                typeof(AllEntity).GetProperty("CreatedDate")!.SetValue(entity, DateTime.Now);
                typeof(AllEntity).GetProperty("ModifiedDate")!.SetValue(entity, DateTime.Now);

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
                var entity = GetByGuid(guid);
                if (entity == null)
                {
                    return false;
                }

                _context.Set<AllEntity>().Remove(entity);
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

        public AllEntity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<AllEntity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
        }

        public bool Update(AllEntity entity)
        {
            try
            {
                var guid = (Guid)typeof(AllEntity).GetProperty("Guid")!.GetValue(entity)!;

                var oldEntity = GetByGuid(guid);
                if (oldEntity == null)
                {
                    return false;
                }

                var getCreatedDate = typeof(AllEntity).GetProperty("CreatedDate")!.GetValue(oldEntity)!;

                typeof(AllEntity).GetProperty("CreatedDate")!.SetValue(entity, getCreatedDate);
                typeof(AllEntity).GetProperty("ModifiedDate")!.SetValue(entity, DateTime.Now);
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
