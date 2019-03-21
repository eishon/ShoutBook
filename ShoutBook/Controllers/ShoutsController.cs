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
    public class ShoutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shouts
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var shouts = from m in _context.Shout
                         select m;

            shouts = shouts.OrderByDescending(n => n.Time);

            return View(await shouts.ToListAsync());
        }

        // GET: Shouts
        [Authorize]
        public async Task<IActionResult> MyShout(string searchString)
        {

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);

            var shouts = _context.Shout.Where(m => m.ShoutByID.Equals(user.Id)).OrderByDescending(m => m.Time);

            //shouts = shouts.OrderByDescending(n => n.Time);

            return View(await shouts.ToListAsync());
        }

        // GET: Shouts/Search
        [Authorize]
        public async Task<IActionResult> Search(string searchString)
        {
            var shouts = from m in _context.Shout
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                shouts = shouts.Where(s => s.Data.Contains(searchString));
            }

            shouts = shouts.OrderByDescending(n => n.Time);

            return View(await shouts.ToListAsync());
        }

        // GET: Shouts/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shout = await _context.Shout
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shout == null)
            {
                return NotFound();
            }

            return View(shout);
        }

        // GET: Shouts/Create
        [Authorize]
        public IActionResult Create()
        {
            //return View();

            var shoutCreateVM = new ShoutCreateVM(this._context);
            return View(shoutCreateVM);
        }

        [Authorize]
        public async Task UpdateShoutReaction(int id, int reaction)
        {
            var shout = await _context.Shout.FindAsync(id);

            if (reaction == 1)
            {
                shout.Vote = shout.Vote + 1;
            }
            else if (reaction == 2)
            {
                shout.Reject = shout.Reject + 1;

                try
                {
                    _context.Update(shout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
        }

        [Authorize]
        public async Task Vote(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
            var shoutReactions = _context.ShoutReactionData
                                        .Where(m => m.ShoutID == id)
                                        .Where(m => m.UserID == user.Id);
            
            if (shoutReactions == null)
            {
                await UpdateShoutReaction(id, 1);
                ShoutReactionData t = new ShoutReactionData
                {
                    ShoutID = id,
                    UserID = user.Id,
                    Reaction = 1
                };

                _context.Add(t);
                await _context.SaveChangesAsync();
            }
            else
            {
                foreach(var shoutReaction in shoutReactions)
                {
                    if (shoutReaction.Reaction != 1 && shoutReaction.UserID == user.Id)
                    {
                        await UpdateShoutReaction(shoutReaction.ShoutID, 1);
                        ShoutReactionData t = new ShoutReactionData
                        {
                            ShoutID = id,
                            UserID = user.Id,
                            Reaction = 1
                        };

                        try
                        {
                            _context.Update(t);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {

                        }
                    }
                }
            }

            //return RedirectToAction("Index", "Shouts");
        }

        [Authorize]
        public async Task Reject(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
            var shoutReactions = _context.ShoutReactionData
                                        .Where(m => m.ShoutID == id)
                                        .Where(m => m.UserID == user.Id);

            if (shoutReactions == null)
            {
                await UpdateShoutReaction(id, 2);
                ShoutReactionData t = new ShoutReactionData
                {
                    ShoutID = id,
                    UserID = user.Id,
                    Reaction = 2
                };

                _context.Add(t);
                await _context.SaveChangesAsync();
            }
            else
            {
                foreach (var shoutReaction in shoutReactions)
                {
                    if (shoutReaction.Reaction != 2 && shoutReaction.UserID == user.Id)
                    {
                        await UpdateShoutReaction(shoutReaction.ShoutID, 2);
                        ShoutReactionData t = new ShoutReactionData
                        {
                            ShoutID = id,
                            UserID = user.Id,
                            Reaction = 2
                        };

                        try
                        {
                            _context.Update(t);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {

                        }
                    }
                }
            }

            //return RedirectToAction("Index", "Shouts");
        }

        /*
         // POST: Shouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Type,Data,Location,ShoutBy,ShoutByID,Vote,Reject,Time,Attach,Image")] Shout shout)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
                shout.ShoutBy = user.UserName;
                shout.ShoutByID = user.Id;
                var user2 = await _context.UserData.FirstOrDefaultAsync(m => m.UserID == shout.ShoutByID);
                shout.Location = user2.Location;
                shout.Vote = 0;
                shout.Reject = 0;
                shout.Time = DateTime.Now;
                if (string.IsNullOrEmpty(shout.Attach)) shout.Attach = "None";
                if (string.IsNullOrEmpty(shout.Image)) shout.Image = "None";

                _context.Add(shout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shout);
        }
        */

        // POST: Shouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Data")] ShoutCreateVM shoutCreateVM)
        {
            if (ModelState.IsValid)
            {
                Shout shout = new Shout()
                {
                    Type = shoutCreateVM.Type,
                    Data = shoutCreateVM.Data,
                    Vote = 0,
                    Reject = 0,
                    Time = DateTime.Now
                };

                var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
                shout.ShoutBy = user.UserName;
                shout.ShoutByID = user.Id;
                var user2 = await _context.UserData.FirstOrDefaultAsync(m => m.UserID == shout.ShoutByID);
                shout.Location = user2.Location;
                if (string.IsNullOrEmpty(shout.Attach)) shout.Attach = "None";
                if (string.IsNullOrEmpty(shout.Image)) shout.Image = "None";

                _context.Add(shout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoutCreateVM);
        }

        // GET: Shouts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shout = await _context.Shout.FindAsync(id);
            if (shout == null)
            {
                return NotFound();
            }
            return View(shout);
        }

        // POST: Shouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Type,Data,Location,ShoutBy,ShoutByID,Vote,Reject,Time,Attach,Image")] Shout shout)
        {
            if (id != shout.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoutExists(shout.ID))
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
            return View(shout);
        }

        // GET: Shouts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shout = await _context.Shout
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shout == null)
            {
                return NotFound();
            }

            return View(shout);
        }

        // POST: Shouts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shout = await _context.Shout.FindAsync(id);
            _context.Shout.Remove(shout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoutExists(int id)
        {
            return _context.Shout.Any(e => e.ID == id);
        }
    }
}
