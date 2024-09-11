﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> BookCar([FromBody] BookingRequestDto bookingRequest)
        {
            if (bookingRequest == null)
            {
                return BadRequest("Invalid booking request.");
            }

            try
            {
                bool isBooked = await _bookingService.BookCarAsync(bookingRequest.Customer_Id,bookingRequest.Car_Id,bookingRequest.Days_Of_Rental);

                if (isBooked)
                {
                    return Ok("Car booked successfully.");
                }
                else
                {
                    return StatusCode(500, "An error occurred while processing your request."); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}