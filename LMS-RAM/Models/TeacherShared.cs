using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_RAM.Models
{
    public class TeacherShared
    {
        [Key]
        public int Id { get; set; }

		public int CourseId { get; set; }
		public int TeacherId { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Display(Name = "Description")]
        public String Description { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Display(Name = "File name")]
        public String FileName { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
    }
}