using Microsoft.AspNetCore.Mvc;
using Rental_Rides.IRepo;
using System;
using System.Threading.Tasks;

namespace Rental_Rides.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnServiceController : ControllerBase
    {
        private readonly IReturnService _returnService;

        public ReturnServiceController(IReturnService returnService)
        {
            _returnService = returnService;
        }

        [HttpPost("{rentalId}")]
        public async Task<IActionResult> ReturnCar(int rentalId)
        {
            DateTime date = new DateTime(2024, 9, 16, 10, 30, 0); 
            var result = await _returnService.ReturnCarAsync(rentalId, date);

            if (!result)
            {
                return NotFound(); // Or appropriate error response
            }

            return NoContent();
        }
    }
}
