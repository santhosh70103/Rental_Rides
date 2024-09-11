namespace Rental_Rides.IRepo
{
    public interface IReturnService
    {
        Task<int> ReturnCarAsync(string email, DateTime actualReturnDate);
        
    }
}
