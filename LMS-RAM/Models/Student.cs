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
		[Display(Name="Personnummer")]
        public string SSN { get; set; }

        [Required]
		[Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required]
		[Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

    }
}