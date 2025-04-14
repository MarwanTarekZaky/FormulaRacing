using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Infrastructure.DTO;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Adminstration.Controllers;
[Authorize]
public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllAsync();
            return View(events);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventDTO eventDto)
        {
            if (!ModelState.IsValid)
                return View(eventDto);

            await _eventService.AddAsync(eventDto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var eventDto = await _eventService.GetByIdAsync(id);
            if (eventDto == null) return NotFound();

            return View(eventDto);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventDTO eventDto)
        {
            if (id != eventDto.Id) return BadRequest();

            if (!ModelState.IsValid)
                return View(eventDto);

            await _eventService.UpdateAsync(eventDto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var eventDto = await _eventService.GetByIdAsync(id);
            if (eventDto == null) return NotFound();

            return View(eventDto);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var eventDto = await _eventService.GetByIdAsync(id);
            if (eventDto == null) return NotFound();

            return View(eventDto);
        }
    }