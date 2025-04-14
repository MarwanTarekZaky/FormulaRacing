using Domain.Models;
using Infrastructure.IRepository;
using Infrastructure.MainContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public BookingRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Booking> GetBookingsByUserEmailAsync(string userEmail)
    {
        return await _context.Bookings.Where(b => b.UserEmail == userEmail).OrderByDescending(u => u.Id).FirstOrDefaultAsync();
    }
}