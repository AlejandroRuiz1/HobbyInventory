using System;
using System.Collections.Generic;

namespace HobbyInventory.Models.DB
{
    public partial class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public string Status { get; set; }
        public int? HobbyId { get; set; }

        public virtual Hobby Hobby { get; set; }
    }
}
