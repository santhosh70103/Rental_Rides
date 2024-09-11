using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentrides_hexa.DTO_Models;
using Rentrides_hexa.IRepo;

namespace Rentrides_hexa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCarWithFeedBack : ControllerBase
    {
        private readonly ICarService _carService;

        public GetCarWithFeedBack(ICarService carService)
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
