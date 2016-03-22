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

		public DbSet<Course> Courses { get; set; }
		public DbSet<ScheduleItem> ScheduleItems { get; set; }
		public DbSet<Assignment> Assignments { get; set; }

		public DbSet<Student> Students { get; set; }
		public DbSet<StudentCourse> StudentCourses { get; set; }
		public DbSet<StudentShared> StudentShareds { get; set; }

		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<TeacherShared> TeacherShareds { get; set; }

    }
}