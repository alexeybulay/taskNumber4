using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.Models
{
    public class Fandom
    {
        public int FandomID { get; set; }          
        public string FandomName { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Fanfic> Fanfics { get; set; } = new List<Fanfic>();
    }
}
