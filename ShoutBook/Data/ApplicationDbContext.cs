using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoutBook.Models;

namespace ShoutBook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ShoutBook.Models.Shout> Shout { get; set; }
        public DbSet<ShoutBook.Models.ShoutType> ShoutType { get; set; }
        public DbSet<ShoutBook.Models.ShoutReactionData> ShoutReactionData { get; set; }
        public DbSet<ShoutBook.Models.UserData> UserData { get; set; }
        public DbSet<ShoutBook.Models.LocationType> LocationType { get; set; }
        public DbSet<ShoutBook.Models.Beacon> Beacon { get; set; }
    }
}
