using System;
using System.Collections.Generic;

namespace HobbyInventory.Models.DB
{
    public partial class Hobby
    {
        public Hobby()
        {
            Category = new HashSet<Category>();
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
