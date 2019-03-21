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
    public class UserDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserDatas
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserData.ToListAsync());
        }

        // GET: UserDatas/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserData
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // GET: UserDatas/Create
        [Authorize]
        public IActionResult Create()
        {
            var userData = new SetLocationVM(this._context);
            return View(userData);
        }

        /*
        // POST: UserDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Image,Location")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
                userData.UserID = user.Id;
                if (string.IsNullOrEmpty(userData.Image)) userData.Image = "None";
                if (string.IsNullOrEmpty(userData.Location)) userData.Location = "None";

                _context.Add(userData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userData);
        }*/

        // POST: UserDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Image,Location")] SetLocationVM setLocationVM)
        {
            if (ModelState.IsValid)
            {
                var userData = new UserData();
                var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
                userData.UserID = user.Id;
                if (string.IsNullOrEmpty(userData.Image)) userData.Image = "None";

                if (string.IsNullOrEmpty(setLocationVM.Location)) userData.Location = "Dhaka";
                else userData.Location = setLocationVM.Location;

                _context.Add(userData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Shouts");
            }
            return View(setLocationVM);
        }

        // GET: UserDatas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData2 = await _context.UserData.FindAsync(id);
            var userData = new SetLocationVM(this._context);

            if (userData2 == null)
            {
                return RedirectToAction("Create", "UserDatas");
            }
            else
            {
                userData.UserID = userData2.UserID;
                userData.Image = userData2.Image;
                userData.Location = userData2.Location;

                return View(userData);
            }
        }

        /*
        // POST: UserDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserID,Image,Location")] UserData userData)
        {
            if (id != userData.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
                    userData.UserID = user.Id;

                    _context.Update(userData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataExists(userData.UserID))
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
            return View(userData);
        }*/

        // POST: UserDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserID,Image,Location")] SetLocationVM setLocationVM)
        {
            if (id != setLocationVM.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userData = new UserData();
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
                    setLocationVM.UserID = user.Id;

                    userData.UserID = setLocationVM.UserID;
                    userData.Image = setLocationVM.Image;
                    userData.Location = setLocationVM.Location;

                    _context.Update(userData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataExists(userData.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Shouts");
            }
            return View(setLocationVM);
        }

        // GET: UserDatas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserData
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // POST: UserDatas/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userData = await _context.UserData.FindAsync(id);
            _context.UserData.Remove(userData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDataExists(string id)
        {
            return _context.UserData.Any(e => e.UserID == id);
        }
    }
}
