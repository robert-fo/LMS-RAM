namespace LMS_RAM.Migrations
{
	using LMS_RAM.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_RAM.DataAccessLayer.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
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

			context.Teacher.AddOrUpdate(
			  //p => p.Teacher,
			  new Teacher { SSN="19630101-1234", FirstName="Lena", LastName = "Ek" },
			  new Teacher { SSN = "19630202-2345", FirstName = "Kalle", LastName ="Ek" }
			);

			context.Student.AddOrUpdate(
			  //p => p.Student,
			  new Student { SSN="20050101-1234", FirstName="Lotta", LastName = "Åhl" },
			  new Student { SSN = "20050202-2345", FirstName = "Pelle", LastName ="Åhlen" }
			);

			context.Course.AddOrUpdate(
			  //p => p.Course,
			  new Course { Name="Datakurs", StartDate=DateTime.Now, EndDate=DateTime.Now.AddDays(30), Points=15, TeacherId=1 },
			  new Course { Name="dotNET", StartDate=DateTime.Now, EndDate=new DateTime().AddDays(50), Points=15, TeacherId=1 }
			);
        }
    }
}
