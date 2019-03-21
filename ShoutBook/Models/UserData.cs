using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShoutBook.Data;
using Microsoft.EntityFrameworkCore;

namespace ShoutBook.Models
{
    public class UserData
    {
        [Key]
        public string UserID { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
    }
}
