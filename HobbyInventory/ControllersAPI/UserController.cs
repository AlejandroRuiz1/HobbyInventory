using HobbyInventory.Models.DB;
using HobbyInventory.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbyInventory.ControllersAPI
{
    [Route("api/orders")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public List<UserDTO> GetUsers()
        {
            using (var context = new HobbyInventoryContext())
            {
                var user = context.Users
                    .Include(u => u.Orders)
                    .ToList();

                return user.Select(u => new UserDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    AddressLine1 = u.AddressLine1,
                    AddressLine2 = u.AddressLine2,
                    City = u.City,
                    EmailAddress = u.EmailAddress,
                    Phone = u.Phone,
                    Id = u.Id,
                    Zipcode = u.Zipcode
                }).ToList();
            }
        }
        
        [Route("")]
        [HttpPost]
        public UserDTO AddUser(UserDTO u)
        {
            using (var context = new HobbyInventoryContext())
            {
                var user = context.Users.FirstOrDefault(user => user.Id == u.Id);
                if(user != null)
                {
                    user.FirstName = u.FirstName;
                    user.LastName = u.LastName;
                    user.AddressLine1 = u.AddressLine1;
                    user.AddressLine2 = u.AddressLine2;
                    user.City = u.City;
                    user.EmailAddress = u.EmailAddress;
                    user.Phone = u.Phone;
                    user.Id = u.Id;
                    user.Zipcode = u.Zipcode;
                    context.SaveChanges();
                    return u;
                }
                else
                {
                    context.Users.Add(new Models.DB.User
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        AddressLine1 = u.AddressLine1,
                        AddressLine2 = u.AddressLine2,
                        City = u.City,
                        EmailAddress = u.EmailAddress,
                        Phone = u.Phone,
                        Id = u.Id,
                        Zipcode = u.Zipcode,
                    });
                    context.SaveChanges();
                    return u;
                }
                
            }
        }

        [Route("{id}")]
        [HttpGet]
        public UserDTO GetUser(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var u = context.Users.Find(id);
                return new UserDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    AddressLine1 = u.AddressLine1,
                    AddressLine2 = u.AddressLine2,
                    City = u.City,
                    EmailAddress = u.EmailAddress,
                    Phone = u.Phone,
                    Id = u.Id,
                    Zipcode = u.Zipcode,
                };
            }
        }

        [Route("{id}")]
        [HttpPatch]
        public UserDTO UpdateUsder(int id, UserDTO updatedUser)
        {
            using (var context = new HobbyInventoryContext())
            {//can only change the status of the order
                var user = context.Users.Find(id);
                if (!user.IsRetired)
                {
                    user.IsRetired = false;
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.AddressLine1 = updatedUser.AddressLine1;
                    user.AddressLine2 = updatedUser.AddressLine2;
                    user.City = updatedUser.City;
                    user.EmailAddress = updatedUser.EmailAddress;
                    user.Phone = updatedUser.Phone;
                    user.Id = updatedUser.Id;
                    user.Zipcode = updatedUser.Zipcode;
                    context.SaveChanges();
                    return updatedUser;

                }
                else
                {
                    return null;
                }

            }
        }
        [Route("{id}")]
        [HttpDelete]
        public UserDTO RemoveUser(int id)
        {
            using (var context = new HobbyInventoryContext())
            {

                var removed = context.Users.Find(id);
                removed.IsRetired = true;
                context.SaveChanges();
                return new UserDTO 
                {
                    FirstName = removed.FirstName,
                    LastName = removed.LastName,
                    AddressLine1 = removed.AddressLine1,
                    AddressLine2 = removed.AddressLine2,
                    City = removed.City,
                    EmailAddress = removed.EmailAddress,
                    Phone = removed.Phone,
                    Id = removed.Id,
                    Zipcode = removed.Zipcode
                };
                
            }
        }
    }
}
