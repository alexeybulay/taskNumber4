using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp3.Models;

namespace WebApp3.ViewModels
{
    public class FanficDetailsViewModel
    {
        public string NameFanfic { get; set; }
        [Column(TypeName = "nvarchar(2000)")]
        public string Description { get; set; }
        public string TagTitle { get; set; }
        public string TagDescription { get; set; }
        public DateTime DateTimePublish { get; set; } = DateTime.Now;
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public List<SelectListItem> Tags { get; set; }
        public string[] SelectedTags { get; set; }
    }
}
