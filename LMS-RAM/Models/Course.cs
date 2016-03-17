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
     
        public string Name { get; set; }
      
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Double Points { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher teacher { get; set; }
    }
}