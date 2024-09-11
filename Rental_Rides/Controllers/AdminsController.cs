using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rental_Rides.Models;

namespace Rental_Rides.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly CarRentalDbContext _context;
        private readonly IConfiguration _config;

        public AdminsController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.Admin_ID)
            {
                return BadRequest();
            }

            var existingAdmin = await _context.Admins
                                     .FirstOrDefaultAsync(a => a.Admin_Email == admin.Admin_Email);

            if (existingAdmin != null)
            {
                
                return Conflict(new { message = "An admin with this email already exists." });
            }
            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            var existingAdmin = await _context.Admins
                                      .FirstOrDefaultAsync(a => a.Admin_Email == admin.Admin_Email);

            if (existingAdmin != null)
            {
                // Return a conflict status code (409) if the email already exists
                return Conflict(new { message = "An admin with this email already exists." });
            }
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.Admin_ID }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.Admin_ID == id);
        }

        [HttpPost]
        [Route("AdminLogin")]
        public async Task<IActionResult> LoginValidation(string Admin_Mail, string Password)
        {
            var Admin = await _context.Admins.FirstOrDefaultAsync(a => a.Admin_Email == Admin_Mail);
            if (Admin == null)
            {
                return NotFound("Customer Not Found");
            }
            if (Password != Admin.Admin_Password)
            {
                return BadRequest("Password Wrong");
            }
            string Token = GenerateJwtToken(Admin, Admin.Role);
            return StatusCode(200, "Login Successfull" + " " + Token);
        }

        private string GenerateJwtToken(Admin user, string Role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSection:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Admin_ID.ToString()),
                new Claim(ClaimTypes.Email,user.Admin_Email),
                new Claim(ClaimTypes.Name,user.Admin_Name),
                new Claim(ClaimTypes.Role, Role)
            };



            var jwtToken = new JwtSecurityToken(
                issuer: _config["JwtSection:Issuer"],
                audience: _config["JwtSection:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
