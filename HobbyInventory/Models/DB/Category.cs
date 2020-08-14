using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HobbyInventory.Models.DB
{
    public partial class Category
    {
        public Category()
        {
            Hobbies = new HashSet<Hobby>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRetired { get; set; }

        public virtual ICollection<Hobby> Hobbies { get; set; }
        
    }
}
