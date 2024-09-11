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

    public async Task<IEnumerable<IGrouping<int?, Order>>> GetOrdersByCustomerIdAsync(int customerId)
    {
        var orders = await _context.Orders
            .Where(o => o.Customer_ID == customerId)
            .GroupBy(o => o.Customer_ID)
            .ToListAsync();

        return orders;
    }
}
