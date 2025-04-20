using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using IoC.Services.Interface;
using Infrastructure.DTO;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Formula.Controllers
{
    public class BookingController : Controller
    {
        private readonly ICarService _carService;
        private readonly IRaceService _raceService;
        private readonly IBookingService _bookingService;
        private readonly IRaceCarService _raceCarService;
        private readonly IMapper _mapper;

        public BookingController(ICarService carService, IRaceService raceService, IBookingService bookingService, IMapper mapper, IRaceCarService raceCarService)
        {
            _carService = carService;
            _raceService = raceService;
            _bookingService = bookingService;
            _mapper = mapper;
            _raceCarService = raceCarService;
        }

        
        // GET: Test Action For Razor View 
        public IActionResult Test()
        {
            return View();
        }
        // GET: Display the Booking Request Form
        [HttpGet]
        public async Task<IActionResult> BookingRequest(int? carId, int? raceId)
        {
            var racesWithCars = await _raceCarService.GetRacesWithCarsAsync();

            BookingFormViewModel model = new()
            {
                Booking = new Booking
                {
                    CarId = carId,
                    RaceId = raceId
                },
                Races = racesWithCars.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList(),

                Cars = new List<SelectListItem>() // Initially empty
            };

            return View(model);
        }


        // POST: Handle Booking Request Form submission
        [HttpPost]
        public async Task<IActionResult> BookingRequest(BookingFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDTO = _mapper.Map<BookingDTO>(model.Booking);
                await _bookingService.AddAsync(modelDTO);
                
                return RedirectToAction("Confirmation");
            }

            model.Cars = _carService.GetSelectListAsync().GetAwaiter().GetResult();
            model.Races = (_raceService.GetSelectListAsync(u => (u.Visibility == true) && (u.Occupancy > u.NumberOfBookedSeats ))).GetAwaiter().GetResult();
            
            return View("BookingRequest");
        }

        // GET: Confirmation page after submission
        public IActionResult Confirmation()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCarsByRace(int raceId)
        {
            var cars = await _raceCarService.GetCarsByRaceIdAsync(raceId); // You will implement this
            var carList = cars.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Model
            }).ToList();

            return Json(carList);
        }

    }
}

