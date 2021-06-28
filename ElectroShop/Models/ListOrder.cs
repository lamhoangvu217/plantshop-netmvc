using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectroShop.Models
{
    public class ListOrder
    {
        public int ID { get; set; }
        public String CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public double SAmount { get; set; }
        public int? Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ExportDate { get; set; }
    }
}