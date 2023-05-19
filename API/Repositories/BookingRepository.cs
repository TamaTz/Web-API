﻿using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingManagementDbContext _context;

        public BookingRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        public Booking Create(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Add(booking);
                _context.SaveChanges();
                return booking;
            }
            catch
            {
                return new Booking();
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var booking = GetByGuid(guid);
                if (booking == null)
                {
                    return false;
                }
                _context.Set<Booking>().Remove(booking);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Set<Booking>().ToList();
        }

        public Booking? GetByGuid(Guid guid)
        {
            return _context.Set<Booking>().Find(guid);
        }

        public bool Update(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Update(booking);
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
