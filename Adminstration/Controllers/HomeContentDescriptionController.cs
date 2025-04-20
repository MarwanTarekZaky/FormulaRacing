using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;


namespace Adminstration.Controllers;
[Authorize]

public class HomeContentDescriptionController : Controller
{
    
    private readonly IHomeContentDescriptionService _homeContentDescriptionService;
    public HomeContentDescriptionController(IHomeContentDescriptionService homeContentDescriptionService)
    {
        _homeContentDescriptionService = homeContentDescriptionService;
    }

    public async Task<IActionResult> Index()
    {
        var dto =  await _homeContentDescriptionService.GetLastAsync();
        ViewData["Title"] = "Home Page Content";
        if (dto == null)
        {
            ViewData["Message"] = "There is no home content description.";
            return RedirectToAction(nameof(Create));
        }
        
        return View(dto);
    }
    //GET
    public async Task<IActionResult> Create()
    {
     
        ViewData["Title"] = "Home Page Content";
        var dto = new HomeContentDescriptionDTO(); // Empty DTO
        return View(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Create(HomeContentDescriptionDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _homeContentDescriptionService.AddAsync(dto);
        ViewData["Title"] = "Home Page Content";
        return RedirectToAction(nameof(Index));
    }
    //GET
    public async Task<IActionResult> EditRaceDescription()
    {
        var dto =  await _homeContentDescriptionService.GetLastAsync();
        ViewData["Title"] = "Race Description Content";
        return View(dto);
    }
    [HttpPost]
    public async Task<IActionResult> EditRaceDescription(HomeContentDescriptionDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _homeContentDescriptionService.UpdatePartialAsync(dto);
        ViewData["Title"] = "Home Page Content";
        return RedirectToAction(nameof(Index));
    }
    //GET
    public async Task<IActionResult> EditTrackDescription()
    {
        var dto =  await _homeContentDescriptionService.GetLastAsync();
        ViewData["Title"] = "Track Description Content";
        return View(dto);
    }
  
    [HttpPost]
    public async Task<IActionResult> EditTrackDescription(HomeContentDescriptionDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _homeContentDescriptionService.UpdatePartialAsync(dto);
        ViewData["Title"] =  "Home Page Content";
        return RedirectToAction(nameof(Index));
    }
    
}