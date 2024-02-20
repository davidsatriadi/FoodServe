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
    public class FoodOrderController : ControllerBase
    {
        private readonly FoodServeContext _context;

        public FoodOrderController(FoodServeContext context)
        {
            _context = context;
        }

        // GET: api/FoodOrder
        [HttpGet]
        public IEnumerable<FsFoodOrder> GetFsFoodOrder()
        {
            return _context.FsFoodOrder;
        }

        // GET: api/FoodOrder/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFsFoodOrder([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsFoodOrder = await _context.FsFoodOrder.FindAsync(id);

            if (fsFoodOrder == null)
            {
                return NotFound();
            }

            return Ok(fsFoodOrder);
        }

        // PUT: api/FoodOrder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFsFoodOrder([FromRoute] string id, [FromBody] FsFoodOrder fsFoodOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fsFoodOrder.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(fsFoodOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FsFoodOrderExists(id))
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

        // POST: api/FoodOrder
        [HttpPost]
        public async Task<IActionResult> PostFsFoodOrder([FromBody] FsFoodOrder fsFoodOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FsFoodOrder.Add(fsFoodOrder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FsFoodOrderExists(fsFoodOrder.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFsFoodOrder", new { id = fsFoodOrder.OrderId }, fsFoodOrder);
        }

        // DELETE: api/FoodOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFsFoodOrder([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsFoodOrder = await _context.FsFoodOrder.FindAsync(id);
            if (fsFoodOrder == null)
            {
                return NotFound();
            }

            _context.FsFoodOrder.Remove(fsFoodOrder);
            await _context.SaveChangesAsync();

            return Ok(fsFoodOrder);
        }

        private bool FsFoodOrderExists(string id)
        {
            return _context.FsFoodOrder.Any(e => e.OrderId == id);
        }
    }
}