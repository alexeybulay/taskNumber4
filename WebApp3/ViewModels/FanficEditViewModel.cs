using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp3.ViewModels
{
    public class FanficEditViewModel
    {
        public int FanficId { get; set; }
        public string NameFanfic { get; set; }
        [Column(TypeName = "nvarchar(2000)")]
        public string Description { get; set; }
        public List<SelectListItem> Tags { get; set; } = new List<SelectListItem>();
        public string[] SelectedTags { get; set; }
    }
}
