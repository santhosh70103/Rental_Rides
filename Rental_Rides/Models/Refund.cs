using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rental_Rides.Models
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

        public int Refund_Status { get; set; }

        // Navigation Property
        public virtual Rented_Car Rented_Car { get; set; }  
    }
}