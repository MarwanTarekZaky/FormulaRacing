using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Formula.Controllers;

public class CarController : Controller
{
    private readonly ICarService _carService;
    private readonly IRaceService _raceService;
 
    public CarController(ICarService carService, IRaceService raceService)
    {
        _carService = carService;
        _raceService = raceService;
       
    }
    // GET
    public IActionResult Index()
    {
        ViewBag.cars =  _carService.GetAllAsync(u => u.Visibility == true).GetAwaiter().GetResult();
        return View();
    }
    
    // GET
    public async Task<IActionResult> CarImages(int carId)
    {
        var carDto = await _carService.GetByIdAsync(carId); 
        if (carDto == null)
        {
            return NotFound();
        }

        return View(carDto);
    }
}