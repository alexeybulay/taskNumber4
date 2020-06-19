using System.Net.Mime;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp3.Models;

namespace WebApp3.Areas.Identity.DbContext
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
