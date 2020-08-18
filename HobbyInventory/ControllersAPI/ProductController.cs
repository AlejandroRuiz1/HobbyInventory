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
                    .Include(h => h.Hobby)
                    .Where(x => x.IsRetired == false || isRetired)
                    .ToList();
                return products.Select(product => new ProductDTO
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    HobbyName = product.Hobby.Name,
                    Status = product.Status.ToString()
                }).ToList();
            }
        }
        [Route("")]
        [HttpPost]
        public ProductDTO AddProduct(ProductDTO product)
        {
            //we only all the addition of a product to a hobby. Nothing else can be added
            using (var context = new HobbyInventoryContext())
            {
                var newProduct = context.Products.FirstOrDefault(p => p.Name == product.Name);
                var hobby = context.Hobby.FirstOrDefault(h => h.Name == product.HobbyName);
                var category = context.Categories.FirstOrDefault(c => c.Name == product.CategoryName);
                if(category != null && !category.IsRetired)
                {

                    if(hobby != null && !hobby.IsRetired)
                    {

                        if(newProduct != null)
                        {
                            newProduct.Hobby = hobby;
                            newProduct.HobbyId = hobby.Id;
                            newProduct.Price = product.Price;
                            newProduct.Quantity = product.Quantity;
                            context.SaveChanges();
                            return product;
                        }
                        else
                        {
                            context.Products.Add(new Products 
                            {
                                Name = product.Name,
                                Price = product.Price,
                                Hobby = hobby,
                                HobbyId = hobby.Id,
                                Quantity = product.Quantity
                            });
                            context.SaveChanges();
                            return product;

                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
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
                    Price = product.Price,
                    HobbyName =  hobby.Name,   
                    CategoryName = hobby.Category.Name,
                    Status = product.Status.ToString()
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
            {
                var hobby = context.Hobby.FirstOrDefault(x => x.Name == updatedProduct.HobbyName);
                var product = context.Products.Find(id);
                var category = context.Categories.FirstOrDefault(c => c.Name == updatedProduct.CategoryName);
                if(category != null && !category.IsRetired)
                {
                    if(hobby != null && !hobby.IsRetired)
                    {
                        if (!product.IsRetired)
                        {
                            product.Name = updatedProduct.Name;
                            product.Hobby = hobby;
                            product.HobbyId = hobby.Id;
                            product.Price = updatedProduct.Price;
                            product.Quantity = updatedProduct.Quantity;
                            context.SaveChanges();
                            return updatedProduct;
                        }
                        else
                        {
                            return null;
                        }
                        
                        
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }

}
