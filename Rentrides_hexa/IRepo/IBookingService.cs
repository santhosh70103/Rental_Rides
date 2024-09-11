namespace Rentrides_hexa.IRepo
{
    public interface IBookingService
    {
        Task<bool> BookCarAsync(int customerId, int carId, int rentalDays);
    }
}
