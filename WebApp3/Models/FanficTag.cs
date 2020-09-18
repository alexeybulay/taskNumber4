using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.Models
{
    public class FanficTag
    {
        [Key]
        public int FanficId { get; set; }
       
        public Fanfic Fanfic { get; set; }
        [Key]
        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
