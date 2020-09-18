using Microsoft.EntityFrameworkCore;
using WebApp3.Models;

namespace WebApp3.Areas.Identity.DbContext
{
    public class FanficDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Fanfic> Fanfics { get; set; }
        public FanficDbContext() 
        {
            Database.EnsureCreated();
        }
    }
}
