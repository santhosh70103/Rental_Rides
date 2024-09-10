namespace Rental_Rides.DTO_Models
{
    public class BookingRequestDto
    {
        public int Car_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Days_Of_Rental { get; set; }
    }
}
