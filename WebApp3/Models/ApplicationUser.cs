using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebApp3.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; } 
        [PersonalData]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset LastTimeLogin { get; set; }
        [PersonalData]
        [Column(TypeName = "bit")]
        public bool IsBlocked { get; set; }

    }
}