using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp3.Models;

namespace WebApp3.Views.Home
{
    public class UserInformationModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "Моя aстраница";
        }

    }
}
