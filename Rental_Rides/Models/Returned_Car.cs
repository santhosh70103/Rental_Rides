using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rental_Rides.Models
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
        public virtual Rented_Car Rented_Car { get; set; }
    }
}
