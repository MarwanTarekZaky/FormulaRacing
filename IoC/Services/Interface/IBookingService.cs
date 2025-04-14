using Domain.Models;
using Infrastructure.DTO;

namespace IoC.Services.Interface;

public interface IBookingService
{
    Task<IEnumerable<BookingDTO>> GetAllAsync();
    Task<BookingDTO?> GetByIdAsync(int id);
    Task AddAsync(BookingDTO bookingDto);
    Task UpdateAsync(BookingDTO bookingDto);
    Task DeleteAsync(int id);
   
    Task<Booking> GetBookingsByUserEmailAsync(string userEmail);
}