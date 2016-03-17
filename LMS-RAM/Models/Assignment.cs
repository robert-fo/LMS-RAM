using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
 
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Display(Name="Benämning")]
        public String Name { get; set; }

        [Display(Name = "Betyg")]
        public Grade? Grade { get; set; }

        [StringLength(200, ErrorMessage = "Max 200 tecken")]
        [Display(Name = "Kommentar")]
        [DisplayFormat(NullDisplayText = " ")]
        public String Comment { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Display(Name = "Filnamn")]
        public String FileName { get; set; }


        [ForeignKey("StudentId"), Display(Name = "Student")]
        public virtual Student Student { get; set; }
  
        [ForeignKey("TeacherId"), Display(Name = "Lärare")]
        public virtual Teacher Teacher { get; set; }
        
        [ForeignKey("ScheduleId"), Display(Name = "Schema")]
        public virtual List<ScheduleItem> Schedule { get; set; }
    }
}