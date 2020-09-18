using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Models;

namespace WebApp3.ViewModels
{
    public class CreateChapterViewModel
    {
        public int ChapterId { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int FanficId { get; set; }
        public Fanfic Fanfics { get; set; }
        public string NameChapter { get; set; }
        public string TextChapter { get; set; }
        public DateTime DateTimePuplish { get; set; } = DateTime.Now;
        public IEnumerable<Chapter> Chapters { get; set; } = new List<Chapter>();
    }
}
