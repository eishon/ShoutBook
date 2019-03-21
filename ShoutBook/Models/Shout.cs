using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutBook.Models
{
    public class Shout
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public string Location { get; set; }
        public string ShoutBy { get; set; }
        public string ShoutByID { get; set; }
        public int? Vote { get; set; }
        public int? Reject { get; set; }
        public DateTime? Time { get; set; }
        public string Attach { get; set; }
        public string Image { get; set; }
    }
}
