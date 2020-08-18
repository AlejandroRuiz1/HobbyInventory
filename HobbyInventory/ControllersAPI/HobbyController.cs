using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HobbyInventory.Models.DB;
using HobbyInventory.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HobbyInventory.Controllers
{
    [Route("api/hobbies")]
    [ApiController]
    public class HobbyController : ControllerBase
    {

        [Route("")]
        [HttpGet]
        public List<HobbyDTO> GetHobbies([FromQuery] bool isRetired = false)
        {
            using (var context = new HobbyInventoryContext())
            {
                var hobbies = context.Hobby
                    .Include(hobby => hobby.Products)
                    .Include(hobby => hobby.Category)
                    .Where(x => x.IsRetired == false || isRetired)
                    .ToList();

                return hobbies.Select(hobby => new HobbyDTO
                {
                    Name = hobby.Name,
                    CategoryName = hobby.Category.Name,
                    Products = hobby.Products.Select(product => new ProductDTO
                    {
                        Name = product.Name
                    })
                }).ToList();
 
            }
        }

        [Route("")]
        [HttpPost]
        public HobbyDTO AddHobby(HobbyDTO hobby)
        {//Note: this doesn't to take into consideration that the Category for which it is a part of is Retired or not
            using (var context = new HobbyInventoryContext())
            {
                var newHobby = context.Hobby.FirstOrDefault(hobbies => hobbies.Name == hobby.Name);
                var category = context.Categories.First(c => c.Name == hobby.CategoryName);
                if (newHobby != null)
                {//it exists so we "add it"
                    newHobby.IsRetired = false;
                    newHobby.CategoryId = category.Id;
                    newHobby.Category = category;
                    context.SaveChanges();
                    return hobby;
                }
                else
                {//it doesnt exist so we create/add it
                    context.Hobby.Add(new Hobby
                    {
                        Name = hobby.Name,
                        Category = context.Categories.First(c => c.Name == hobby.CategoryName)
                    });
                    context.SaveChanges();
                    return hobby;
                }
            }
        }

        [Route("{id}")]
        [HttpGet]
        public HobbyDTO GetHobby(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var hobby = context.Hobby.Find(id);
                var category = context.Categories.Find(hobby.CategoryId);
                return new HobbyDTO
                {
                    Name = hobby.Name,
                    CategoryName = category.Name
                };

            }
        }

        [Route("{id}")]
        [HttpDelete]
        public HobbyDTO RemoveHobby(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var removed = context.Hobby.Find(id);
                var products = context.Products;
                foreach (Products p in products)
                {
                    if (removed.Id == p.HobbyId)
                    {
                        p.IsRetired = true;
                    }
                }
                removed.IsRetired = true;
                context.SaveChanges();
                return new HobbyDTO
                {
                    Name = removed.Name,
                    Products = removed.Products.Select(p => new ProductDTO 
                    {
                        Name = p.Name
                    }),
                    CategoryName = removed.Category.Name
                };
            }
        }
        [Route("{id}")]
        [HttpPatch]
        public HobbyDTO UpdateHobby(int id, HobbyDTO updatedHobby)
        {
            //asume that is is always updating a entry that already exists
            using (var context = new HobbyInventoryContext())
            {
                var hobby = context.Hobby.Find(id);
                var category = context.Categories.FirstOrDefault(c => c.Name == updatedHobby.CategoryName);

                if (!hobby.IsRetired)
                {
                    if (category == null)
                    {
                        //the catergory that came in doesnt exist so we can't move the hobby to it
                        return null;
                    }
                    else
                    {
                        hobby.Category = category;
                        hobby.Name = updatedHobby.Name;
                        context.SaveChanges();
                        return updatedHobby;
                    }
                }

                return null;


            }
        }
    }

}
