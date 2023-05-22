using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(BookingManagementDbContext context) : base(context) { }
    }
}
