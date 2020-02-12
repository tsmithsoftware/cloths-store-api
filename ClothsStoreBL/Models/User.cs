using System;
using System.Collections.Generic;
using System.Text;

namespace ClothsStore.BL.Models
{
    // hashed password information stored in db
    public class User
    {
        public int id { get; set; }
        public String Username { get; set; }
        public String HashedPassword { get; set; }
    }
}
