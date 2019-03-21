using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoutBook.Data;

namespace ShoutBook.Models.ShoutBookVM
{
    public class SetLocationVM
    {
        ApplicationDbContext _context;

        public SetLocationVM()
        {
            _context = null;
        }

        public SetLocationVM(ApplicationDbContext con)
        {
            _context = con;
            Initialize();
        }
        
        public string UserID { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }

        public IEnumerable<SelectListItem> AllLocation { get; set; }

        void Initialize()
        {
            using (var context = _context)
            {
                List<SelectListItem> locationList = context.LocationType.AsNoTracking()
                    //.OrderBy(n => n.Loaction)
                        .Select(n =>
                            new SelectListItem
                            {
                                Value = n.Loaction,
                                Text = n.Loaction
                            }).ToList();

                AllLocation = new SelectList(locationList, "Value", "Text");
            }
        }
    }
}
