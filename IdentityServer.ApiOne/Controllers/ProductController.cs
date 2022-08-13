using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.ApiOne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.ApiOne.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Authorize(policy:"ReadProduct")]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Pencil",
                    Price = 11.5,
                    Stock = 100
                },
                new Product
                {
                    Id = 2,
                    Name = "Notebook",
                    Price = 11.5,
                    Stock = 100
                },
                new Product
                {
                    Id = 3,
                    Name = "Paper",
                    Price = 11.5,
                    Stock = 100
                }
            };
            return Ok(products);
        }

        [Authorize(policy:"UpdateOrCreate")]
        [HttpPut]
        public IActionResult UpdateProduct(int id)
        {
            return NoContent();
        }
        
        [Authorize(policy:"UpdateOrCreate")]
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            return Ok(product);
        }
    }
}
