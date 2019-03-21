using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShoutBook.Models.ShoutBookVM
{
    public class UserRoleVM
    {
        public string ID { get; set; }
        [DisplayName("User")]
        public string UName { get; set; }
        [DisplayName("Role")]
        public string URole { get; set; }
    }
}
