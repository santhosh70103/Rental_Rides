﻿using Rental_Rides.Models;

namespace Rental_Rides.IRepo
{
    public interface IOrderService
    {
        Task<IEnumerable<IGrouping<int?, Order>>> GetOrdersByCustomerIdAsync(int customerId);
    }
}
