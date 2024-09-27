using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rent_Rides.Models;
using Rental_Rides.DTO_Models;
using Rental_Rides.Models;

namespace Rental_Rides.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class User_FeedbackController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public User_FeedbackController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/User_Feedback
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<User_Feedback>>> GetUser_Feedbacks()
        {
            return await _context.User_Feedbacks.ToListAsync();
        }

        // GET: api/User_Feedback/5
        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User_Feedback>> GetUser_Feedback(int id)
        {
            var user_Feedback = await _context.User_Feedbacks.FindAsync(id);

            if (user_Feedback == null)
            {
                return NotFound();
            }

            return user_Feedback;
        }

        // PUT: api/User_Feedback/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser_Feedback(int id, User_Feedback user_Feedback)
        {
            if (id != user_Feedback.Feedback_Id)
            {
                return BadRequest();
            }

            _context.Entry(user_Feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_FeedbackExists(id))
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

        // POST: api/User_Feedback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        
        public async Task<ActionResult<User_Feedback>> PostUser_Feedback(User_Feedback user_Feedback)
        {
            _context.User_Feedbacks.Add(user_Feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_Feedback", new { id = user_Feedback.Feedback_Id }, user_Feedback);
        }

        // DELETE: api/User_Feedback/5
        //[Authorize(Roles = "Admin")]
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser_Feedback(int id)
        {
            var user_Feedback = await _context.User_Feedbacks.FindAsync(id);
            if (user_Feedback == null)
            {
                return NotFound();
            }

            _context.User_Feedbacks.Remove(user_Feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("add")]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult> AddFeedback( int rentalId,  userFeedbackDTO userFeedback)
        {
            if (rentalId <= 0)
            {
                return BadRequest("Invalid rental ID.");
            }

            var rented_car = await _context.Rented_Cars.FirstOrDefaultAsync(rc=>rc.Rental_Id==rentalId );
            var car = await _context.Car_Details.FirstOrDefaultAsync(c=> c.Car_Id == rented_car.Car_Id);

            if(rented_car ==null ||  car == null)
            {
                return BadRequest("Car or Rent Not found");
            }

            var NewFeedback = new User_Feedback
            {
                Customer_Id=rented_car.Customer_ID,
                Car_Id=rented_car.Car_Id,
                Feedback_Point=userFeedback.Points,
                Feedback_Query=userFeedback.Query

            };

            _context.User_Feedbacks.Add(NewFeedback);
            await _context.SaveChangesAsync();

            return Ok("Feedback submitted successfully.");
        }
        private bool User_FeedbackExists(int id)
        {
            return _context.User_Feedbacks.Any(e => e.Feedback_Id == id);
        }
    }
}
