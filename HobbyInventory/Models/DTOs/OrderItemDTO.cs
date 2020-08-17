using HobbyInventory.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbyInventory.Models.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }


    }
}
