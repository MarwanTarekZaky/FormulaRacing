using AutoMapper;
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
        private readonly IMapper _mapper;

        public BookingController(ICarService carService, IRaceService raceService, IBookingService bookingService, IMapper mapper)
        {
            _carService = carService;
            _raceService = raceService;
            _bookingService = bookingService;
            _mapper = mapper;
        }

        // GET: Display the Booking Request Form
        [HttpGet]
        public IActionResult BookingRequest()
        {
            BookingFormViewModel model = new()
            {
                Cars = _carService.GetSelectListAsync().GetAwaiter().GetResult(),
                Races = _raceService.GetSelectListAsync().GetAwaiter().GetResult()
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

            model.Races = _carService.GetSelectListAsync().GetAwaiter().GetResult();
            model.Cars = _raceService.GetSelectListAsync().GetAwaiter().GetResult();
            
            return View("BookingRequest");
        }

        // GET: Confirmation page after submission
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}

