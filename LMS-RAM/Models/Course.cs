using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Display(Name = "Kursnamn")]
        public string Name { get; set; }
      
        [Display(Name="Startdatum")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Poäng")]
        public Double Points { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher teacher { get; set; }
    }
}