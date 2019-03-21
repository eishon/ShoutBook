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
using ShoutBook.Models.ShoutBookVM;

namespace ShoutBook.Controllers
{
    public class BeaconsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeaconsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Beacons
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
            var received = _context.Beacon.Where(m => m.To.Equals(user.UserName)).OrderByDescending(m => m.Time);

            return View(await received.ToListAsync());
        }

        // GET: Beacons
        [Authorize]
        public async Task<IActionResult> Sent()
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
            var received = _context.Beacon.Where(m => m.From.Equals(user.UserName)).OrderByDescending(m => m.Time);

            return View(await received.ToListAsync());
        }

        // GET: Beacons/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beacon = await _context.Beacon
                .FirstOrDefaultAsync(m => m.ID == id);
            if (beacon == null)
            {
                return NotFound();
            }

            if (beacon.To.Equals(User.Identity.Name) && beacon.Seen == 0)
            {
                beacon.Seen = 1;
                try
                {
                    _context.Update(beacon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeaconExists(beacon.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(beacon);
        }

        // GET: Beacons/Create
        [Authorize]
        public IActionResult Create()
        {
            var beacon = new CreateBeaconVM(this._context);
            return View(beacon);
        }

        // POST: Beacons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Time,From,To,Message,Seen")] CreateBeaconVM beaconVM)
        {
            if (ModelState.IsValid)
            {
                Beacon beacon = new Beacon
                {
                    ID = beaconVM.ID,
                    Time = DateTime.Now,
                    From = User.Identity.Name,
                    To = beaconVM.To,
                    Message = beaconVM.Message,
                    Seen = 0
                };

                _context.Add(beacon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beaconVM);
        }


        /*
        // GET: Beacons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beacon = await _context.Beacon.FindAsync(id);
            if (beacon == null)
            {
                return NotFound();
            }
            return View(beacon);
        }

        // POST: Beacons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Time,From,To,Message,Seen")] Beacon beacon)
        {
            if (id != beacon.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beacon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeaconExists(beacon.ID))
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
            return View(beacon);
        }
        */

        // GET: Beacons/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beacon = await _context.Beacon
                .FirstOrDefaultAsync(m => m.ID == id);
            if (beacon == null)
            {
                return NotFound();
            }

            return View(beacon);
        }

        // POST: Beacons/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beacon = await _context.Beacon.FindAsync(id);
            _context.Beacon.Remove(beacon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeaconExists(int id)
        {
            return _context.Beacon.Any(e => e.ID == id);
        }
    }
}
