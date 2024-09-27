using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental_Rides.DTO_Models;
using Rental_Rides.IRepo; // Ensure this namespace is correct
using System.Threading.Tasks;

namespace Rent_Rides.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // POST api/booking
        
        [HttpPost]
        //[Authorize(Roles ="Customer")]
        public async Task<IActionResult> BookCar([FromBody] BookingRequestDto bookingRequest)
        {
            if (bookingRequest == null)
            {
                return BadRequest("Invalid booking request.");
            }

            try
            {
                int isBooked = await _bookingService.BookCarAsync(bookingRequest.Car_Id, bookingRequest.Customer_Id,bookingRequest.Days_Of_Rental,bookingRequest.date);

                if (isBooked==100)
                {
                    return Ok("Car booked successfully.");
                }
                else if(isBooked==1)
                {
                    return StatusCode(500, "More Than 2 Orders are reserved"); 
                }
                else if(isBooked==2)
                {
                    return StatusCode(500, "customer Not Found");
                }
                else if(isBooked==4)
                {
                    return StatusCode(500, "car has to be picked up within 3 days");
                }
                else
                {
                    return StatusCode(500, "3");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("Cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            int result = await _bookingService.CancelOrderAsync(orderId);
            if(result==100)
            {
                return StatusCode(200, "Order SuccesFull");
            }
            if (result==1)
            {
                return NotFound("Order not found or could not be cancelled.");
            }
            else 
            {
                return NotFound("Rented details not found");
            }
        }
    }
}
