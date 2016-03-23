namespace LMS_RAM.Migrations
{
	using LMS_RAM.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_RAM.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
			ContextKey = "LMS_RAM.Models.ApplicationDbContext";
        }

        protected override void Seed(LMS_RAM.Models.ApplicationDbContext context)
        {
			if (!context.Users.Any(u => u.UserName == "admin"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { UserName = "admin" };

				manager.Create(user, "Password#1");
			}

			System.Diagnostics.Debug.WriteLine("Seed of Teachers started");

			if (!context.Teachers.Any())
			{
				var teachers = new List<Teacher> 
				{ 
 					new Teacher { SSN = "19630101-1234", FirstName = "Lena", LastName = "Ek" },
					new Teacher { SSN = "19630202-2345", FirstName = "Kalle", LastName = "Ek" }
				};

				teachers.ForEach(teacher => context.Teachers.AddOrUpdate(teacher));
				context.SaveChanges();
			}

			if (!context.Students.Any())
			{
				var students = new List<Student> 
				{ 
					new Student { SSN = "20050101-1234", FirstName = "Lotta", LastName = "Åhl" },
					new Student { SSN = "20050202-2345", FirstName = "Pelle", LastName = "Åhlen" }
				};

				students.ForEach(student => context.Students.AddOrUpdate(student));
				context.SaveChanges();
			}

			if (!context.Courses.Any())
			{
				var courses = new List<Course> 
				{ 
					new Course { Name = "Datakurs", StartDate = new DateTime(2016, 5, 15), EndDate = new DateTime(2016, 10, 15), Points = 15, TeacherId = 1 },
					new Course { Name = "dotNET", StartDate = new DateTime(2016, 6, 20), EndDate = new DateTime(2016, 12, 31), Points = 10, TeacherId = 1 }
				};

				courses.ForEach(course => context.Courses.AddOrUpdate(course));
				context.SaveChanges();
			}
        }
    }
}
