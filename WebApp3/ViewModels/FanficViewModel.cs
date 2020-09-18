using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp3.Interfaces;
using WebApp3.Models;

namespace WebApp3.ViewModels
{
    public class FanficViewModel
    {
        public int FanficId { get; set; }
        public string NameFanfic { get; set; }
        public string Description { get; set; }
        public DateTime DateTimePublish { get; set; } = DateTime.Now;
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public List<FanficTag> Tags { get; set; }
        public IEnumerable<Fanfic> Fanfics { get; set; }
        public IEnumerable<FanficTag> FanficsTags { get; set; }
    }
}
