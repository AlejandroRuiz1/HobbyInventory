using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using HobbyInventory.Models.DB;
using HobbyInventory.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace HobbyInventory.ControllersAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public List<CategoryDTO> GetCategories([FromQuery] bool isRetired = false)
        {
            using (var context = new HobbyInventoryContext())
            {
                var categories = context.Categories
                    .Include(category => category.Hobbies)
                    .Where(x => x.IsRetired == false || isRetired)
                    .ToList();
                return categories.Select(category => new CategoryDTO
                {
                    Name = category.Name,
                    Hobbies = category.Hobbies.Select(hobby => new HobbyDTO
                    {
                        Name = hobby.Name
                    })
                }).ToList();
            }
        }

        [Route("")]
        [HttpPost]
        public CategoryDTO AddCategory(CategoryDTO category)
        {
            using (var context = new HobbyInventoryContext())
            {
                var newCategory = context.Categories.FirstOrDefault(c => c.Name == category.Name);
                if(newCategory != null)
                {
                    newCategory.IsRetired = false;
                    context.SaveChanges();
                    return category;
                }
                else
                {
                    context.Categories.Add(new Category
                    {
                        Name = category.Name
                    });
                    context.SaveChanges();
                    return category;
                }
                
            }
        }

        [Route("{id}")]
        [HttpPatch]
        public Category UpdateCategory(int id, Category updatedCategory)
        {//this may to account for the hobbies being attached to it
            using (var context = new HobbyInventoryContext())
            {
                var category = context.Categories.Find(id);
                if (!category.IsRetired)
                {
                    category.Name = updatedCategory.Name;
                    category.IsRetired = updatedCategory.IsRetired;
                    context.SaveChanges();
                    return updatedCategory;
                }
                else
                {
                    return null;
                }
                
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public CategoryDTO RemoveCategory(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                
                var removed = context.Categories.Find(id);
                var hobbies = context.Hobby.Where(hobby => hobby.CategoryId == removed.Id)
                    .Include(h => h.Products)
                    .ToList();
                removed.IsRetired = true;
                foreach (Hobby h in hobbies)
                {
                        h.IsRetired = true;
                        foreach(Products p in h.Products)
                    {
                        p.IsRetired = true;
                    }
                } 
                context.SaveChanges();

                return new CategoryDTO {
                    Name = removed.Name,
                    Hobbies = removed.Hobbies.Select(hobby => new HobbyDTO
                    {
                        Name = hobby.Name
                    })
                };
            }
        }
        [Route("{id}")]
        [HttpGet]
        public CategoryDTO GetCategory(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var categoryDTO = context.Categories.Find(id);
                return new CategoryDTO() {
                    Name = categoryDTO.Name,
                    Hobbies = categoryDTO.Hobbies.Select(hobby => new HobbyDTO
                    {
                        Name = hobby.Name
                    })
                };
            }
        }

    }
}
