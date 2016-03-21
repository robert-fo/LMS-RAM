namespace LMS_RAM.Migrations
{
	using LMS_RAM.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;


    internal sealed class Configuration : DbMigrationsConfiguration<LMS_RAM.DataAccessLayer.DataContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
			AutomaticMigrationsEnabled = true;
			ContextKey = "LMS_RAM.Models.ApplicationDbContext";
        }

        protected override void Seed(LMS_RAM.DataAccessLayer.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

			if (!context.Users.Any(u => u.UserName == "admin"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { UserName = "admin" };

				manager.Create(user, "password");
			}

			context.Teacher.AddOrUpdate(
			  //p => p.Teacher,
			  new Teacher { SSN="19630101-1234", FirstName="Lena", LastName = "Ek" },
			  new Teacher { SSN = "19630202-2345", FirstName = "Kalle", LastName ="Ek" }
			);

			context.Student.AddOrUpdate(
			  //p => p.Students,
			  new Student { SSN="20050101-1234", FirstName="Lotta", LastName = "Åhl" },
			  new Student { SSN = "20050202-2345", FirstName = "Pelle", LastName ="Åhlen" }
			);

			context.Course.AddOrUpdate(
				//p => p.Courses,
			  new Course { Name = "Datakurs", StartDate=new DateTime(2016, 5, 15), EndDate=new DateTime(2016, 10, 15), Points = 15, TeacherId = 1 },
			  new Course { Name = "dotNET", StartDate=new DateTime(2016, 6, 20), EndDate=new DateTime(2016, 12, 31), Points = 10, TeacherId = 1 }
			);
        }
    }
}
