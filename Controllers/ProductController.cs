using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cs_ProductApi.Database;
using Cs_ProductApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cs_ProductApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        ILogger<ProductController> _logger;
        private readonly DatabaseContext DatabaseContext;

        public ProductController(ILogger<ProductController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            this.DatabaseContext = databaseContext;
        }

        // ------> GET: api/values
        [HttpGet]
        //public IActionResult GetProducts()
        public async Task<ActionResult<Product>> GetProducts() 
        {
            try
            {
                //IEnumerable<Product> data = DatabaseContext.Products.Include(p => p.ProductCategory).ToList();
                //return Ok(data);

                var data = await DatabaseContext.Products.Include(p => p.ProductCategory).ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProducts: {ex.Message}");
                return NotFound(ex.Message);
            }
        }



        [HttpGet("join")]
        public IActionResult GetProductsByJoin()
        {
            try
            {
                IEnumerable<Product> result = DatabaseContext.Products.Include(p => p.ProductCategory).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProductsByJoin: {ex.Message}");
                return BadRequest();
            }
        }

        // GET: api/product/1
        //[HttpGet("{id}", Name = "GetItem")]
        //public IActionResult CreateProduct([FromBody] Product model)
        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {
            try
            {
                //Product product =  DatabaseContext.Products.SingleOrDefault(p => p.ID == id);
                var product = await DatabaseContext.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
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
        //public IActionResult CreateProduct([FromBody] Product model)
       public async Task<ActionResult<Product>> CreateProduct([FromBody] Product model)
        {
            try
            {
               
                //DatabaseContext.Products.Add(model);
                DatabaseContext.Products.Add(model);
                //DatabaseContext.SaveChanges();
                await DatabaseContext.SaveChangesAsync();
                //return CreatedAtRoute("GetProduct", new { id = 8 }, model);
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
        //public async Task<IActionResult> UpdateProduct(int id, Product model)

        {
            try
            {
                var product = DatabaseContext.Products.Find(id);

                if (product == null)
                {
                    return NotFound();
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
        //public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = DatabaseContext.Products.Find(id);

                if (product == null)
                {
                    return NotFound();
                }

                DatabaseContext.Products.Remove(product);
                DatabaseContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteProduct: {ex.Message}");
                return BadRequest();
            }
            //    var product = await DatabaseContext.Products.FindAsync(id);
            //    if (product == null)
            //    {
            //        return NotFound("Not Found");
            //    }

            //    DatabaseContext.Products.Remove(product);
            //    await DatabaseContext.SaveChangesAsync();

            //    return NoContent();
        }
    }
}
