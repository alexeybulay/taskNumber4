using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagTitle { get; set; }
        public string TagDescription { get; set; }
        public List<FanficTag> FanficsTags { get; set; } = new List<FanficTag>(); 
    }
}
