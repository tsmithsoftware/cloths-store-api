using System;
using System.Collections.Generic;
using System.Text;

namespace ClothsStore.BL.Models
{
    public class Product
    {
        public long id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string price { get; set; }
        public string oldPrice { get; set; }
        public int stock { get; set; }
    }
}