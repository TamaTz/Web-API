using API.Models;
using API.View_Models.Bookings;

namespace API.Contracts
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        IEnumerable<BookingDurationVM> GetBookingDuration();

        // Kelompok 4
        IEnumerable<BookingDetailVM> GetAllBookingDetail();
        BookingDetailVM GetBookingDetailByGuid(Guid guid);
    }
}
