using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoutBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutBook.Models
{
    public class Beacon
    {
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public int? Seen { get; set; }
    }
}
