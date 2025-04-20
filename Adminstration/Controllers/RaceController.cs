using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using AutoMapper;
using Infrastructure.DTO;
using IoC.Services.Implementation;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Adminstration.Controllers;
[Authorize]
public class RaceController : Controller
{
    private readonly IRaceService _raceService;
    private readonly ILocationService _locationService;
    private readonly ICarService _carService;   
    private readonly IRaceCarService _raceCarService;

    public RaceController(IRaceService raceService, ILocationService locationService, ICarService carService, IRaceCarService raceCarService)
    {
        _raceService = raceService;
        _locationService = locationService;
        _carService = carService;
        _raceCarService = raceCarService;
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

    public async Task<IActionResult> Create()
    {
        ViewBag.Locations = await _locationService.GetSelectListAsync();
        ViewBag.Cars = await _carService.GetSelectListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RaceDTO raceDto)
    {
        if (ModelState.IsValid)
        {
            var raceId = await _raceService.AddAsync(raceDto);
            await _raceCarService.AddCarsToRaceAsync(raceId: raceId, raceDto.SelectedCarIds);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Cars = await _carService.GetSelectListAsync();
        ViewBag.Locations = await _locationService.GetSelectListAsync();
        return View(raceDto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var race = await _raceService.GetByIdAsync(id);
        if (race == null) return NotFound();
        ViewBag.Locations = await _locationService.GetSelectListAsync();
        ViewBag.Cars = await _carService.GetSelectListAsync();
        return View(race);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RaceDTO raceDto)
    {
        if (ModelState.IsValid)
        { 
            var raceId = await _raceService.UpdateAsync(raceDto);
            await _raceCarService.UpdateCarsOfRaceAsync(raceId:raceId, raceDto.SelectedCarIds);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Locations = await _locationService.GetSelectListAsync();
        ViewBag.Cars = await _carService.GetSelectListAsync();
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
        // 1. Delete related RaceCars first
        await _raceCarService.DeleteByRaceIdAsync(id);
        await _raceService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
