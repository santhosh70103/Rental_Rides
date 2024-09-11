using Microsoft.AspNetCore.Mvc;
using Rental_Rides.IRepo;
using Rent_Rides.Models;
using System.Threading.Tasks;

namespace Rental_Rides.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCarWithFeeedBack : ControllerBase
    {
        private readonly ICarService _carService;

        public GetCarWithFeeedBack(ICarService carService)
        {
            _carService = carService;
        }

       
        [HttpGet]
        public async Task<ActionResult<CarDetailsWithFeedbackDTO>> GetCarDetailsWithFeedback()
        {
            var carDetails = await _carService.GetAllCarsWithFeedbackAsync();

            if (carDetails == null)
            {
                return NotFound("Car details not found.");
            }

            return Ok(carDetails);
        }
    }
}
