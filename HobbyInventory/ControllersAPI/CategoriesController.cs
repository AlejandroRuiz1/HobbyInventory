using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using HobbyInventory.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HobbyInventory.ControllersAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public List<Category> Categories()
        {
            using (var context = new HobbyInventoryContext())
            {
                return context.Categories.ToList();
            }
            //return "hello!";
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

    }
}
