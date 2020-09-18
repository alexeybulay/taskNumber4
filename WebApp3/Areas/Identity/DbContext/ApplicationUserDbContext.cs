using System.Net.Mime;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Areas.Identity.DbContext
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Fanfic> Fanfics { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<FanficTag> FanficsTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
       public DbSet<Fandom> Fandoms { get; set; }  
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // One User -> Many Fanfics
            builder.Entity<Fanfic>()
                .HasOne(e => e.ApplicationUser)
                .WithMany(e => e.Fanfics)
                .HasForeignKey(fk => fk.ApplicationUserId);

            // One User -> Many Comments
            builder.Entity<Comment>()
                .HasOne(e => e.ApplicationUsers)
                .WithMany(e => e.Comments)
                .HasForeignKey(fk => fk.ApplicationUserId);


            //One Fanfic -> Many Likes
            builder.Entity<Like>()
                .HasOne(e => e.Fanfics)
                .WithMany(e => e.Likes)
                .HasForeignKey(fk => fk.FanficId);
            // One User -> Many Likes


            // One Fanfic -> Many Comments
            builder.Entity<Like>()
                .HasOne(e => e.Fanfics)
                .WithMany(e => e.Likes)
                .HasForeignKey(fk => fk.FanficId);      
            
            // One Fanfic -> Many Chapters
            builder.Entity<Chapter>()
                .HasOne(e => e.Fanfics)
                .WithMany(e => e.Chapters)
                .HasForeignKey(fk => fk.FanficId);


            //  One Category -> Many Fanfics
            builder.Entity<Fanfic>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Fanfics)
                .HasForeignKey(fk => fk.CategoryId);

            //  One Fandom -> Many Fanfics
            builder.Entity<Fanfic>()
                .HasOne(e => e.Fandoms)
                .WithMany(e => e.Fanfics)
                .HasForeignKey(fk => fk.FandomId);

            // Many Fanfics -> Many Tags
            builder.Entity<FanficTag>()
                .HasKey(key => new {key.FanficId, key.TagId});
            //  builder.Entity<Fanfic>()
            //      .HasKey(key => key.FanficId);
            //  builder.Entity<Tag>()
            //      .HasKey(key => key.TagId);

            builder.Entity<FanficTag>()
                .HasOne(e => e.Fanfic)
                .WithMany(e => e.FanficsTags)
                .HasForeignKey(e => e.FanficId);

            builder.Entity<FanficTag>()
                .HasOne(e => e.Tag)
                .WithMany(e => e.FanficsTags)
                .HasForeignKey(e => e.TagId);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
