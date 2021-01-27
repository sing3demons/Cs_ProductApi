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
        public IActionResult GetProducts()
        {
            try
            {
                IEnumerable<Product> data = DatabaseContext.Products.Include(p => p.ProductCategory).ToList();
            return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProducts: {ex.Message}");
                return BadRequest(ex.Message);
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

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Product product = DatabaseContext.Products.SingleOrDefault(p => p.ID == id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProduct: {ex.Message}");
                return BadRequest();
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
