using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rental_Rides.Models;

namespace Rental_Rides.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Rented_CarController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public Rented_CarController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/Rented_Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rented_Car>>> GetRented_Cars()
        {
            return await _context.Rented_Cars.ToListAsync();
        }

        // GET: api/Rented_Car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rented_Car>> GetRented_Car(int id)
        {
            var rented_Car = await _context.Rented_Cars.FindAsync(id);

            if (rented_Car == null)
            {
                return NotFound();
            }

            return rented_Car;
        }

        // PUT: api/Rented_Car/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRented_Car(int id, Rented_Car rented_Car)
        {
            if (id != rented_Car.Rental_Id)
            {
                return BadRequest();
            }

            _context.Entry(rented_Car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Rented_CarExists(id))
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

        // POST: api/Rented_Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rented_Car>> PostRented_Car(Rented_Car rented_Car)
        {
            _context.Rented_Cars.Add(rented_Car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRented_Car", new { id = rented_Car.Rental_Id }, rented_Car);
        }

        // DELETE: api/Rented_Car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRented_Car(int id)
        {
            var rented_Car = await _context.Rented_Cars.FindAsync(id);
            if (rented_Car == null)
            {
                return NotFound();
            }

            _context.Rented_Cars.Remove(rented_Car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Rented_CarExists(int id)
        {
            return _context.Rented_Cars.Any(e => e.Rental_Id == id);
        }
    }
}
