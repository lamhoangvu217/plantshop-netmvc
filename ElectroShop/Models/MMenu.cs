namespace ElectroShop.Models
{

    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

 
   [Table("Menus")]
    public class MMenu
    {
        [Key]

        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public int? TableId { get; set; }
        public int? ParentId { get; set; }
        public string Position { get; set; }
        public int Orders { get; set; }
        public DateTime? Created_at { get; set; }
        public int? Created_by { get; set; }
        public DateTime? Updated_at { get; set; }
        public int? Updated_by { get; set; }
        public int Status { get; set; }
    }
}