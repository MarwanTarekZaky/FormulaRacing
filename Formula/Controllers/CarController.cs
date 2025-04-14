using IoC.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Formula.Controllers;

public class CarController : Controller
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }
    // GET
    public IActionResult Index()
    {
        ViewBag.cars =  _carService.GetAllAsync().GetAwaiter().GetResult();
        return View();
    }
}