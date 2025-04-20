using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.ViewModel;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Formula.Controllers;

public class RaceController : Controller
{
    private readonly IRaceService _raceService;
    private readonly IRaceCarService _raceCarService;
    private readonly IMapper _mapper;
    private readonly ICarService _carService;
    public RaceController(IRaceService raceService, IRaceCarService raceCarService, IMapper mapper, ICarService carService)
    {
        _raceService = raceService;
        _raceCarService = raceCarService;
        _mapper = mapper;
        _carService = carService;
    }
    // GET
    public IActionResult Index()
    {
        ViewBag.races =  _raceService.GetAllAsync(u => (u.Visibility == true) && (u.Occupancy > u.NumberOfBookedSeats )).GetAwaiter().GetResult();
        return View();
    }
    
    //GET
    public async Task<IActionResult> Details(int id)
    {
        
        var race = await _raceService.GetByIdAsync(id);
        var raceCars = await _raceCarService.GetByRaceIdAsync(id);
        
        var viewModel = new RaceDetailsViewModel
        {
            Race = race,
            Cars  = _mapper.Map<List<CarDTO>>(raceCars.Select(rc => rc.Car).ToList())
        };
        if (race == null)
        {
            return NotFound();
        }

        return View(viewModel);
    }
    
    //GET
    public async Task<IActionResult> RaceCarDetails(int raceId, int carId)
    {
        var race = await _raceService.GetByIdAsync(raceId);
        var car  = await _carService.GetByIdAsync(carId);
        ViewData["car"] = car;
        ViewData["race"] = race;
        if (race == null || car == null)
        {
            return NotFound();
        }
        return View();
    }
}