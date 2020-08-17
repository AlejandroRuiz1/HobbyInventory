using HobbyInventory.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbyInventory.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public string CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}
