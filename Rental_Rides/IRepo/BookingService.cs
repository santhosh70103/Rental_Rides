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

        public async Task<bool> BookCarAsync(int customerId, int carId, int rentalDays)
        {
            // Check if the car is available
            var car = await _context.Car_Details.FirstOrDefaultAsync(c => c.Car_Id == carId && c.Available_Cars > 0);

            if (car == null)
            {
                return false; // Car is not available
            }

            // Create the rental entry
            var rentedCar = new Rented_Car
            {
                Customer_ID = customerId,
                Car_Id = carId,
                Rented_Date = DateTime.UtcNow,
                Expected_Return_Date = DateTime.UtcNow.AddDays(rentalDays),
                Total_Price = rentalDays * car.Rental_Price_PerDay,
                Penalty_PerDay = car.Penalty_Amt,
                Payment_Status="Pending",
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
                // Assuming 1 is for "active"
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Create a payment entry
            var payment = new Payment
            {
                Rental_Id = rentedCar.Rental_Id,
                Total_Amount = rentedCar.Total_Price,
                Payment_Type="Card",
                Payment_Status="1"
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Update car availability
            car.Available_Cars -= 1;
            _context.Car_Details.Update(car);
            await _context.SaveChangesAsync();
                
            return true;
        }
    }
}
