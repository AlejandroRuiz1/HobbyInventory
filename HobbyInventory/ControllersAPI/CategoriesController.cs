using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using HobbyInventory.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HobbyInventory.ControllersAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public List<Category> Categories([FromQuery] bool isRetired = false)
        {
            using (var context = new HobbyInventoryContext())
            {
                var category = context.Categories
                    .Include(category => category.Hobbies)
                    .Where(x => x.IsRetired == false || isRetired)
                    .ToList();
                    
                return category;
            }
        }

        [Route("")]
        [HttpPost]
        public Category AddCategory(Category category)
        {
            using (var context = new HobbyInventoryContext())
            {
                var newCategory = context.Categories.Add(category).Entity;
                context.SaveChanges();
                return newCategory;
            }
        }

        [Route("{id}")]
        [HttpPatch]
        public Category UpdateCategory(int id, Category updatedCategory)
        {
            using (var context = new HobbyInventoryContext())
            {
                var category = context.Categories.Find(id);
                category.Name = updatedCategory.Name;
                category.IsRetired = updatedCategory.IsRetired;
                context.SaveChanges();
                return category;
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public Category RemoveCategory(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var removed = context.Categories.Find(id);
                removed.IsRetired = true;
                context.SaveChanges();
                return removed;
            }
        }
        [Route("{id}")]
        [HttpGet]
        public Category GetCategory(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                return context.Categories.Find(id);
            }
        }

    }
}
