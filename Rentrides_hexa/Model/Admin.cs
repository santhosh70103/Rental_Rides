using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentrides_hexa.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]

        public int Admin_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Admin_Name { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(25)]

        public string Admin_Email { get; set; } = "";

        [Phone]
        [StringLength(25)]
        public string Admin_PhoneNo { get; set; } = "";

        [Required]
        [StringLength(10)]
        public string Admin_Password { get; set; } = "";
    }
}
