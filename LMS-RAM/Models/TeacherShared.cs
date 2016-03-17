using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class TeacherShared
    {
        [Display(Name = "Beskrivning")]
        public String Description { get; set; }
        [Display(Name = "Filnamn")]
        public String FileName { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Student Student { get; set; }

    }
}