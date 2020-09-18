using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Models;

namespace WebApp3.ViewModels
{
    public class ApplicationUserFanficViewModel
    {
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
        public IEnumerable<Fanfic> Fanfics { get; set; } = new List<Fanfic>();
    }
}
