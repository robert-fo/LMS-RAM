namespace LMS_RAM.Migrations
{
	using LMS_RAM.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_RAM.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
//			ContextKey = "Identity.Models.ApplicationDbContext";
        }

        protected override void Seed(LMS_RAM.Models.ApplicationDbContext context)
        {


        }
    }
}
