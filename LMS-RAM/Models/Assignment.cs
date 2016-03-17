using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Benämning")]
        public String Name { get; set; }
        [Display(Name = "Betyg")]
        public String Grade { get; set; }
        [Display(Name = "Kommentar")]
        public String Comments { get; set; }
        [Display(Name = "Filnamn")]
        public String FileName { get; set; }

        [ForeignKey("StudentId"), Display(Name = "Student")]
        public virtual Student Student { get; set; }
        [ForeignKey("TeacherId"), Display(Name = "Lärare")]
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("ScheduleId"), Display(Name = "Schema")]
        public virtual List<Schedule> Schedule { get; set; }
    }
}