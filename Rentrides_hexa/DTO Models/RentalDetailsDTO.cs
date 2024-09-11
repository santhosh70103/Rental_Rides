namespace Rentrides_hexa.DTO_Models
{
    public class RentalDetailsDTO
    {
        public int? Rental_ID { get; set; }
        public DateTime? Rent_Start_Date { get; set; }
        public DateTime? Rent_End_Date { get; set; }
        public string Car_Name { get; set; }
        public decimal? Penalty { get; set; }
        public DateTime? Return_Date { get; set; }
        public int Status { get; set; }
    }
}
