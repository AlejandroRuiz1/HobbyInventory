using HobbyInventory.Models.DB;
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
        public List<Order> GetOrders()
        {
            using (var context = new HobbyInventoryContext())
            {
                var order = context.Orders
                    .Include(order => order.User)
                    .ToList();

                return order;
            }
        }
        [Route("")]
        [HttpPost]
        public Order AddOrder(Order order)
        {
            using (var context = new HobbyInventoryContext())
            {
                var newOrder = context.Orders.Add(order).Entity;
                context.SaveChanges();
                return newOrder;
            }
        }
        [Route("{id}")]
        [HttpGet]
        public Order GetOrder(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                return context.Orders.Find(id);
            }
        }
         [Route("{id}")]
        [HttpPatch]
        public Order UpdateOrder(int id, Order updatedOrder)
        {
            using (var context = new HobbyInventoryContext())
            {
                var order = context.Orders.Find(id);
                //order.Status = updatedOrder.Status;
                order.User = updatedOrder.User;
                order.UserId = updatedOrder.UserId;
                context.SaveChanges();
                return order;
            }
        }
    }
}
