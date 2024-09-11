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

        public async Task<bool> ReturnCarAsync(int rentalId, DateTime actualReturnDate)
        {
            var returned_Car = await _context.Returned_Cars.FirstOrDefaultAsync(rc=> rc.Rental_Id == rentalId);

            if(returned_Car != null )
            {
                return false;
            }
            var rentedCar = await _context.Rented_Cars
                .Include(rc => rc.Car_Details)
                .FirstOrDefaultAsync(rc => rc.Rental_Id == rentalId);

            if (rentedCar == null)
            {
                return false; // Car not found
            }

            // Calculate penalty if actual return date is later than expected return date
            decimal penaltyAmount = 0;
            if (actualReturnDate > rentedCar.Expected_Return_Date)
            {
                var daysLate = (actualReturnDate - rentedCar.Expected_Return_Date.Value).Days;
                penaltyAmount = daysLate * (rentedCar.Penalty_PerDay?? 0);
            }


            var payment = new Payment
            {
                Rental_Id = rentalId,
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
            if (penaltyAmount > 0 && payment.Payment_Status == "1")

            {
                rentedCar.Status = 3;//pending
            }
            else 
            {
                rentedCar.Status = 4;//Completed
            }
            // Car returned
            _context.Rented_Cars.Update(rentedCar);

            var OrderDet = await _context.Orders.FirstOrDefaultAsync(o => o.Rental_Id == rentalId);
            OrderDet.Order_Status = 4;


            // Create and add a Returned_Car record
            var returnedCar = new Returned_Car
            {
                Rental_Id = rentalId,
                Actual_Return_Date = actualReturnDate,
                Difference_In_Days = penaltyAmount > 0 ? (actualReturnDate - rentedCar.Expected_Return_Date.Value).Days : (int?)null,
                Penalty = penaltyAmount
            };

            _context.Returned_Cars.Add(returnedCar);

            var CarDetails= await _context.Car_Details.FirstOrDefaultAsync(c => c.Car_Id == rentedCar.Car_Id );

            CarDetails.Available_Cars+=1;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log error, return status code)
                return false;
            }

            return true;
        }
    }
}
