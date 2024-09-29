using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental_Rides.DTO_Models;
using Rental_Rides.IRepo;
using System.Threading.Tasks;

namespace Rental_Rides.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalServiceController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalServiceController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        // GET: api/Rental/5
        [HttpGet("{OrderID}")]

        public async Task<ActionResult<RentalDetailsDTO>> GetRentalDetails(int OrderID)
        {
            var rentalDetails = await _rentalService.GetRentalDetailsByRentalIdAsync(OrderID);

            if (rentalDetails == null)
            {
                return NotFound("Rental details not found.");
            }

            return Ok(rentalDetails);
        }

        //[Authorize(Roles ="Customer")]
        [HttpPost("rent/{email}")]
        public async Task<ActionResult> RentCar(string email)
        {
            int result = await _rentalService.RentCarAsync(email);
            if(result == 100)
            {
                return Ok("Car rented successfully.");
            }
            if (result == 1)
            {
                return BadRequest("Customer Not Found ");
            }
            else if (result == 2) 
            {
                return BadRequest("Order Not Found with pending status");
            }
            else if (result == 3)
            {
                return BadRequest("Rented_car Not Found");
            }
            else
            {
                return BadRequest("Payment Is pending");
            }

        }
    }
}
