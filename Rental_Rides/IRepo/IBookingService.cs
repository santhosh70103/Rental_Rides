using Rent_Rides.Controllers;

namespace Rental_Rides.IRepo
{
    public interface IBookingService
    {
        Task<int> BookCarAsync(int customerId, int carId, int rentalDays,DateTime date);
        Task<bool> CancelOrderAsync(int orderId);

    }
}
