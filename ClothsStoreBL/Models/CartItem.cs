using System;
using System.Collections.Generic;
using System.Text;

namespace ClothsStore.BL.Models
{
    public class CartItem
    {
        public int id { get; set; }
        public Product product { get; set; }
        public User user { get; set; }
    }
}
