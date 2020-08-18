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
    public class OrderController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public List<OrderDTO> GetOrders()
        {
            using (var context = new HobbyInventoryContext())
            {
                var order = context.Orders
                    .Include(order => order.User)
                    .ToList();

                return order.Select(o => new OrderDTO 
                {
                    CreatedAt = o.CreatedAt,
                    Id = o.Id,
                    FirstName = o.User.FirstName,
                    Status = o.Status
                }).ToList();
            }
        }
        
        [Route("")]
        [HttpPost]
        public OrderDTO AddOrder(OrderDTO order)
        {
            using (var context = new HobbyInventoryContext())
            {
                var user = context.Users.Find(order.UserId);
                context.Orders.Add(new Order 
                {
                    CreatedAt = order.CreatedAt,
                    Id = order.Id,
                    Status = order.Status,
                    UserId = order.UserId,
                    User = user
                });
                context.SaveChanges();
                return order;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public OrderDTO GetOrder(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var order = context.Orders.Find(id);
                return new OrderDTO
                {
                    CreatedAt = order.CreatedAt,
                    FirstName = order.User.FirstName,
                    Id = order.Id,
                    Status = order.Status,
                    UserId = order.UserId
                };
            }
        }

         [Route("{id}")]
        [HttpPatch]
        public OrderDTO UpdateOrder(int id, OrderDTO updatedOrder)
        {
            using (var context = new HobbyInventoryContext())
            {//can only change the status of the order
                var order = context.Orders.Find(id);
                order.Status = updatedOrder.Status;
                context.SaveChanges();
                return updatedOrder;
            }
        }
    }
}
