using Rental_Rides.DTO_Models;

namespace Rental_Rides.IRepo
{
    public interface IRentalService
    {
        Task<RentalDetailsDTO> GetRentalDetailsByRentalIdAsync(int orderId);
    }
}
