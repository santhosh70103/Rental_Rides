using System.Collections.Generic;

namespace Rent_Rides.Models
{
    public class CarDetailsWithFeedbackDTO
    {
        public int Car_Id { get; set; }
        public string Car_Name { get; set; }
        public string Car_Type { get; set; }
        public string Fuel_Type { get; set; }
        public decimal? Price_Per_Hour{ get; set; }
        public string Transmission_Type { get; set; }
        public decimal? Price_Per_Day { get; set; }
        public int? Available_Cars { get; set; }
        public decimal? Penalty { get; set; }
        public string Available_Location { get; set; }
        public string Car_Image { get; set; }
        public int? Number_Of_Seats { get; set; }
        public IEnumerable<UserFeedbackDTO> Feedback { get; set; }
    }

    public class UserFeedbackDTO
    {
        public string Feedback_Query { get; set; }
        public int? Feedback_Point { get; set; }
    }
}
