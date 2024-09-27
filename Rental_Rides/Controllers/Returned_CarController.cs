using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rental_Rides.Models;

namespace Rental_Rides.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class Returned_CarController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public Returned_CarController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/Returned_Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Returned_Car>>> GetReturned_Cars()
        {
            return await _context.Returned_Cars.ToListAsync();
        }

        // GET: api/Returned_Car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Returned_Car>> GetReturned_Car(int id)
        {
            var returned_Car = await _context.Returned_Cars.FindAsync(id);

            if (returned_Car == null)
            {
                return NotFound();
            }

            return returned_Car;
        }

        // PUT: api/Returned_Car/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReturned_Car(int id, Returned_Car returned_Car)
        {
            if (id != returned_Car.Return_Id)
            {
                return BadRequest();
            }

            _context.Entry(returned_Car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Returned_CarExists(id))
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

        // POST: api/Returned_Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Returned_Car>> PostReturned_Car(Returned_Car returned_Car)
        {
            _context.Returned_Cars.Add(returned_Car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReturned_Car", new { id = returned_Car.Return_Id }, returned_Car);
        }

        // DELETE: api/Returned_Car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReturned_Car(int id)
        {
            var returned_Car = await _context.Returned_Cars.FindAsync(id);
            if (returned_Car == null)
            {
                return NotFound();
            }

            _context.Returned_Cars.Remove(returned_Car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Returned_CarExists(int id)
        {
            return _context.Returned_Cars.Any(e => e.Return_Id == id);
        }
    }
}
