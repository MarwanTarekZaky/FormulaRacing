using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adminstration.Controllers;

// Adminstration/Controllers/BannerController.cs
[Authorize]
public class BannerController : Controller
{
    private readonly IBannerService _bannerService;

    public BannerController(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    public async Task<IActionResult> Index()
    {
        var banners = await _bannerService.GetAllAsync();
        return View(banners);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(BannerDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _bannerService.AddAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _bannerService.GetByIdAsync(id);
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BannerDTO dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _bannerService.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        
        var bannerDto = await _bannerService.GetByIdAsync(id);
        if (bannerDto == null) return NotFound();
        return View(bannerDto);
    }
    
    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _bannerService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
