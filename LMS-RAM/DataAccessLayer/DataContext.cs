using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS_RAM.DataAccessLayer
{
    public class DataContext : DbContext
    {
		public DataContext() : base("DefaultConnection") { }

		public DbSet<Course> Course { get; set; }
		public DbSet<ScheduleItem> ScheduleItem { get; set; }
		public DbSet<Assignment> Assignment { get; set; }

		public DbSet<Student> Student { get; set; }
		public DbSet<StudentCourse> StudentCourse { get; set; }
		public DbSet<StudentShared> StudentShared { get; set; }

		public DbSet<Teacher> Teacher { get; set; }
		public DbSet<TeacherShared> TeacherShared { get; set; }

    }
}