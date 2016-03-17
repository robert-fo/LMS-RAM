using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class ScheduleItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Starttid")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Sluttid")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Obligatorisk")]
        public Boolean Mandatory { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course course { get; set; }
    }
}