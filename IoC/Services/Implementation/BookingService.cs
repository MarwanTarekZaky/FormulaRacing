using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.IRepository;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Hosting;

namespace IoC.Services.Implementation;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddAsync(BookingDTO bookingDto)
    {
        var booking = _mapper.Map<Booking>(bookingDto);
        await _unitOfWork.Bookings.AddAsync(booking);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(BookingDTO bookingDto)
    {
        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingDto.Id);
        if (booking == null) return;

        _mapper.Map(bookingDto, booking);
        _unitOfWork.Bookings.Update(booking);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(int bookingId)
    {
        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
        if (booking != null)
        {
            _unitOfWork.Bookings.Remove(booking);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async Task<BookingDTO> GetByIdAsync(int bookingId)
    {
        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
        return booking == null ? null : _mapper.Map<BookingDTO>(booking);
    }

    public async Task<IEnumerable<BookingDTO>> GetAllAsync()
    {
        var bookings = await _unitOfWork.Bookings.GetAllAsync();
        var bookingDtos = _mapper.Map<IEnumerable<BookingDTO>>(bookings);

        // Populate CarName and RaceName for each booking
        foreach (var bookingDto in bookingDtos)
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(bookingDto.CarId);
            var race = await _unitOfWork.Races.GetByIdAsync(bookingDto.RaceId);
            if (car != null)
            {
                bookingDto.CarName = car.BrandName;
                bookingDto.Car = car;
                // Replace with the actual property name for car name
            } 

            if (race != null)
            {
                bookingDto.RaceName = race.Name;
                bookingDto.Race = race;
                // Replace with the actual property name for race name
            }
        }
        return bookingDtos;
    }
    
    public async Task<Booking> GetBookingsByUserEmailAsync(string userEmail)
    {
        return await _unitOfWork.Bookings.GetBookingsByUserEmailAsync(userEmail);
    }
}
