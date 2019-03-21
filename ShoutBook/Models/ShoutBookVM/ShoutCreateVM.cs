using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoutBook.Data;

namespace ShoutBook.Models.ShoutBookVM
{
    public class ShoutCreateVM
    {
        ApplicationDbContext _context;

        public ShoutCreateVM()
        {
            _context = null;
        }

        public ShoutCreateVM(ApplicationDbContext context)
        {
            _context = context;
            Initialize();
        }

        [Required]
        [Display(Name = "Shout Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Shout")]
        [StringLength(512, MinimumLength = 1)]
        public string Data { get; set; }

        //[Required]
        //[Display(Name = "Location")]
        //public string Location { get; set; }

        public IEnumerable<SelectListItem> AllShoutType { get; set; }
        //public IEnumerable<SelectListItem> AllLocationType { get; set; }

        void Initialize()
        {
            using (var context = _context)
            {
                List<SelectListItem> shoutTypeList = context.ShoutType.AsNoTracking()
                    //.OrderBy(n => n.Name)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Name,
                            Text = n.Name
                        }).ToList();

                /*List<SelectListItem> locationList = context.LocationType.AsNoTracking()
                    .OrderBy(n => n.Loaction)
                        .Select(n =>
                            new SelectListItem
                            {
                                Value = n.ID.ToString(),
                                Text = n.Loaction
                            }).ToList();*/

                AllShoutType = new SelectList(shoutTypeList, "Value", "Text");
                //AllLocationType = new SelectList(locationList, "Value", "Text");
            }
        }
    }
}
