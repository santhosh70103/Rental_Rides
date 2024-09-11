using Rent_Rides.Controllers;

namespace Rental_Rides.IRepo
{
    public interface IBookingService
    {
        Task<bool> BookCarAsync(int customerId, int carId, int rentalDays);
        
    }
}
