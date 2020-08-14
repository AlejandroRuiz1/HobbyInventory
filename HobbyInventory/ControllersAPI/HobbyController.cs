﻿using System;
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
        [HttpGet]
        public List<Hobby> GetHobbies([FromQuery] bool isRetired = false)
        {
            using (var context = new HobbyInventoryContext())
            {
                var hobby = context.Hobby
                    .Include(hobby => hobby.Products)
                    .Where(x => x.IsRetired == false || isRetired)
                    .ToList();


                return hobby;
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
        public Hobby GetHobby(int id)
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
                var removed = context.Hobby.Find(id);
                removed.IsRetired = true;
                context.SaveChanges();
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
                hobby.Name = updatedHobby.Name;
                hobby.Products = updatedHobby.Products;
                hobby.IsRetired = updatedHobby.IsRetired;
                hobby.CategoryId = updatedHobby.CategoryId;
                hobby.Category = updatedHobby.Category;
                context.SaveChanges();
                return hobby;
            }
        }
    }

}
