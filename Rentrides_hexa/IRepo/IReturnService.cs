namespace Rentrides_hexa.IRepo
{
    public interface IReturnService
    {
        Task<bool> ReturnCarAsync(int rentalId, DateTime actualReturnDate);
    }
}
