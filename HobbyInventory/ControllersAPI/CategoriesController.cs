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
        //    using (var context = new AdventureWorksDW2016Context()) {
        //            var employees = context.DimEmployee
        //                .Include(x => x.FactSalesQuota)
        //                .Include(x => x.FactResellerSales)
        //                .Where(x => x.DepartmentName == "Sales")
        //                .ToList();
        //output.WriteLine(employees.Count.ToString());
        //        }
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
                //category.Id = updatedCategory.Id;
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
                //prob big O(n^2) yikes
                var removed = context.Categories.Find(id);
                removed.IsRetired = true;
                context.SaveChanges();
                return removed;
            }
        }
        [Route("{id}")]
        [HttpGet]
        public Category GetCategoryID(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                return context.Categories.Find(id);
            }
        }

    }
}
