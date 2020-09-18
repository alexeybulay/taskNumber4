using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp3.Views.Home
{
    public class UsersListModel : PageModel
    {
        public string SearchTerm { get; set; }
        public void OnGet()
        {

        }
    }
}
