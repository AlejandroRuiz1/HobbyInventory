using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HobbyInventory.Models.DB
{
    public partial class Hobby
    {
        public Hobby()
        {
            Products = new HashSet<Products>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool IsRetired { get; set; }

        public virtual Category Category { get; set; }


        public virtual ICollection<Products> Products { get; set; }
    }
}
