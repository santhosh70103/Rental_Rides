using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rentrides_hexa.Model
{
    [Table("Customers")]
    public class Customers
    {
        [Key]

        public int Customer_Id { get; set; }


        [Required]
        [StringLength(25)]
        public string Customer_Name { get; set; }

        [Required]
        [StringLength(25)]
        public string Customer_Email { get; set; }

        [StringLength(25)]
        public string Customer_PhoneNo { get; set; }

        [Required]
        [StringLength(25)]
        public string Customer_Password { get; set; }

        // Navigation properties
        [ValidateNever]
        [JsonIgnore]
        public virtual ICollection<User_Feedback> User_Feedback { get; private set; } = new HashSet<User_Feedback>();
        [ValidateNever]
        [JsonIgnore]
        public virtual ICollection<Rented_Car> Rented_Car { get; private set; } = new HashSet<Rented_Car>();
    }
}
