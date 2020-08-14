using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HobbyInventory.Models.DB
{
    public partial class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }
        public string CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
    public enum OrderStatus
    {
        cancelled,
        in_process,
        shipped,
        in_route,
        running_late,
        delivered
    }
}
