using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyInventory.Models.DB
{
    public partial class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [DefaultValue(1)]
        public int Quantity { get; set; }
        public bool IsRetired { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
