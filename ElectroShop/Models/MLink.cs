namespace ElectroShop
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

    [Table("Links")]
    public class MLink
    {
        [Key]

        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        //Type = Category,Topic,Page
        public string Type { get; set; }
        public int? TableId { get; set; }
        
    }
}