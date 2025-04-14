using IoC.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Formula.Controllers;

public class RaceController : Controller
{
    private readonly IRaceService _raceService;

    public RaceController(IRaceService raceService)
    {
        _raceService = raceService;
    }
    // GET
    public IActionResult Index()
    {
        ViewBag.races =  _raceService.GetAllAsync().GetAwaiter().GetResult();
        return View();
    }
}