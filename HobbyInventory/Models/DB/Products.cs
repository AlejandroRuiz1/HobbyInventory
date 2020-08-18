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
        public int Price { get; set; }
        
        public ProductStatus Status => Quantity==0 
            ? ProductStatus.out_of_stock : Quantity<10 
            ? ProductStatus.running_low : ProductStatus.in_stock;
        
        public int HobbyId { get; set; }
        public bool IsRetired { get; set; }
        [ForeignKey("HobbyId")]
        public virtual Hobby Hobby { get; set; }

        

    }

    public enum ProductStatus
    {
        out_of_stock,
        running_low,
        in_stock
        
    }

}
