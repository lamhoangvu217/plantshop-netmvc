using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace ElectroShop.Models
{
    [Table("VoteLog")]
    public class MVoteLog
    {
        [Key]
        public int id { get; set; }

        public int VoteForId { get; set; }
        public Int16 SectionId { get; set; }
        public String UserName { get; set; }
        public Int16 Vote { get; set; }
        public bool Active { get; set; }

    }
}