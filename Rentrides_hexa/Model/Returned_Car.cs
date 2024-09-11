using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rentrides_hexa.Model
{
    [Table("Returned_Car")]
    public class Returned_Car
    {
        [Key]

        public int Return_Id { get; set; }

        [ForeignKey("Rented_Car")]
        public int? Rental_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Actual_Return_Date { get; set; }

        [Column(TypeName = "int")]
        public int? Difference_In_Days { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Penalty { get; set; }

        // Navigation Property
        [ValidateNever]
        [JsonIgnore]
        public virtual Rented_Car Rented_Car { get; set; }
    }
}
