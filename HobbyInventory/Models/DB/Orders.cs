using System;
using System.Collections.Generic;

namespace HobbyInventory.Models.DB
{
    public partial class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }

        public virtual Users User { get; set; }
    }
}
