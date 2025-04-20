using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;


namespace Adminstration.Controllers;
[Authorize]

public class HomeController : Controller
{
    
    private readonly IHomePageContentService _homePageContentService;
    public HomeController(IHomePageContentService homePageContentService)
    {
        _homePageContentService = homePageContentService;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Edit()
    {
        var dto = await _homePageContentService.Get();
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(HomePageContentDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _homePageContentService.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Documentation()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}