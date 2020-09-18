using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace WebApp3.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public sealed class ApplicationUser : IdentityUser
    {
        [AllowNull]
        public string ImageUrl { get; set; }

        //[PersonalData]
        //[Column(TypeName = "nvarchar(100)")]
        //public string Login { get; set; }
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
        [PersonalData]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; } 
        public string AboutMe{get;set;}
        public List<Fanfic> Fanfics { get; set; } = new List<Fanfic>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Like> Likes { get; set; } = new List<Like>();

    }

}