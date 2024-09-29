using Microsoft.EntityFrameworkCore;
using Rental_Rides.IRepo;
using Rental_Rides.Models;

public class OrderService : IOrderService
{
    private readonly CarRentalDbContext _context;

    public OrderService(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IGrouping<int?, Order>>> GetOrdersByEmailAsync(string email)
    {
        // Get the Customer ID based on the provided email
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Customer_Email == email);

        if (customer == null)
        {
            // Handle the case where the customer is not found
            return Enumerable.Empty<IGrouping<int?, Order>>();
        }

        var orders = await _context.Orders
            .Where(o => o.Customer_ID == customer.Customer_Id)
            .GroupBy(o => o.Customer_ID)
            .ToListAsync();

        return orders;
    }
}
