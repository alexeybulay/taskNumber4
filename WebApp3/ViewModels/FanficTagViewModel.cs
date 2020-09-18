using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Interfaces;
using WebApp3.Models;

namespace WebApp3.ViewModels
{
    public class FanficTagViewModel
    {
        public PaginationPageViewModel PaginationPageViewModel { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; } 
        public IEnumerable<Fanfic> Fanfics { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Comment> Comments { get; set; } 
        public IEnumerable<Fandom> Fandoms { get; set; } 
        public IEnumerable<FanficTag> FanficsTags { get; set; } 
        public IQueryable<FanficTag> FanficsTagsQuerEnumerable { get; set; } 
        public int FanficId { get; set; }
        public string NameFanfic { get; set; }
        public string Description { get; set; }
        public DateTime DateTimePublish { get; set; } = DateTime.Now;
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Fanfic Fanfic { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public string CommentContent { get; set; }
    }
}
