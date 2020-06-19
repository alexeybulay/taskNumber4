using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationUserDbContext _aplApplicationUserDbContext;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<ApplicationUser> signInManager,
            ApplicationUserDbContext aplApplicationUserDbContext, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _aplApplicationUserDbContext = aplApplicationUserDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (user.IsBlocked == true)
                {
                    return View("AccountBlockInformation");
                }
                return View();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            if(User.Identity.IsAuthenticated) 
                return View(_userManager.Users.ToList());
            return Redirect("/Identity/Account/Login");
        }
        public IActionResult Login()
        {
            return Redirect("/Identity/Account/Login");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "Home");
        }

        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            IdentityResult result = await _userManager.DeleteAsync(user);
            return RedirectToAction("Logout", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user,
                        changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        await _signInManager.RefreshSignInAsync(user);
                        return View("ChangePasswordConfirmation");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(changePasswordViewModel);
            }
        }
        public async Task<IActionResult> BlockOrUnBlockAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if (user.IsBlocked == true)
                {
                    user.IsBlocked = false;
                   await _userManager.UpdateAsync(user);
                   return RedirectToAction("Logout","Home");
                }
                else
                {
                    user.IsBlocked = true;
                   await  _userManager.UpdateAsync(user);
                   return RedirectToAction("Logout", "Home");
                }
            }            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
