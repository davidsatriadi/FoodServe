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
    public class LoginController : ControllerBase
    {
        private readonly Models.FoodServeContext _context;

        public LoginController(Models.FoodServeContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.FsUser
                .FirstOrDefaultAsync(u => u.UserId == request.Username && u.Password == request.Password);

            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            // Perform additional login logic if needed

            return Ok("Login successful");
        }
    }

}