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
  
       //update, add, remove, compare? for each....
       //if time make a func that can change the category that a given product is in

        [Route("remove")]
        public void removeCategory()
        {
            //we have to remove it and remove all the the hobbies associated
        }

        [Route("getHobbies")]
        public List<Hobby> GetHobbies()
        {
            using (var context = new HobbyInventoryContext())
            {
                return context.Hobby.ToList();
            }
        }
    }

}
