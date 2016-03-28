using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [StringLength(13, ErrorMessage = "Max 13 tecken")]
        [Required, Display(Name = "Personnr")]
        public String SSN { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Required, Display(Name = "Förnamn")]
        public String FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Required, Display(Name = "Efternamn")]
        public String LastName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

    }
}