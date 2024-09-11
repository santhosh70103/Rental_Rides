namespace Rental_Rides.IRepo
{
    public interface IReturnService
    {
        Task<bool> ReturnCarAsync(string email, DateTime actualReturnDate);
        
    }
}
