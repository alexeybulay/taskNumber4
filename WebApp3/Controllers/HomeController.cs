using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit.Text;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Helpers;
using WebApp3.Interfaces;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationUserDbContext _applicationUserDbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        [BindProperty(SupportsGet = true)]
        public string SearchUser { get; set; }
        public HomeController(ILogger<HomeController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            ApplicationUserDbContext applicationUserDbContext, IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _applicationUserDbContext = applicationUserDbContext;
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
      
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (user.IsBlocked == true)
                {
                    return View("AccountBlockInformation");
                }
                return View(await _unitOfWork.Fanfic.GetAllAsync());
            }

            return View(await _unitOfWork.Fanfic.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> UsersList(int page = 1)
        {
            IQueryable<ApplicationUser> users = _applicationUserDbContext.ApplicationUsers.Include(p => p.Fanfics);
            int pageSize = 12;
            int countElement = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PaginationPageViewModel paginationPageViewModel = new PaginationPageViewModel(countElement, page, pageSize);
            FanficTagViewModel fanficTagViewModel = new FanficTagViewModel()
            {
                PaginationPageViewModel = paginationPageViewModel,
                ApplicationUsers = items
            };

            return View(fanficTagViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmEmail(string email = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, String.Empty);
                return Json(new {isValid = false, textMessage = "Данной почты не существует!"});
            }

            if (user.EmailConfirmed)
            {
                ModelState.AddModelError(String.Empty, String.Empty);
                return Json(new { isValid = false, textMessage = "Данная почта уже подтверждена!" });
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var returnUrl = Url.Action("ConfirmEmail", "Home", new {userId = user.Id, code = code}, HttpContext.Request.Scheme);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(user.Email, "Подтверждение почты",
                $"Для подтверждения почты перейдите по <a href='{returnUrl}'>ссылке</a>");
            return Json(new {isValid = true});
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string userId, string code = null)
        {
            var user = await _userManager.FindByIdAsync(userId);
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel();
            resetPasswordViewModel.Email = user.Email;
            return code == null ? View("Error") : View(resetPasswordViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "Повторите попытку!");
                return View(resetPasswordViewModel);
            }

            if (!resetPasswordViewModel.Password.Equals(resetPasswordViewModel.ConfirmPassword))
            {
                ModelState.AddModelError(String.Empty, "Пароли не совпадают");
                return View(resetPasswordViewModel);

            }

            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
               var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code,
                    resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction("UsersList", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> SendResetPassword(string email = null)
        {
            Guid securityCode = new Guid();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, String.Empty);
                return Json(new { isValid = false, textMessage = "Данной почты не существует!" });
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(String.Empty, String.Empty);
                return Json(new { isValid = false, textMessage = "Данная почта не подтверждена!" });
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var returnUrl = Url.Action("ResetPassword", "Home", new { userId = user.Id, code = code }, HttpContext.Request.Scheme);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(user.Email, "Сброс пароля",
                $"Для подтверждения почты перейдите по {returnUrl}");
            return Json(new { isValid = true, secureCode = securityCode });
        }
        [HttpGet]
        [NoDirectAccessAttribute]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    ImageUrl = registerViewModel.ImageUrl,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    LockoutEnd = DateTimeOffset.Now,
                    IsBlocked = false
                };
            
          
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var returnUrl = Url.Action("ConfirmEmail", "Home", new {userId = user.Id, code = code}, protocol: HttpContext.Request.Scheme);
                    
                    //user.LastTimeLogin = DateTimeOffset.Now;
                    //user.IsActive = true;
                    //await _userManager.UpdateAsync(user);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(registerViewModel.Email, "Подтверждение почты", $"Для подтверждения почты перейдите по <a href='{returnUrl}'>ссылке</a>");
                    return Json(new {isValid = true});

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Json(new
                {isValid = false, html = HelperForJSON.RenderRazorViewToString(this, "Register", registerViewModel)});
        }

        //GET: Home/Login
        [HttpGet]
        [NoDirectAccessAttribute]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string returnUrl,LoginViewModel model)
        {
            string errorMessage = null;
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    errorMessage = "Указанный пользователь не найден!";
                    ModelState.AddModelError(String.Empty, String.Empty);
                    return Json(new { isValid = false, textMessage = errorMessage, html = HelperForJSON.RenderRazorViewToString(this, "Login", model) });
                }
                if (user.IsBlocked == true)
                {
                    errorMessage = "Указанный пользователь не найден!";
                    ModelState.AddModelError(String.Empty, String.Empty);
                    return Json(new { isValid = false, textMessage = errorMessage, html = HelperForJSON.RenderRazorViewToString(this, "Login", model) });
                }

                //if (!user.EmailConfirmed)
                //{
                //    errorMessage = "Для входа в аккаунт необходимо подтверждение электронной почты!";
                //    ModelState.AddModelError(String.Empty, String.Empty);
                //    return Json(new { isValid = false, textMessage = errorMessage, html = HelperForJSON.RenderRazorViewToString(this, "Login", model) });
                //}
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    if (user.ImageUrl == null)
                    {
                        user.ImageUrl = "standart.jpg";
                    }
                    user.IsActive = true;
                    user.LastTimeLogin = DateTimeOffset.Now;
                    await _userManager.UpdateAsync(user);
                    return Json(new { isValid = true, html = HelperForJSON.RenderRazorViewToString(this, "Index", model) });

                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa",
                        new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    errorMessage = "Некорректный ввод логина!";
                    ModelState.AddModelError(String.Empty, String.Empty);
                    return Json(new { isValid = false, textMessage = errorMessage, html = HelperForJSON.RenderRazorViewToString(this, "Login", model) });
                }
            }


            // If we got this far, something failed, redisplay form
            return Json(new { isValid = false, html = HelperForJSON.RenderRazorViewToString(this, "Login", model) });
        }
        [HttpPost]
        [NoDirectAccessAttribute]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name); 
            user.IsActive = false; 
            await   _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            return Json(new { isValid = true });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            user.IsActive = false;
            await _signInManager.SignOutAsync();
            await _userManager.UpdateAsync(user);
            IdentityResult result = await _userManager.DeleteAsync(user);

            return RedirectToAction("UsersList", "Home");
        }

        [HttpGet]
        public IActionResult MyInformation(string id = null, string username = null)
        {
            if (!string.IsNullOrEmpty(id))
            {
                IEnumerable<ApplicationUser> users1 = _applicationUserDbContext.ApplicationUsers.Include(p => p.Fanfics)
                    .ThenInclude(n => n.Comments)
                        .Where(n => n.Id == id)
                        .ToList();
                    return View(users1);
            }
            if (!string.IsNullOrEmpty(username))
            {
                IEnumerable<ApplicationUser> users1 = _applicationUserDbContext.ApplicationUsers.Include(p => p.Fanfics)
                    .Where(n => n.Email == username)
                    .ToList();
                return View(users1);
            }
            return View();
        }

        [HttpGet]
        public IActionResult UserInformation(string id = null)
        {
           // var username = _userManager.FindByEmailAsync(User.Identity.Name);
            IEnumerable<ApplicationUser> users =  _applicationUserDbContext.ApplicationUsers.Include(p => p.Fanfics)
                .Where(n => n.Id == id)
                .ToList();
            return View(users);
        }
        [HttpGet]
        [NoDirectAccessAttribute]
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
                     //   user.IsActive = false;
                        await _userManager.UpdateAsync(user);
                        await _signInManager.RefreshSignInAsync(user);
                        return Json(new{isValid = true });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                    return Json(new { isValid = false, html = HelperForJSON.RenderRazorViewToString(this, "ChangePassword", changePasswordViewModel) });
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
        [HttpPost]
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
                    user.IsActive = false;
                    await  _userManager.UpdateAsync(user);
                   return RedirectToAction("Logout", "Home");
                }
            }            
        }
        [HttpGet]
        public IActionResult AddOrEditAboutMe()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> AddOrEditAboutMe(AddOrEditAboutMeViewModel addOrEditAboutMeViewModel)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            user.AboutMe = addOrEditAboutMeViewModel.AboutMe;
            await _userManager.UpdateAsync(user);
            return Json(new {isValid = true});
        }
          
        [HttpGet]
        public IActionResult ChangeNameProfile()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> ChangeNameProfile(ChangeNameProfileViewModel changeNameProfileViewModel)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (changeNameProfileViewModel.FirstName == null || changeNameProfileViewModel.LastName == null)
            {
                return Json(new { isValid = false, textMessage = "Поля не должны быть пустыми!", html = HelperForJSON.RenderRazorViewToString(this, "ChangeNameProfile", changeNameProfileViewModel) });
            }
            else
            { 
                user.FirstName = changeNameProfileViewModel.FirstName;
            user.LastName = changeNameProfileViewModel.LastName;
            await _userManager.UpdateAsync(user);
            return Json(new {isValid = true});
            }
        }

        [NoDirectAccessAttribute]
        public IActionResult ViewFanfic(ApplicationUserViewModel vm, int a)
        {
            var user = _userManager.FindByEmailAsync(User.Identity.Name);
            vm.ImageUrl = user.Result.ImageUrl;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> ViewFanfic(ApplicationUserViewModel vm)
        {
            string stringFileName = UploadFile(vm);
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            user.ImageUrl = stringFileName;
            _applicationUserDbContext.ApplicationUsers.Update(user);
            await _applicationUserDbContext.SaveChangesAsync();
            return Json(new {name = stringFileName});
        }

        private string UploadFile(ApplicationUserViewModel vm)
        {
            string fileName = null;
            if (vm.ProfileImage != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + '-' + vm.ProfileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.ProfileImage.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        public string Privacy()
        {
            return "Hello!";
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

