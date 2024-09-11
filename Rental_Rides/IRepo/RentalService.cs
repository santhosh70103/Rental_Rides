using Rental_Rides.IRepo;

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
                                       from Returned_Car in returnedCarGroup.DefaultIfEmpty() 
                                       where Order.Rental_Id == order.Rental_Id
                                       select new RentalDetailsDTO
                                       {
                                           Rental_ID = Order.Rental_Id,
                                           Rent_Start_Date =Rented_Car.PickUp_Date,
                                           Rent_End_Date = Rented_Car.Expected_Return_Date,
                                           Car_Name = Rented_Car.Car_Details.Car_Name, 
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

        public async Task<int> RentCarAsync(string email)
        {
            // Fetch customer by email
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Customer_Email == email);

            if (customer == null)
            {
                return 1; // Customer isnot found
            }

            // Fetch the orders for the customer with status = 1 reserved
            var orders = await _context.Orders
                .Where(o => o.Customer_ID == customer.Customer_Id && o.Order_Status == 1)
                .ToListAsync();

            if (!orders.Any())
            {
                return 2; // No pending orders found
            }

            // Assume we are processing the first pending order for simplicity
            var order = orders.First();
            var rentedCar = await _context.Rented_Cars
                .FirstOrDefaultAsync(rc => rc.Rental_Id == order.Rental_Id);

            if (rentedCar == null)
            {
                return 3; // Rented car not found
            }

            // Update order status to rented (2)
            order.Order_Status = 2;
            _context.Orders.Update(order);

            // Update rented car status to rented (2)
            rentedCar.Status = 2;
            _context.Rented_Cars.Update(rentedCar);

            // Check payment status
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Rental_Id == rentedCar.Rental_Id);

            if (payment == null || payment.Payment_Status != "1")
            {
                return 4; // Payment not found or not successful
            }

            // Save changes
            await _context.SaveChangesAsync();

            return 100; // Successfully rented
        }
    }
}
