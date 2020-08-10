using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HobbyInventory.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HobbyInventory.Controllers
{
    [Route("api/hobbies")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        [Route("hello")]
        public string Hello()
        {
            return "Hello!";
        }

        [Route("")]
        public Hobby getHobby()
        {
            return new Hobby { Id = 1, Name="Fishing"};
        }
       //update, add, remove, compare? for each....
       //if time make a func that can change the category that a given product is in
    }
}
