using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Rental_Rides.IRepo;
using Rental_Rides.Models;

namespace Rental_Rides.Services
{
    public class ReturnService : IReturnService
    {
        private readonly CarRentalDbContext _context;

        public ReturnService(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<int> ReturnCarAsync(string email, int orderId, DateTime actualReturnDate)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Customer_Email == email);

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Customer_ID == customer.Customer_Id && o.Order_Status == 2 && o.Order_Id == orderId);
            var rentedCar = await _context.Rented_Cars.FirstOrDefaultAsync(rc => rc.Rental_Id == order.Rental_Id);

            if (customer == null)
            {
                return 1; // Customer not found
            }
            if (order == null)
            {
                return 2; // Order not found or not in the correct status
            }
            if (rentedCar == null)
            {
                return 3; // Rented car not found
            }

            // Calculate penalty if actual return date is later than expected return date
            decimal penaltyAmount = 0;
            if (actualReturnDate > rentedCar.Expected_Return_Date)
            {
                var daysLate = (actualReturnDate - rentedCar.Expected_Return_Date.Value).Days;
                penaltyAmount = daysLate * (rentedCar.Penalty_PerDay ?? 0);
            }

            var payment = new Payment
            {
                Rental_Id = rentedCar.Rental_Id,
                Payment_Type = "Penalty",
                Total_Amount = penaltyAmount,
                Payment_Status = "1" // or "Completed" based on your logic
            };

            // Create and add/update Payment record if penalty is incurred
            if (penaltyAmount > 0)
            {
                _context.Payments.Add(payment);
            }

            // Update the Rented_Car table
            if (penaltyAmount > 0 && payment.Payment_Status != "1")
            {
                rentedCar.Status = 3; // Pending
                order.Order_Status = 3; // Pending
            }
            else
            {
                rentedCar.Status = 4; // Completed
                order.Order_Status = 4; // Completed
            }

            // Car returned
            _context.Rented_Cars.Update(rentedCar);

            // Create and add a Returned_Car record
            var returnedCar = new Returned_Car
            {
                Rental_Id = rentedCar.Rental_Id,
                Actual_Return_Date = actualReturnDate,
                Difference_In_Days = penaltyAmount > 0 ? (actualReturnDate - rentedCar.Expected_Return_Date.Value).Days : (int?)null,
                Penalty = penaltyAmount
            };

            _context.Returned_Cars.Add(returnedCar);

            var carDetails = await _context.Car_Details.FirstOrDefaultAsync(c => c.Car_Id == rentedCar.Car_Id);

            carDetails.Available_Cars += 1; // Increment available cars

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log error, return status code)
                return 4; // Error occurred during saving
            }

            return 100; // Success
        }
    }
}
    
