using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cs_ProductApi.Database;
using Cs_ProductApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cs_ProductApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {

        ILogger<ProductController> _logger;
        private readonly DatabaseContext DatabaseContext;

        public ProductController(ILogger<ProductController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            this.DatabaseContext = databaseContext;
        }

        // ------> GET: api/product
        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts() 
        {
            try
            {
                var data = await DatabaseContext.Products.Include(p => p.ProductCategory).ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProducts: {ex.Message}");
                return NotFound(ex.Message);
            }
        }


        // GET: api/product/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {
            try
            {
                var product = await DatabaseContext.Products.FindAsync(id);
                if (product == null)
                {
                     throw new ArgumentException();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProduct: {ex.Message}");
                return NotFound(ex.Message);
            }
        }

        // POST: api/product
       [HttpPost]
       public async Task<ActionResult<Product>> CreateProduct([FromBody] Product model)
        {
            try
            {
                DatabaseContext.Products.Add(model);
                await DatabaseContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetItem), new { id = model.ID }, model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateProduct: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromBody] Product model, int id)
        {
            try
            {
                var product = DatabaseContext.Products.Find(id);

                if (product == null)
                {
                    throw new AggregateException();
                }

                product.Name = model.Name;
                product.Desc = model.Desc;
                product.Image = model.Image;
                product.Price = model.Price;
                product.CategoryID = model.CategoryID;
              

                DatabaseContext.Products.Update(product);
                DatabaseContext.SaveChanges();

                return Ok("success");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateProduct: {ex.Message}");
                return BadRequest();
            }
        }
  
      
        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = DatabaseContext.Products.Find(id);

                if (product == null)
                {
                    throw new AggregateException();
                }

                DatabaseContext.Products.Remove(product);
                DatabaseContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteProduct: {ex.Message}");
                return NotFound(ex.Message);
            }
        }
    }
}
