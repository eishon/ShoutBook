using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoutBook.Data;
using ShoutBook.Models;

namespace ShoutBook.Controllers
{
    public class LocationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LocationTypes
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocationType.ToListAsync());
        }

        // GET: LocationTypes/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationType = await _context.LocationType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (locationType == null)
            {
                return NotFound();
            }

            return View(locationType);
        }

        // GET: LocationTypes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Loaction")] LocationType locationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationType);
        }

        // GET: LocationTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationType = await _context.LocationType.FindAsync(id);
            if (locationType == null)
            {
                return NotFound();
            }
            return View(locationType);
        }

        // POST: LocationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Loaction")] LocationType locationType)
        {
            if (id != locationType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationTypeExists(locationType.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(locationType);
        }

        // GET: LocationTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationType = await _context.LocationType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (locationType == null)
            {
                return NotFound();
            }

            return View(locationType);
        }

        // POST: LocationTypes/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationType = await _context.LocationType.FindAsync(id);
            _context.LocationType.Remove(locationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationTypeExists(int id)
        {
            return _context.LocationType.Any(e => e.ID == id);
        }
    }
}
