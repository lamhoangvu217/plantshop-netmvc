namespace ElectroShop.Models
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


    
    [Table("Contacts")]
    public class MContact
    {
        public int Id { get; set; }

        [Required]
        
        public string FullName { get; set; }

        [Required]

        public string Email { get; set; }

        [Required]

        public int Phone { get; set; }

        [Required]

        public string Title { get; set; }
        [Required]
        public string Detail { get; set; }
        public int Flag { get; set; }
        public string Reply { get; set; }
        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public int? Updated_by { get; set; }

        public int? Status { get; set; }
    }
}