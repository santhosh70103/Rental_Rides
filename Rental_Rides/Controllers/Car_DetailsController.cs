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
    public class Car_DetailsController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public Car_DetailsController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/Car_Details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car_Details>>> GetCar_Details()
        {
            return await _context.Car_Details.ToListAsync();
        }

        // GET: api/Car_Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car_Details>> GetCar_Details(int id)
        {
            var car_Details = await _context.Car_Details.FindAsync(id);

            if (car_Details == null)
            {
                return NotFound();
            }

            return car_Details;
        }

        // PUT: api/Car_Details/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar_Details(int id, Car_Details car_Details)
        {
            if (id != car_Details.Car_Id)
            {
                return BadRequest();
            }

            _context.Entry(car_Details).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Car_DetailsExists(id))
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

        // POST: api/Car_Details
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car_Details>> PostCar_Details(Car_Details car_Details)
        {
            _context.Car_Details.Add(car_Details);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar_Details", new { id = car_Details.Car_Id }, car_Details);
        }

        // DELETE: api/Car_Details/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar_Details(int id)
        {
            var car_Details = await _context.Car_Details.FindAsync(id);
            if (car_Details == null)
            {
                return NotFound();
            }

            _context.Car_Details.Remove(car_Details);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Car_DetailsExists(int id)
        {
            return _context.Car_Details.Any(e => e.Car_Id == id);
        }

        

    }
}
