namespace Rental_Rides.IRepo
{
    public interface IReturnService
    {
        Task<int> ReturnCarAsync(string email,int orderId, DateTime actualReturnDate);
        
    }
}
