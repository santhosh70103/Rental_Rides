using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rental_Rides.Models
{ 
[Table("Rented_Car")] // Maps the class to the Rented_Car table
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

    public int Status { get; set; }

    // Navigation Properties
    public virtual Car_Details Car_Details { get; set; }
    public virtual Customers Customer { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    public virtual ICollection<Returned_Car> Returned_Car { get; set; } = new HashSet<Returned_Car>();
}
}
