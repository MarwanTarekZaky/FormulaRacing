using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Adminstration.Controllers;
[Authorize]
public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: Location
        public async Task<IActionResult> Index()
        {
            var locationDtos = await _locationService.GetAllAsync();
            return View(locationDtos);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationDTO locationDto)
        {
            if (!ModelState.IsValid)
                return View(locationDto);

            await _locationService.AddAsync(locationDto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Location/Edit/{id: int}
        public async Task<IActionResult> Edit(int id)
        {
            var locationDto = await _locationService.GetByIdAsync(id);
            if (locationDto == null) return NotFound();

            return View(locationDto);
        }

        // POST: Location/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( LocationDTO locationDto)
        {   
            if (!ModelState.IsValid)
                return View(locationDto);
            
            await _locationService.UpdateAsync(locationDto);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Location/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var locationDto = await _locationService.GetByIdAsync(id);
            if (locationDto == null) return NotFound();

            return View(locationDto);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _locationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var locationDto = await _locationService.GetByIdAsync(id);
            if (locationDto == null) return NotFound();

            return View(locationDto);
        }
    }