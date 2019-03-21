using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutBook.Models
{
    public class ShoutReactionData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int ShoutID { get; set; }
        public string UserID { get; set; }
        public int Reaction { get; set; }
    }
}
