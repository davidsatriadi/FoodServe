using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodServeAPI.Models;

namespace FoodServeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodServeContext _context;

        public FoodController(FoodServeContext context)
        {
            _context = context;
        }

        // GET: api/Food
        [HttpGet]
        public IEnumerable<FsFood> GetFsFood()
        {
            return _context.FsFood;
        }

        // GET: api/Food/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFsFood([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsFood = await _context.FsFood.FindAsync(id);

            if (fsFood == null)
            {
                return NotFound();
            }

            return Ok(fsFood);
        }

        // PUT: api/Food/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFsFood([FromRoute] string id, [FromBody] FsFood fsFood)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fsFood.FoodId)
            {
                return BadRequest();
            }

            _context.Entry(fsFood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FsFoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Food
        [HttpPost]
        public async Task<IActionResult> PostFsFood([FromBody] FsFood fsFood)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FsFood.Add(fsFood);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FsFoodExists(fsFood.FoodId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFsFood", new { id = fsFood.FoodId }, fsFood);
        }

        // DELETE: api/Food/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFsFood([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsFood = await _context.FsFood.FindAsync(id);
            if (fsFood == null)
            {
                return NotFound();
            }

            _context.FsFood.Remove(fsFood);
            await _context.SaveChangesAsync();

            return Ok(fsFood);
        }

        private bool FsFoodExists(string id)
        {
            return _context.FsFood.Any(e => e.FoodId == id);
        }
    }
}