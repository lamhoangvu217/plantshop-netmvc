namespace ElectroShop.Models
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


    [Table("Users")]
    public class MUser
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }//Giới tính
        public int Phone { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public int Access { get; set; }

        public int Status { get; set; }
        public DateTime Created_at { get; set; }
        public int Created_by { get; set; }
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }

    }
}