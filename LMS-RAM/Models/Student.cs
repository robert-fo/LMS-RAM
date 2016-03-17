using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 13 tecken")]
        [Display(Name = "Personnummer")]
        public string SSN { get; set; }

        [Required]
        [Display(Name = "Förnamn")]
        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternamn")]
        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        public string LastName { get; set; }
    }
}