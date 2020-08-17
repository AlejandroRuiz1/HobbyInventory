using HobbyInventory.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbyInventory.Models.DTOs
{
    public class HobbyDTO
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public virtual CategoryDTO Category { get; set; }


        public virtual IEnumerable<ProductDTO> Products { get; set; }
    }
}
