using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoutBook.Data;
using ShoutBook.Models.ShoutBookVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ShoutBook.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<UserRoleVM> AllUserRolesData = new List<UserRoleVM>();

            var leftJoin = from p in _context.Users
                        join r in _context.UserRoles 
                        on p.Id equals r.UserId
                        into temp
                        from r in temp.DefaultIfEmpty()
                        select new {
                            id = p.Id,
                            username = p.UserName,
                            urole = r.RoleId
                        };

            var rightJoin = from r in _context.UserRoles
                            join p in _context.Users
                           on r.UserId equals p.Id
                           into temp
                           from p in temp.DefaultIfEmpty()
                           select new
                           {
                               id = p.Id,
                               username = p.UserName,
                               urole = r.RoleId
                           };

            var query = leftJoin.Union(rightJoin);

            foreach (var v in query)
            {
                if (string.IsNullOrEmpty(v.urole))
                {
                    CreateRole(v.id, "User");
                }

                UserRoleVM temp = new UserRoleVM
                {
                    ID = v.id,
                    UName = v.username,
                    URole = v.urole
                };

                AllUserRolesData.Add(temp);
            }

            return View(AllUserRolesData);
        }

        [Authorize(Roles = "Admin")]
        public void CreateRole(string id, string role)
        {
            //IdentityUserRole identityRole = new IdentityUserRole
            //{
            //    UserId = id,
            //    RoleId = role
            //};

            //_context.UserRoles.Add(identityRole);
        }

        /*
        // GET: Shouts/Details/5
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
        public IActionResult Create()
        {
            //return View();

            var shoutCreateVM = new ShoutCreateVM(this._context);
            return View(shoutCreateVM);
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
        
        // POST: Shouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        }*/
    }
}