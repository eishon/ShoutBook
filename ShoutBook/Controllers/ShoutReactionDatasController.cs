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
    public class ShoutReactionDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoutReactionDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoutReactionDatas
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShoutReactionData.ToListAsync());
        }

        // GET: ShoutReactionDatas/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoutReactionData = await _context.ShoutReactionData
                .FirstOrDefaultAsync(m => m.ShoutID == id);
            if (shoutReactionData == null)
            {
                return NotFound();
            }

            return View(shoutReactionData);
        }

        // GET: ShoutReactionDatas/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoutReactionDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoutID,UserID,Reaction")] ShoutReactionData shoutReactionData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoutReactionData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoutReactionData);
        }

        // GET: ShoutReactionDatas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoutReactionData = await _context.ShoutReactionData.FindAsync(id);
            if (shoutReactionData == null)
            {
                return NotFound();
            }
            return View(shoutReactionData);
        }

        // POST: ShoutReactionDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShoutID,UserID,Reaction")] ShoutReactionData shoutReactionData)
        {
            if (id != shoutReactionData.ShoutID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoutReactionData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoutReactionDataExists(shoutReactionData.ShoutID))
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
            return View(shoutReactionData);
        }

        // GET: ShoutReactionDatas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoutReactionData = await _context.ShoutReactionData
                .FirstOrDefaultAsync(m => m.ShoutID == id);
            if (shoutReactionData == null)
            {
                return NotFound();
            }

            return View(shoutReactionData);
        }

        // POST: ShoutReactionDatas/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoutReactionData = await _context.ShoutReactionData.FindAsync(id);
            _context.ShoutReactionData.Remove(shoutReactionData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoutReactionDataExists(int id)
        {
            return _context.ShoutReactionData.Any(e => e.ShoutID == id);
        }
    }
}
