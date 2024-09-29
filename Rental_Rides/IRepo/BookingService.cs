using Microsoft.EntityFrameworkCore;
using Rental_Rides.Models;

namespace Rental_Rides.IRepo
{
    public class BookingService : IBookingService
    {
        private readonly CarRentalDbContext _context;

        public BookingService(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<int> BookCarAsync(int carId, int customerId, int rentalDays, DateTime date)
        {

            var customerIsExist = await _context.Customers.FirstOrDefaultAsync(c => c.Customer_Id == customerId);
            var count = await _context.Rented_Cars.Where(r => r.Customer_ID == customerId && (r.Status != 5 && r.Status != 4)).CountAsync();

            if (date >= DateTime.Now.AddDays(3))
            {
                return 4;
            }

            if (count > 1)
            {
                return 1;
            }
            if (customerIsExist == null)
            {
                return 2;
            }
            // Check if the car is available
            var car = await _context.Car_Details.FirstOrDefaultAsync(c => c.Car_Id == carId && c.Available_Cars > 0);

            if (car == null)
            {
                return 3; // Car is not available
            }

            // Create the rental entry
            var rentedCar = new Rented_Car
            {
                Customer_ID = customerId,
                Car_Id = carId,
                PickUp_Date = date,
                Expected_Return_Date = date.AddDays(rentalDays),
                Total_Price = rentalDays * car.Rental_Price_PerDay,
                Penalty_PerDay = car.Penalty_Amt,
                Payment_Status = "1",
                Days_of_Rent = rentalDays,
                Status = 1 // Assuming 1 is for "rented"
            };

            _context.Rented_Cars.Add(rentedCar);
            await _context.SaveChangesAsync();

            // Create an order entry
            var order = new Order
            {
                Rental_Id = rentedCar.Rental_Id,  // Associate the newly created rented car
                Car_Id = carId,
                Order_Status = 1,//pending
                Total_Price = rentedCar.Total_Price,
                Customer_ID = rentedCar.Customer_ID
                // Assuming 1 is for "active"
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Create a payment entry
            var payment = new Payment
            {
                Rental_Id = rentedCar.Rental_Id,
                Total_Amount = rentedCar.Total_Price,
                Payment_Type = "Normal",
                Payment_Status = "1"
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Update car availability
            car.Available_Cars -= 1;
            _context.Car_Details.Update(car);
            await _context.SaveChangesAsync();

            return 100;
        }


        public async Task<int> CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Rented_Car)
                .FirstOrDefaultAsync(o => o.Order_Id == orderId);

            if (order == null || order.Order_Status == 4 || order.Order_Status == 3)
            {
                return 1; // Order not found or already completed/penalty pending
            }

            var rentedCar = await _context.Rented_Cars
                .FirstOrDefaultAsync(rc => order.Rental_Id == rc.Rental_Id);

            if (rentedCar == null)
            {
                return 2; // Rented car not found
            }

            // Update Rented_Car status to 5 (Cancelled)
            rentedCar.Status = 5;
            order.Order_Status = 5;
            _context.Rented_Cars.Update(rentedCar);

            var car = await _context.Car_Details.FirstOrDefaultAsync(c => c.Car_Id == rentedCar.Car_Id);
            car.Available_Cars++;

            // Calculate the difference in days between Rent_StartDate and the current date
            var rentStartDate = rentedCar.PickUp_Date;
            var currentDate = DateTime.Now;

            // Calculate days between now and Rent_StartDate
            int daysRented = (currentDate - rentStartDate.Value).Days;

            // If rent start date is in the future, treat it as 0 days
            if (daysRented < 0)
            {
                daysRented = 0;
            }

            decimal? refundAmount = rentedCar.Total_Price; // Default full refund
            decimal reductionRate = 0.05m; // 5% reduction per day

            // Apply a 5% reduction for each day (up to 3 days)
            if (daysRented >= 1 && daysRented <= 3)
            {
                decimal reductionPercentage = daysRented * reductionRate; // 5% per day
                refundAmount = rentedCar.Total_Price * (1 - reductionPercentage); // Adjust refund amount
            }
            else if (daysRented > 3)
            {
                decimal reductionPercentage = 4 * reductionRate;
                refundAmount = rentedCar.Total_Price * (1 - reductionPercentage); // No refund after 3 days
            }

            // Check if payment was successful
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Rental_Id == order.Rental_Id && p.Payment_Status == "1");

            if (payment != null)
            {
                // Create refund data
                var refund = new Refund
                {
                    Rental_Id = order.Rental_Id,
                    Refund_Price = refundAmount, // Adjusted refund amount
                    Refund_Status = 0
                };

                await _context.Refunds.AddAsync(refund);

                // Update the payment status to refunded
                var newPayment = new Payment
                {
                    Rental_Id = order.Rental_Id,
                    Payment_Type = "Refund",
                    Total_Amount = refundAmount,
                    Payment_Status = "1"
                };
                _context.Payments.Update(newPayment);
            }

            // Save all changes
            await _context.SaveChangesAsync();

            return 100; // Success
        }


    }
}


