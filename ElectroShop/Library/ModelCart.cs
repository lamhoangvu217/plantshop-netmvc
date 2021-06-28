using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectroShop.Library
{
    public class ModelCart
    {
        public int ProductID { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public String Image { get; set; }


        
    }
}