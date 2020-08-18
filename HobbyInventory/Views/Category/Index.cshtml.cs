
using HobbyInventory.ControllersView;
using HobbyInventory.Models.DB;
using HobbyInventory.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbyInventory.Views.Category
{
    public class Index
    {
        private readonly CategoriesController categoriesController;

        public IEnumerable<CategoryDTO> Categories { get; set; } 

        public Index(CategoriesController categories)
        {
            this.categoriesController = categories;
        }
        public void OnGet()
        {
            Categories = categoriesController.GetCategories();
        }
    }
}
