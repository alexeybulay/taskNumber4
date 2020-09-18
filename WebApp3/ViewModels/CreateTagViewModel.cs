using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp3.ViewModels
{
    public class CreateTagViewModel
    {
        public string TagTitle { get; set; }
        public string TagDescription { get; set; }
        public DateTime DateTimePublish { get; set; } = DateTime.Now;
        public List<SelectListItem> Tags { get; set; }
        public string[] SelectedTags { get; set; }
    }
}
