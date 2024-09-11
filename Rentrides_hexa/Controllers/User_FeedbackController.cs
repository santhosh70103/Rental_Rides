using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentrides_hexa.Model;

namespace Rentrides_hexa.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User_Feedback>>> GetUser_Feedbacks()
        {
            return await _context.User_Feedbacks.ToListAsync();
        }

        // GET: api/User_Feedback/5
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
        [HttpPost]
        public async Task<ActionResult<User_Feedback>> PostUser_Feedback(User_Feedback user_Feedback)
        {
            _context.User_Feedbacks.Add(user_Feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_Feedback", new { id = user_Feedback.Feedback_Id }, user_Feedback);
        }

        // DELETE: api/User_Feedback/5
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

        private bool User_FeedbackExists(int id)
        {
            return _context.User_Feedbacks.Any(e => e.Feedback_Id == id);
        }
    }
}
