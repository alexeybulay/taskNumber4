using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.ViewModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Не указана почта")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")] public bool RememberMe { get; set; } = false;

        public string ReturnUrl { get; set; }
    }
}
