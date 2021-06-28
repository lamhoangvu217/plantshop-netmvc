namespace ElectroShop.Models
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;



    [Table("Posts")]
    public class MPost
    {
        [Key]
        public int Id { get; set; }
        public int Topid { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Detail { get; set; }
        public string Img { get; set; }
        public string Type { get; set; }
        public string MetaKey { get; set; }
        public string MetaDesc { get; set; }
        public DateTime Created_At { get; set; }
        public int? Created_By { get; set; }
        public DateTime Updated_At { get; set; }
        public int? Updated_By { get; set; }
        public int Status { get; set; }
    }
}