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
    public class ShoutTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoutTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoutTypes
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShoutType.ToListAsync());
        }

        // GET: ShoutTypes/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoutType = await _context.ShoutType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shoutType == null)
            {
                return NotFound();
            }

            return View(shoutType);
        }

        // GET: ShoutTypes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoutTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] ShoutType shoutType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoutType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoutType);
        }

        // GET: ShoutTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoutType = await _context.ShoutType.FindAsync(id);
            if (shoutType == null)
            {
                return NotFound();
            }
            return View(shoutType);
        }

        // POST: ShoutTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] ShoutType shoutType)
        {
            if (id != shoutType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoutType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoutTypeExists(shoutType.ID))
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
            return View(shoutType);
        }

        // GET: ShoutTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoutType = await _context.ShoutType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shoutType == null)
            {
                return NotFound();
            }

            return View(shoutType);
        }

        // POST: ShoutTypes/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoutType = await _context.ShoutType.FindAsync(id);
            _context.ShoutType.Remove(shoutType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoutTypeExists(int id)
        {
            return _context.ShoutType.Any(e => e.ID == id);
        }
    }
}
