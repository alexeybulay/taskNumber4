using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public string CommentContent { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
        public int FanficId { get; set; }
        public Fanfic Fanfics { get; set; }
    }
}
