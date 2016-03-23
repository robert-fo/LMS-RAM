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
			System.Diagnostics.Debug.WriteLine("Seed of Users and Roles started");

			if (!context.Users.Any(u => u.UserName == "admin@email.com"))
			{
				// add roles
				// ---------
				var roleStore = new RoleStore<IdentityRole>(context);
				var roleManager = new RoleManager<IdentityRole>(roleStore);
	
				roleManager.Create(new IdentityRole {Name = "admin"});
				roleManager.Create(new IdentityRole {Name = "teacher"});
				roleManager.Create(new IdentityRole {Name = "student"});

				// add users
				// ---------
				var userStore = new UserStore<ApplicationUser>(context);
				var userManager = new UserManager<ApplicationUser>(userStore);

                var user1 = new ApplicationUser { UserName = "admin@mail.com" };
                var user2 = new ApplicationUser { UserName = "teacher1@mail.com" };
                var user3 = new ApplicationUser { UserName = "teacher2@mail.com" };
                var user4 = new ApplicationUser { UserName = "elev1@mail.com" };
                var user5 = new ApplicationUser { UserName = "elev2@mail.com" };

				userManager.Create(user1, "Password#1");
				userManager.Create(user2, "Password#1");
				userManager.Create(user3, "Password#1");
				userManager.Create(user4, "Password#1");
				userManager.Create(user5, "Password#1");

				// add user to a role
				userManager.AddToRole(user1.Id, "admin");
				userManager.AddToRole(user2.Id, "teacher");
				userManager.AddToRole(user3.Id, "teacher");
				userManager.AddToRole(user4.Id, "student");
				userManager.AddToRole(user5.Id, "student");
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

			System.Diagnostics.Debug.WriteLine("Seed of Students started");

			if (!context.Students.Any())
			{
				var students = new List<Student> 
				{ 
					new Student { SSN = "20050101-1234", FirstName = "Lotta", LastName = "Åhl" },
					new Student { SSN = "20050202-2345", FirstName = "Pelle", LastName = "Åhlen" },
					new Student { SSN = "20060112-1111", FirstName = "Christer", LastName = "Nilsson" },
					new Student { SSN = "20051231-2222", FirstName = "Lena", LastName = "Svensson" }
				};

				students.ForEach(student => context.Students.AddOrUpdate(student));
				context.SaveChanges();
			}

			System.Diagnostics.Debug.WriteLine("Seed of courses started");

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
