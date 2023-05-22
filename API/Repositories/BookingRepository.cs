using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories;

public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    public BookingRepository(BookingManagementDbContext context) : base(context) { }
}
