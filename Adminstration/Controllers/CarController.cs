using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Domain.Models;
using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Adminstration.Controllers;

[Authorize]
public class CarController : Controller
{
    private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllAsync();
            return View(cars);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarDTO carDto)
        {
            if (ModelState.IsValid)
            {
                await _carService.AddAsync(carDto);
                return RedirectToAction(nameof(Index));
            }
            return View(carDto);
        }

        // GET: Car/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var carDto = await _carService.GetByIdAsync(id);
            if (carDto == null) return NotFound();
            return View(carDto);
        }

        // POST: Car/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarDTO carDto)
        {
            if (id != carDto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _carService.UpdateAsync(carDto);
                return RedirectToAction(nameof(Index));
            }
            return View(carDto);
        }

        // GET: Car/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var carDto = await _carService.GetByIdAsync(id);
            if (carDto == null) return NotFound();
            return View(carDto);
        }

        // POST: Car/DeleteConfirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _carService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null) return NotFound();

            return View(car);
        }
  
}