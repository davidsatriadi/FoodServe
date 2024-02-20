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
    public class OrderDetailController : ControllerBase
    {
        private readonly FoodServeContext _context;

        public OrderDetailController(FoodServeContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetail
        [HttpGet]
        public IEnumerable<FsOrderDetail> GetFsOrderDetail()
        {
            return _context.FsOrderDetail;
        }

        // GET: api/OrderDetail/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFsOrderDetail([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsOrderDetail = await _context.FsOrderDetail.FindAsync(id);

            if (fsOrderDetail == null)
            {
                return NotFound();
            }

            return Ok(fsOrderDetail);
        }

        // PUT: api/OrderDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFsOrderDetail([FromRoute] string id, [FromBody] FsOrderDetail fsOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fsOrderDetail.DetailId)
            {
                return BadRequest();
            }

            _context.Entry(fsOrderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FsOrderDetailExists(id))
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

        // POST: api/OrderDetail
        [HttpPost]
        public async Task<IActionResult> PostFsOrderDetail([FromBody] FsOrderDetail fsOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FsOrderDetail.Add(fsOrderDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FsOrderDetailExists(fsOrderDetail.DetailId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFsOrderDetail", new { id = fsOrderDetail.DetailId }, fsOrderDetail);
        }

        // DELETE: api/OrderDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFsOrderDetail([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsOrderDetail = await _context.FsOrderDetail.FindAsync(id);
            if (fsOrderDetail == null)
            {
                return NotFound();
            }

            _context.FsOrderDetail.Remove(fsOrderDetail);
            await _context.SaveChangesAsync();

            return Ok(fsOrderDetail);
        }

        private bool FsOrderDetailExists(string id)
        {
            return _context.FsOrderDetail.Any(e => e.DetailId == id);
        }
    }
}