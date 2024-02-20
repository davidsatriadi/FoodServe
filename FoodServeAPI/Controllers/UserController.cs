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
    public class UserController : ControllerBase
    {
        private readonly FoodServeContext _context;

        public UserController(FoodServeContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<FsUser> GetFsUser()
        {
            return _context.FsUser;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFsUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsUser = await _context.FsUser.FindAsync(id);

            if (fsUser == null)
            {
                return NotFound();
            }

            return Ok(fsUser);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFsUser([FromRoute] string id, [FromBody] FsUser fsUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fsUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(fsUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FsUserExists(id))
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

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostFsUser([FromBody] FsUser fsUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FsUser.Add(fsUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FsUserExists(fsUser.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFsUser", new { id = fsUser.UserId }, fsUser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFsUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fsUser = await _context.FsUser.FindAsync(id);
            if (fsUser == null)
            {
                return NotFound();
            }

            _context.FsUser.Remove(fsUser);
            await _context.SaveChangesAsync();

            return Ok(fsUser);
        }

        private bool FsUserExists(string id)
        {
            return _context.FsUser.Any(e => e.UserId == id);
        }
    }
}