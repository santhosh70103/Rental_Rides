﻿using Rental_Rides.IRepo;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rental_Rides.DTO_Models;
using Rental_Rides.Models;

namespace Rental_Rides.Services
{
    public class RentalService : IRentalService
    {
        private readonly CarRentalDbContext _context;

        public RentalService(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<RentalDetailsDTO> GetRentalDetailsByRentalIdAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Order_Id == orderId);
            var rentalDetails = await (from Order in _context.Orders
                                       join Rented_Car in _context.Rented_Cars on Order.Rental_Id equals Rented_Car.Rental_Id
                                       join Returned_Car in _context.Returned_Cars on Rented_Car.Rental_Id equals Returned_Car.Rental_Id into returnedCarGroup
                                       from Returned_Car in returnedCarGroup.DefaultIfEmpty() // Left join to handle non-returned cars
                                       where Order.Rental_Id == order.Rental_Id
                                       select new RentalDetailsDTO
                                       {
                                           Rental_ID = Order.Rental_Id,
                                           Rent_Start_Date =Rented_Car.Rented_Date,
                                           Rent_End_Date = Rented_Car.Expected_Return_Date,
                                           Car_Name = Rented_Car.Car_Details.Car_Name, // Assuming Car_Details has a navigation property
                                           Penalty = Returned_Car.Penalty,
                                           Return_Date = Returned_Car.Actual_Return_Date,
                                           Status = Rented_Car.Status
                                       }).FirstOrDefaultAsync();

            if (rentalDetails == null)
            {
                throw new Exception("Rental details not found for the given Rental_ID");
            }

            return rentalDetails;
        }
    }
}