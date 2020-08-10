using System;
using System.Collections.Generic;

namespace HobbyInventory.Models.DB
{
    public partial class OrderItems
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
