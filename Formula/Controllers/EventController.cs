using IoC.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Formula.Controllers;

public class EventController : Controller
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
    // GET
    public IActionResult Index()
    {
        ViewBag.events = _eventService.GetAllAsync(u => u.Visibility == true).GetAwaiter().GetResult();
        return View();
    }
}