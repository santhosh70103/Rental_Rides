using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rental_Rides.Models
{
[Table("Car_Details")]
public class Car_Details
{

    [Key]

    public int Car_Id { get; set; }


    [Required]
    [StringLength(50)]
    public string Car_Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Car_Type { get; set; }

    [Required]
    public int? Car_Model_Year { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Rental_Price_PerHour { get; set; }

    [Required]
    [StringLength(100)]
    public string? Car_Image { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Rental_Price_PerDay { get; set; }

    [Required]
    public int? Available_Cars { get; set; }

    [StringLength(100)]
    public string Available_Location { get; set; }

    [StringLength(20)]
    public string Fuel_Type { get; set; }

    public int? No_of_seats { get; set; }

    [StringLength(20)]
    public string Transmission_type { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Penalty_Amt { get; set; }


    // Navigation properties
    public virtual ICollection<User_Feedback> User_Feedback { get; set; } = new HashSet<User_Feedback>();
    public virtual ICollection<Rented_Car> Rented_Car { get; set; } = new HashSet<Rented_Car>();
}
}