using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class StudentCourse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int StudentId { get; set; }
        
        public  Grade { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course course { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student student { get; set; }
    }
}