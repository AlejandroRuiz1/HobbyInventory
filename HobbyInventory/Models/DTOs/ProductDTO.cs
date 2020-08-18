using HobbyInventory.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbyInventory.Models.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string HobbyName { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }

       
    }
}
