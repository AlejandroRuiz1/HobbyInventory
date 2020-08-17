using HobbyInventory.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HobbyInventory.Models.DTOs
{
    public class CategoryDTO
    {
        public string Name { get; set; }

        public virtual IEnumerable<HobbyDTO> Hobbies { get; set; }
    }
}
