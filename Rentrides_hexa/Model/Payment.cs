using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rentrides_hexa.Model
{
    [Table("Payment")]
    public class Payment
    {
        [Key]

        public int Payment_Id { get; set; }

        [ForeignKey("Rented_Car")]
        public int? Rental_Id { get; set; }

        [StringLength(20)]
        public string Payment_Type { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Total_Amount { get; set; }

        [Range(0, 1)]
        public string Payment_Status { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public virtual Rented_Car Rented_Car { get; set; }
    }
}
