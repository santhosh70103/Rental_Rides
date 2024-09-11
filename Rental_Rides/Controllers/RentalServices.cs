using Microsoft.AspNetCore.Mvc;
using Rental_Rides.DTO_Models;
using Rental_Rides.IRepo;
using System.Threading.Tasks;

namespace Rental_Rides.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalServices : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalServices(IRentalService rentalService)
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
    }
}
