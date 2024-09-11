using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentrides_hexa.IRepo;
using Rentrides_hexa.Model;

namespace Rentrides_hexa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService) => _carService = carService;

        // GET: api/Car
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Car_Details>>> GetCarsByFilters(
            [FromQuery] string fuelType = null,
            [FromQuery] string transmissionType = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] int? seats = null)
        {
            var cars = await _carService.GetCarsByFiltersAsync(fuelType, transmissionType, minPrice, maxPrice, seats);

            if (cars == null || !cars.Any())
            {
                return NotFound("No cars found matching the filters.");
            }

            return Ok(cars);
        }

    }
}
