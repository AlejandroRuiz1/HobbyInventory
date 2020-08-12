using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HobbyInventory.Models.DB;
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
        [HttpDelete]
        public List<Hobby> RemoveAllHobbyies()
        {
            using(var context = new HobbyInventoryContext())
            {
                //prob big O(n^2) yikes
                var removed = context.Hobby.ToList();
                //we have to remove all of the protucts and categories that belong to it first, then we can delete the hobby itself
                return removed;
            }
        }

        [Route("")]
        [HttpGet]
        public List<Hobby> GetHobbies()
        {
            using (var context = new HobbyInventoryContext())
            {
                return context.Hobby.ToList();
            }
        }
        [Route("")]
        [HttpPost]
        public Hobby AddHobby(Hobby hobby)
        {
            using (var context = new HobbyInventoryContext())
            {
                var newHobby = context.Hobby.Add(hobby).Entity;
                context.SaveChanges();
                return newHobby;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public Hobby GetHobbyID(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                return context.Hobby.Find(id);
            }
        }
        [Route("{id}")]
        [HttpDelete]
        public Hobby RemoveHobby(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                //prob big O(n^2) yikes
                var removed = context.Hobby.Find(id);
                context.Hobby.Remove(context.Hobby.Find(id));
                return removed;
            }
        }
        [Route("{id}")]
        [HttpPatch]
        public Hobby UpdateHobby(int id, Hobby updatedHobby)
        {
            using (var context = new HobbyInventoryContext())
            {
                var hobby = context.Hobby.Find(id);
                hobby.Id = updatedHobby.Id;
                hobby.Name = updatedHobby.Name;
                hobby.Products = updatedHobby.Products;
                context.SaveChanges();
                return hobby;
            }
        }
    }

}
