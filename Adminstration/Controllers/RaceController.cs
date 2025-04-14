using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Adminstration.Controllers;
[Authorize]
public class RaceController : Controller
{
    private readonly IRaceService _raceService;

    public RaceController(IRaceService raceService)
    {
        _raceService = raceService;
    }

    public async Task<IActionResult> Index()
    {
        var races = await _raceService.GetAllAsync();
        return View(races);
    }

    public async Task<IActionResult> Details(int id)
    {
        var race = await _raceService.GetByIdAsync(id);
        if (race == null) return NotFound();

        return View(race);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RaceDTO raceDto)
    {
        if (ModelState.IsValid)
        {
            await _raceService.AddAsync(raceDto);
            return RedirectToAction(nameof(Index));
        }
        return View(raceDto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var race = await _raceService.GetByIdAsync(id);
        if (race == null) return NotFound();

        return View(race);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RaceDTO raceDto)
    {
        if (ModelState.IsValid)
        {
            await _raceService.UpdateAsync(raceDto);
            return RedirectToAction(nameof(Index));
        }
        return View(raceDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var race = await _raceService.GetByIdAsync(id);
        if (race == null) return NotFound();

        return View(race);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _raceService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
