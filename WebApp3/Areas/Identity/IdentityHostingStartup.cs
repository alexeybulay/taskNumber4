using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Data;
using WebApp3.Models;

[assembly: HostingStartup(typeof(WebApp3.Areas.Identity.IdentityHostingStartup))]
namespace WebApp3.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationUserDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WebApp3ContextConnection")));
        services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
            })
                    .AddEntityFrameworkStores<ApplicationUserDbContext>();
        
            });
        }
    }
}