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

		public int StudentId { get; set; }
		public int TeacherId { get; set; }
		public int ScheduleItemId { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Display(Name="Benämning")]
        public String Name { get; set; }

        [Display(Name = "Betyg")]
        [DisplayFormat(NullDisplayText = " ")]
        public Grade? Grade { get; set; }

        [StringLength(200, ErrorMessage = "Max 200 tecken")]
        [Display(Name = "Kommentar")]    
        public String Comment { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        [Display(Name = "Filnamn")]
        public String FileName { get; set; }


        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
  
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        
        [ForeignKey("ScheduleItemId")]
        public virtual ScheduleItem Schedule { get; set; }
    }
}