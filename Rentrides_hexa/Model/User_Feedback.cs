﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rentrides_hexa.Model
{
    [Table("User_Feedback")]
    public class User_Feedback
    {
        [Key]

        public int Feedback_Id { get; set; }


        [ForeignKey("Customer")]
        public int? Customer_Id { get; set; }

        [ForeignKey("Car_Details")]
        public int? Car_Id { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Feedback_Query { get; set; }

        [Range(0, 5)]
        public int? Feedback_Point { get; set; }

        // Navigation Properties
        [ValidateNever]
        [JsonIgnore]
        public virtual Car_Details Car_Details { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public virtual Customers Customer { get; set; }
    }
}