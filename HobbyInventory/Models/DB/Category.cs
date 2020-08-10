using System;
using System.Collections.Generic;

namespace HobbyInventory.Models.DB
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? HobbyId { get; set; }

        public virtual Hobby Hobby { get; set; }
    }
}
