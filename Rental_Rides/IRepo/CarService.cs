using Microsoft.EntityFrameworkCore;
using Rental_Rides.IRepo;
using Rental_Rides.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental_Rides.Services
{
    public class CarService : ICarService
    {
        private readonly CarRentalDbContext _context;

        public CarService(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car_Details>> GetCarsByFiltersAsync(string fuelType, string transmissionType, decimal? minPrice, decimal? maxPrice, int? seats)
        {
            var query = _context.Car_Details.AsQueryable();

            if (!string.IsNullOrEmpty(fuelType))
            {
                query = query.Where(car => car.Fuel_Type == fuelType);
            }

            if (!string.IsNullOrEmpty(transmissionType))
            {
                query = query.Where(car => car.Transmission_type == transmissionType);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(car => car.Rental_Price_PerDay >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(car => car.Rental_Price_PerDay <= maxPrice.Value);
            }

            if (seats.HasValue)
            {
                query = query.Where(car => car.No_of_seats == seats.Value);
            }

            return await query.ToListAsync();
        }
    }
}
