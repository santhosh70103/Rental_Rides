using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rental_Rides.Models;

namespace Rental_Rides.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CarRentalDbContext _context;
        private readonly IConfiguration _config;

        public CustomersController(CarRentalDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> GetCustomers(int id)
        {
            var customers = await _context.Customers.FindAsync(id);

            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomers(int id, Customers customers)
        {
            if (id != customers.Customer_Id)    
            {
                return BadRequest("Customer Not Exist");
            }

            var existingCustomerByEmail = await _context.Customers
                                                 .FirstOrDefaultAsync(c => c.Customer_Email == customers.Customer_Email);

            if (existingCustomerByEmail != null)
            {
                // Return a conflict status code (409) if the email already exists
                return Conflict(new { message = "A customer with this email already exists." });
            }

            _context.Entry(customers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customers>> PostCustomers(Customers customers)
        {

            var existingCustomerByEmail = await _context.Customers
                                                 .FirstOrDefaultAsync(c=>c.Customer_Email == customers.Customer_Email);

            if (existingCustomerByEmail != null)
            {
                // Return a conflict status code (409) if the email already exists
                return Conflict(new { message = "A customer with this email already exists." });
            }
            _context.Customers.Add(customers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomers", new { id = customers.Customer_Id }, customers);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomers(int id)
        {
            var customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.Customer_Id == id);
        }

        [HttpPost]
        [Route("CustomerLogin")]
        public async Task<IActionResult> LoginValidation(string Customer_Mail, string Password)
        {
            var Customer = await _context.Customers.FirstOrDefaultAsync(c => c.Customer_Email == Customer_Mail);
            if (Customer == null)
            {
                return NotFound("Customer Not Found");
            }
            if (Password != Customer.Customer_Password)
            {
                return BadRequest("Password Wrong");
            }
            string Token = GenerateJwtToken(Customer, Customer.Role);
            return StatusCode(200, "Login Successfull" + " " + Token);
        }

        private string GenerateJwtToken(Customers user, string Role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSection:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Customer_Id.ToString()),
                new Claim(ClaimTypes.Email,user.Customer_Email),
                new Claim(ClaimTypes.Name,user.Customer_Name),
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
