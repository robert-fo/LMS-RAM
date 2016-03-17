using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name="Personnr")]
        public String SSN { get; set; }
        [Required, Display(Name = "Förnamn")]
        public String FirstName { get; set; }
        [Required, Display(Name = "Efternamn")]
        public String LastName { get; set; }

    }
}