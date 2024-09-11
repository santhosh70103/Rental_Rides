using Rentrides_hexa.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rentrides_hexa.DTO_Models;


namespace Rentrides_hexa.IRepo
{
    public interface ICarService
    {
        Task<IEnumerable<Car_Details>> GetCarsByFiltersAsync(string fuelType, string transmissionType, decimal? minPrice, decimal? maxPrice, int? seats);//Filter

        Task<IEnumerable<CarDetailsWithFeedbackDTO>> GetAllCarsWithFeedbackAsync();//GetCar with Feedbaack

    }
}
