using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Models;

namespace WebApp3.ViewModels
{
    public class LikeItViewModel
    {
        public string CommentContent { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
        public int FanficId { get; set; }
        public Fanfic Fanfics { get; set; }
    }
}
