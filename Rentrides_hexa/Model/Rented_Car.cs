using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rentrides_hexa.Model
{
    [Table("Rented_Car")]
    public class Rented_Car
    {
        [Key]

        public int Rental_Id { get; set; }


        [ForeignKey("Car_Details")]
        public int? Car_Id { get; set; }

        [ForeignKey("Customer")]
        public int? Customer_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Rented_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Expected_Return_Date { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal? Total_Price { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Penalty_PerDay { get; set; }

        [StringLength(20)]
        public string Payment_Status { get; set; }

        public int? Days_of_Rent { get; set; }

        [Range(1, 5)]
        public int Status { get; set; }

        // Navigation Properties
        [ValidateNever]
        [JsonIgnore]
        public virtual Car_Details Car_Details { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public virtual Customers Customer { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        [ValidateNever]
        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        [ValidateNever]
        [JsonIgnore]
        public virtual ICollection<Returned_Car> Returned_Car { get; set; } = new HashSet<Returned_Car>();

    }
}
