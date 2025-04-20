using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Domain.Models;
using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Adminstration.Controllers;
[Authorize]
public class CarImageController : Controller
    {
        private readonly ICarImageService _carImageService;

        public CarImageController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        // GET: CarImages
        public async Task<IActionResult> Index(int carId)
        {
            var carImage = await _carImageService.GetAllAsync(u => u.CarId == carId);
            ViewBag.carId = carId;
            return View(carImage);
        }

        // GET: CarImage/Create
        public IActionResult Create(int carId)
        {
            ViewBag.carId = carId;
            return View();
        }

        // POST: CarImage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int carId, CarImage carImage)
        {
            if (!ModelState.IsValid)
                return View(carImage);

            await _carImageService.AddAsync(carImage);
            return RedirectToAction(nameof(Index), new {carId});
        }

        
        // GET: CarImage/Edit/{id}
        public async Task<IActionResult> Edit(int carId, int id)
        {
            var carImage = await _carImageService.GetByIdAsync(id);
            if (carImage == null) return NotFound();
            ViewBag.carId = carId;
            return View(carImage);
        }

        // POST: CarImage/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int carId, CarImage carImage)
        {
            if (carId != carImage.CarId) return NotFound();
        
            if (ModelState.IsValid)
            {
                await _carImageService.UpdateAsync(carImage);
                return RedirectToAction(nameof(Index), new {carId});
            }
            ViewBag.carId = carId;
            return View(carImage);
        }
        
        
        // GET: CarImage/Delete/5
        public async Task<IActionResult> Delete(int id, int carId)
        {
            var carImage = await _carImageService.GetByIdAsync(id);
            if (carImage == null) return NotFound();
            ViewBag.carId = carId;
            return View(carImage);
        }

        // POST: CarImage/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int carId)
        {
            await _carImageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new {carId});
        }

   
    }