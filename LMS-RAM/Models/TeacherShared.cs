using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class TeacherShared
    {
        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Display(Name = "Beskrivning")]
        public String Description { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Display(Name = "Filnamn")]
        public String FileName { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
 
        [ForeignKey("TeacherId")]
        public virtual Student Student { get; set; }

    }
}