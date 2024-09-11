using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rentrides_hexa.Model
{
    [Table("Refund")]
    public class Refund
    {
        [Key]

        public int Refund_Id { get; set; }


        [ForeignKey("Rented_Car")]
        public int Rental_Id { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Refund_Price { get; set; }

        [Range(0, 1)]
        public int Refund_Status { get; set; }

        // Navigation Property
        [ValidateNever]
        [JsonIgnore]
        public virtual Rented_Car Rented_Car { get; set; }
    }
}
