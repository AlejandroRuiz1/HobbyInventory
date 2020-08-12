using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyInventory.Models.DB
{
    public partial class Products
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int? Price { get; set; }
        public string Status { get; set; }
        public int HobbyId { get; set; }
        [ForeignKey("HobbyId")]
        public virtual Hobby Hobby { get; set; }
    }

    public enum Status
    {
        // out of stock, in stock, running low
        //enum outOf
    }

}
