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
    [Route("api/orderItems")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public List<OrderItemDTO> GetOrders()
        {
            using (var context = new HobbyInventoryContext())
            {
                var orderItem = context.OrderItems
                    .Include(order => order.Order)
                    .Include(order => order.Product)
                    .ToList();


                return orderItem.Select(o => new OrderItemDTO 
                {
                    Id = o.Id,
                    Quantity = o.Quantity,
                    OrderId = o.Order.Id,
                    ProductName = o.Product.Name
                }).ToList();
            }
        }
        [Route("")]
        [HttpPost]
        public OrderItemDTO AddOrderItem(OrderItemDTO orderItem)
        {
            using (var context = new HobbyInventoryContext())
            {
                var newOrderItems = context.OrderItems.FirstOrDefault(oi => oi.Id == orderItem.Id);
                var order = context.Orders.FirstOrDefault(o => o.Id == orderItem.OrderId);
                var product = context.Products.FirstOrDefault(p => p.Name == orderItem.ProductName);
                if (newOrderItems != null)
                {
                    newOrderItems.IsRetired = false;
                    newOrderItems.Quantity = orderItem.Quantity;
                    context.SaveChanges();
                    return orderItem;
                }
                else
                {
                    if(order != null && product != null && product.IsRetired == false)
                    {
                        var newOrderIteam = context.OrderItems.Add(new OrderItem
                        {
                            Order = order,
                            Product = product,
                            Quantity = orderItem.Quantity,
                            ProductId = product.Id,
                            OrderId = order.Id
                            
                        });
                        context.SaveChanges();
                        return orderItem;
                    }
                    return null;
                }
                
            }
        }
        [Route("{id}")]
        [HttpGet]
        public OrderItemDTO GetOrderItem(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var orderItem = context.OrderItems.Find(id);
                var product = context.Products.Find(orderItem.ProductId);
                var order = context.Orders.Find(orderItem.OrderId);
                return new OrderItemDTO 
                {
                    Id = orderItem.Id,
                    OrderId = order.Id,
                    ProductName = product.Name,
                    Quantity = orderItem.Quantity
                    
                };
                
            }
        }
        [Route("{id}")]
        [HttpPatch]
        public OrderItemDTO UpdateOrderItem(int id, OrderItemDTO updatedOrderItem)
        {
            using (var context = new HobbyInventoryContext())
            {
                var orderItem = context.OrderItems.Find(id);
                var product = context.Products.First(p => p.Id == orderItem.ProductId);
                var order = context.Orders.First(o => o.Id == orderItem.OrderId);
                if (!orderItem.IsRetired)
                {
                    orderItem.Quantity = updatedOrderItem.Quantity;
                    context.SaveChanges();
                    return updatedOrderItem;
                }
                return null;
            }
        }
        [Route("{id}")]
        [HttpDelete]
        public OrderItemDTO RemoveOrderItem(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var removed = context.OrderItems.Find(id);
                removed.IsRetired = true;
                context.SaveChanges();
                return new OrderItemDTO 
                {
                    OrderId = removed.OrderId,
                    Id = removed.Id,
                    Quantity = removed.Quantity,
                    ProductName = removed.Product.Name
                    
                };
            }
        }
    }
}
