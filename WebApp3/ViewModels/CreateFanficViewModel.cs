using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Models;

namespace WebApp3.ViewModels
{
    public class CreateFanficViewModel
    {
        public int FanficId { get; set; }
        public string NameFanfic { get; set; }
        [Column(TypeName = "nvarchar(2000)")]
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Fandom Fandom { get; set; }
        public virtual Category Category { get; set; }
        public DateTime DateTimePublish { get; set; } = DateTime.Now;
        public IList<Fandom> Fandoms { get; set; }
        public List<Category> Categories { get; set; }
        public List<SelectListItem> Tags { get; set; } = new List<SelectListItem>();
        public string[] SelectedTags { get; set; }
        public int selectedFandom { get; set; }
        public int selectedCategory { get; set; }
    }
}
