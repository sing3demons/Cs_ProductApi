using System;
using System.Linq;
using System.Threading.Tasks;
using Cs_ProductApi.Database;
using Cs_ProductApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cs_ProductApi.Controllers
{

    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CategoryController(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        //GET: api/v1/category
        [HttpGet]
         public async Task<ActionResult<Category>> Get() 
        {
            try
            {
                var data = await _context.Categories.ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET: api/v1/category
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            try
            {
                Category result = await _context.Categories.FindAsync(id);
                if (result == null)
                {
                    throw new ArgumentException();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        //POST: api/v1/category
        [HttpPost]
        public async Task<ActionResult<Category>> CreateItem([FromBody] Category model)
        {
            try
            {
                _context.Categories.Add(model);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProductById), new { id = model.CategoryID }, model);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/v1/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] Product model, int id)
        {
            try
            {
                var category =  _context.Categories.Find(id);
                if (category == null)
                {
                    throw new AggregateException();
                }

                category.Name = model.Name;



                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/v1/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                {
                    throw new AggregateException();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
   
                return BadRequest(ex.Message);
            }
        }


    }
}
