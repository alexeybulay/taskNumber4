using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebApp3.Models
{
    public class Fanfic
    {
        [AllowNull]
        public string UlrImage { get; set; }
        public int FanficId { get; set; }
        public string NameFanfic { get; set; }
        [Column(TypeName = "nvarchar(2000)")] 
        public string Description { get; set; }
        public DateTime DateTimePublish { get; set; } = DateTime.Now;
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int FandomId { get; set; }
        public Fandom Fandoms { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<FanficTag> FanficsTags { get; set; } = new List<FanficTag>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Like> Likes { get; set; } = new List<Like>();
        public List<Chapter> Chapters { get; set; } = new List<Chapter>();



    }
}
