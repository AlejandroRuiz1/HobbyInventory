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
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        [Route("")]
        [HttpGet]
        public List<Products> GetProducts([FromQuery] bool isRetired = false)
        {
            using (var context = new HobbyInventoryContext())
            {
                return context.Products
                    .Where(x => x.IsRetired == false || isRetired)
                    .ToList();
            }
        }
        [Route("")]
        [HttpPost]
        public Products AddProduct(Products product)
        {
            using (var context = new HobbyInventoryContext())
            {
                var newProduct = context.Products.Add(product).Entity;
                context.SaveChanges();
                return newProduct;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public Products GetProduct(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                return context.Products.Find(id);
            }
        }
        [Route("{id}")]
        [HttpDelete]
        public Products RemoveProduct(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var removed = context.Products.Find(id);
                removed.IsRetired = true;
                return removed;
            }
        }

        [Route("{id}")]
        [HttpPatch]
        public Products UpdateProduct(int id, Products updatedProduct)
        {
            using (var context = new HobbyInventoryContext())
            {
                var hobby = context.Hobby.First(x => x.Name == updatedProduct.Hobby.Name);
                //DTO
                //
                var product = context.Products.Find(id);
                product.Name = updatedProduct.Name;
                product.IsRetired = updatedProduct.IsRetired;
                product.Quantity = updatedProduct.Quantity;
                product.Status = updatedProduct.Status;
                product.Price = updatedProduct.Price;
                product.Hobby = hobby;
                context.SaveChanges();
                return product;
            }
        }
    }

}
