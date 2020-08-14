using HobbyInventory.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbyInventory.ControllersAPI
{
    [Route("api/orderItems")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public List<OrderItem> GetOrders()
        {
            using (var context = new HobbyInventoryContext())
            {
                var orderItem = context.OrderItems
                    .Include(order => order.Order)
                    .Include(order => order.Product)
                    .ToList();

                return orderItem;
            }
        }
        [Route("")]
        [HttpPost]
        public OrderItem AddOrderItem(OrderItem order)
        {
            using (var context = new HobbyInventoryContext())
            {
                var newOrder = context.OrderItems.Add(order).Entity;
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
        public OrderItem UpdateOrderItem(int id, OrderItem updatedOrderItem)
        {
            using (var context = new HobbyInventoryContext())
            {
                var orderItem = context.OrderItems.Find(id);
                orderItem.IsRetired= updatedOrderItem.IsRetired;
                //orderItem.Order= updatedOrderItem.Order;
                //orderItem.OrderId = updatedOrderItem.OrderId;
                orderItem.Product = updatedOrderItem.Product;
                orderItem.ProductId = updatedOrderItem.ProductId;
                orderItem.Quantity = updatedOrderItem.Quantity;
                context.SaveChanges();
                return orderItem;
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
    }
}
