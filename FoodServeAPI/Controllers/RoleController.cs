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
    public class RoleController : ControllerBase
    {
        private readonly FoodServeContext _context;

        public RoleController(FoodServeContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public IEnumerable<FsRole> GetFsRole()
        {
            return _context.FsRole;
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFsRole([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsRole = await _context.FsRole.FindAsync(id);

            if (fsRole == null)
            {
                return NotFound();
            }

            return Ok(fsRole);
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFsRole([FromRoute] string id, [FromBody] FsRole fsRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fsRole.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(fsRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FsRoleExists(id))
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

        // POST: api/Role
        [HttpPost]
        public async Task<IActionResult> PostFsRole([FromBody] FsRole fsRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FsRole.Add(fsRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FsRoleExists(fsRole.RoleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFsRole", new { id = fsRole.RoleId }, fsRole);
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFsRole([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsRole = await _context.FsRole.FindAsync(id);
            if (fsRole == null)
            {
                return NotFound();
            }

            _context.FsRole.Remove(fsRole);
            await _context.SaveChangesAsync();

            return Ok(fsRole);
        }

        private bool FsRoleExists(string id)
        {
            return _context.FsRole.Any(e => e.RoleId == id);
        }
    }
}