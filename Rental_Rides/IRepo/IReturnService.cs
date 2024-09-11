namespace Rental_Rides.IRepo
{
    public interface IReturnService
    {
        Task<bool> ReturnCarAsync(int rentalId, DateTime actualReturnDate);
        
    }
}
