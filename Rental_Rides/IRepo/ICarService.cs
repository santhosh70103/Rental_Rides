using Rent_Rides.Models;
using Rental_Rides.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental_Rides.IRepo
{
    public interface ICarService
    {
        Task<IEnumerable<Car_Details>> GetCarsByFiltersAsync(string fuelType, string transmissionType, decimal? minPrice, decimal? maxPrice, int? seats);//Filter

        Task<IEnumerable<CarDetailsWithFeedbackDTO>> GetAllCarsWithFeedbackAsync();//GetCar with Feedbaack
    }
}
