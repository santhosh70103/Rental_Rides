using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentrides_hexa.DTO_Models;
using Rentrides_hexa.IRepo;

namespace Rentrides_hexa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalByOrder : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalByOrder(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        // GET: api/Rental/5
        [HttpGet("{rentalId}")]
        public async Task<ActionResult<RentalDetailsDTO>> GetRentalDetails(int rentalId)
        {
            var rentalDetails = await _rentalService.GetRentalDetailsByRentalIdAsync(rentalId);

            if (rentalDetails == null)
            {
                return NotFound("Rental details not found.");
            }

            return Ok(rentalDetails);
        }
    }
}
