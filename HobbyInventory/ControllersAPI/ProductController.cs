using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HobbyInventory.Models.DB;
using HobbyInventory.Models.DTOs;
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
        public List<ProductDTO> GetProducts([FromQuery] bool isRetired = false)
        {
            using (var context = new HobbyInventoryContext())
            {
                var products = context.Products
                    .Where(x => x.IsRetired == false || isRetired)
                    .ToList();
                return products.Select(product => new ProductDTO 
                {
                    Name = product.Name,
                    Price = product.Price,
                    Status = product.Status,
                    Quantity = product.Quantity,
                    Hobby = new HobbyDTO
                    {
                        Name = product.Hobby.Name
                    }
                    
                }).ToList();
            }
        }
        [Route("")]
        [HttpPost]
        public ProductDTO AddProduct(ProductDTO product)
        {
            //Note: this doesn't to take into consideration if the Category, Hobby for which it is a part of is Retired
            using (var context = new HobbyInventoryContext())
            {
                var newProduct = context.Products.FirstOrDefault(p => p.Name == product.Name);
                if (newProduct != null)
                {
                    newProduct.IsRetired = false;
                    newProduct.Price = product.Price;
                    newProduct.Quantity = product.Quantity;
                    newProduct.Status = product.Status;
                    context.SaveChanges();
                    return product;
                }
                else
                {
                    context.Add(new Products
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Status = product.Status,
                        Quantity = product.Quantity,
                        Hobby = new Hobby 
                        {
                            Name = product.Hobby.Name
                        }
                    });
                    context.SaveChanges();
                    return product;
                }
                
            }
        }

        [Route("{id}")]
        [HttpGet]
        public ProductDTO GetProduct(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var product = context.Products.Find(id);
                var hobby = context.Hobby.Find(product.HobbyId);
                var category = context.Products.Find(hobby.CategoryId);
                return new ProductDTO {
                    Name = product.Name,
                    Quantity = product.Quantity,
                    Status = product.Status,
                    Price = product.Price,
                    Hobby = new HobbyDTO
                    {
                        Name =  hobby.Name,
                        //Products = hobby.Products.Select(p => new ProductDTO
                        //{
                        //    Name = p.Name,
                        //    Status = p.Status,
                        //    Price = p.Price,
                        //    Quantity = p.Quantity
                            
                        //}),
                        Category = new CategoryDTO { Name = hobby.Category.Name}
                    }
                };
                
            }
        }
        [Route("{id}")]
        [HttpDelete]
        public ProductDTO RemoveProduct(int id)
        {
            using (var context = new HobbyInventoryContext())
            {
                var removed = context.Products.Find(id);
                removed.IsRetired = true;
                context.SaveChanges();
                return new ProductDTO 
                {
                    Name = removed.Name
                };
            }
        }

        [Route("{id}")]
        [HttpPatch]
        public ProductDTO UpdateProduct(int id, ProductDTO updatedProduct)
        {
            using (var context = new HobbyInventoryContext())
            {//something seems off but I don't know what related to product.Hobby = hobby;
                var hobby = context.Hobby.First(x => x.Name == updatedProduct.Hobby.Name);
                var product = context.Products.Find(id);

                if (!product.IsRetired)
                {
                    if(hobby != null)
                    {
                        product.Name = updatedProduct.Name;
                        product.Quantity = updatedProduct.Quantity;
                        product.Status = updatedProduct.Status;
                        product.Price = updatedProduct.Price;
                        product.Hobby = hobby;
                        context.SaveChanges();
                        return updatedProduct;
                    }
                    return null;
                }
                
                return null;
            }
        }
    }

}
