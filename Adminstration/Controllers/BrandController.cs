using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Adminstration.Controllers;
[Authorize]
public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // GET: Brand
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            return View(brands);
        }

        // GET: Brand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandDTO brandDto)
        {
            if (!ModelState.IsValid)
                return View(brandDto);

            await _brandService.AddAsync(brandDto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Brand/Edit/{id: int}
        public async Task<IActionResult> Edit(int id)
        {
            var brandDto = await _brandService.GetByIdAsync(id);
            if (brandDto == null) return NotFound();

            return View(brandDto);
        }

        // POST: Brand/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( BrandDTO brandDto)
        {   
            if (!ModelState.IsValid)
                return View(brandDto);
            
            await _brandService.UpdateAsync(brandDto);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Brand/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var brandDto = await _brandService.GetByIdAsync(id);
            if (brandDto == null) return NotFound();

            return View(brandDto);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _brandService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Brand/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var brandDto = await _brandService.GetByIdAsync(id);
            if (brandDto == null) return NotFound();

            return View(brandDto);
        }
    }