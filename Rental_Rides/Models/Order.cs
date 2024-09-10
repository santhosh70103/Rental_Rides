using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace Rental_Rides.Models
{
    [Table("Orders")]
    public class Order
    {

        [Key]

        public int Order_Id { get; set; }

        [ForeignKey("Rented_Car")]
        public int? Rental_Id { get; set; }

        [ForeignKey("Car_Details")]
        public int? Car_Id { get; set; }

        [Range(1, 4)]
        public int? Order_Status { get; set; }

        // Navigation Property
        [ValidateNever]
        [JsonIgnore]
        public virtual Rented_Car Rented_Car { get; set; }
    }
}
