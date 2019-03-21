using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoutBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutBook.Models.ShoutBookVM
{
    public class CreateBeaconVM
    {
        ApplicationDbContext _context;

        public CreateBeaconVM()
        {
            _context = null;
        }

        public CreateBeaconVM(ApplicationDbContext con)
        {
            _context = con;
            Initialize();
        }
        
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public int? Seen { get; set; }

        public IEnumerable<SelectListItem> AllUsers { get; set; }

        void Initialize()
        {
            using (var context = _context)
            {
                List<SelectListItem> userList = context.Users.AsNoTracking()
                        //.OrderBy(n => n.Loaction)
                        .Select(n =>
                            new SelectListItem
                            {
                                Value = n.UserName,
                                Text = n.UserName
                            }).ToList();

                AllUsers = new SelectList(userList, "Value", "Text");
            }
        }
    }
}
