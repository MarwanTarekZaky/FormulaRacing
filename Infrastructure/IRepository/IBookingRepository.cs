using Domain.Models;

namespace Infrastructure.IRepository;

public interface IBookingRepository: IRepository<Booking>
{
    Task<Booking> GetBookingsByUserEmailAsync(string userEmail);
}