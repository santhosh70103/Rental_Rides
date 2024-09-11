using Rentrides_hexa.DTO_Models;

namespace Rentrides_hexa.IRepo
{
    public interface IRentalService
    {
        Task<RentalDetailsDTO> GetRentalDetailsByRentalIdAsync(int rentalId);
    }
}
