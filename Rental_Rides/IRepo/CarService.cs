﻿using Microsoft.EntityFrameworkCore;
using Rent_Rides.Models;
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

        public async Task<IEnumerable<CarDetailsWithFeedbackDTO>> GetAllCarsWithFeedbackAsync()
        {
            var cars = await _context.Car_Details
                .Select(c => new CarDetailsWithFeedbackDTO
                {
                    Car_Id = c.Car_Id,
                    Car_Name = c.Car_Name,
                    Car_Type = c.Car_Type,
                    Fuel_Type = c.Fuel_Type,
                    Price_Per_Hour=c.Rental_Price_PerHour,
                    Available_Cars=c.Available_Cars,
                    Available_Location=c.Available_Location,
                    Transmission_Type = c.Transmission_type,
                    Price_Per_Day = c.Rental_Price_PerDay,
                    Car_Image=c.Car_Image,
                    Number_Of_Seats = c.No_of_seats,
                    Feedback = _context.User_Feedbacks
                        .Where(f => f.Car_Id == c.Car_Id)
                        .Select(f => new UserFeedbackDTO
                        {
                            Feedback_Query = f.Feedback_Query,
                            Feedback_Point = f.Feedback_Point
                        })
                        .ToList()
                })
                .ToListAsync();

            return cars;
        }

    }
}
