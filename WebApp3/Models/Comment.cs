using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public DateTime DateTimePublish { get; set; } = DateTime.Now;
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
        public int FanficId { get; set; }
        public Fanfic Fanfics { get; set; }
    }
}
