using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebApp3.Models;

namespace WebApp3.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Name { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string ImageUrl { get; set; }
    }
}
