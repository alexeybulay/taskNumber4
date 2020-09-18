using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApp3.ViewModels
{
    public class ChangeNameProfileViewModel
    {
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string LastName { get; set; }
    }
}
