using API.Models;
using API.View_Models.Rooms;

namespace API.Contracts
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);

        IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();

        // Kelompok 4
        IEnumerable<RoomBookedTodayVM> GetAvailableRoom();

    }
}
