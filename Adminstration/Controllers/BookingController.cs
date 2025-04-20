using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Adminstration.Controllers;

[Authorize]
public class BookingController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly IRaceService _raceService;

    public BookingController(IBookingService bookingService, IRaceService raceService)
    {
        _bookingService = bookingService;
        _raceService = raceService;
    }

    public async Task<IActionResult> Index()
    {
        var bookings = await _bookingService.GetAllAsync();
        return View(bookings);
    }

    public async Task<IActionResult> Create()
    {
        // You may want to pass data for the Car and Race dropdowns
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookingDTO bookingDto)
    {
        if (ModelState.IsValid)
        {
            await _bookingService.AddAsync(bookingDto);
            return RedirectToAction(nameof(Index));
        }

        return View(bookingDto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        return View(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, BookingDTO bookingDto)
    {
        if (id != bookingDto.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            await _bookingService.UpdateAsync(bookingDto);
            return RedirectToAction(nameof(Index));
        }

        return View(bookingDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null) return NotFound();

        return View(booking);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bookingDto = await _bookingService.GetByIdAsync(id);
        var raceDto = await _raceService.GetByIdAsync(bookingDto.RaceId);
        if (raceDto.NumberOfBookedSeats > 0)
        {
            raceDto.NumberOfBookedSeats--;
        }
        
        await _raceService.UpdateAsync(raceDto);
        await _bookingService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
    // Method to confirm a booking
    [HttpPost]
    public async Task<IActionResult> ConfirmBooking(int id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        var raceDto = _raceService.GetByIdAsync(booking.RaceId).GetAwaiter().GetResult();
        if (raceDto != null)
        {
            raceDto.NumberOfBookedSeats++;
            await _raceService.UpdateAsync(raceDto);
        }

        // Confirm the booking
        booking.IsConfirmed = true;
        await _bookingService.UpdateAsync(booking);  // Update confirmation flag

        return RedirectToAction(nameof(Index));  // Redirect to the Index page after confirmation
    }

    // Method to cancel a booking
    [HttpPost]
    public async Task<IActionResult> CancelBooking(int id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        // Cancel the booking
        booking.IsConfirmed = false;

        await _bookingService.DeleteAsync(id);  // Removing the Booking Request

        return RedirectToAction(nameof(Index));  // Redirect to the Index page after canceling
    }
}
