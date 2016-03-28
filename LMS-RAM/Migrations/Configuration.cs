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
            // ----------------------------------------------------------------------------------------------
            // Users and roles
            // ----------------------------------------------------------------------------------------------
            #region UserAndRoles
            System.Diagnostics.Debug.WriteLine("Seed of Users and Roles started");
            if (!context.Users.Any(u => u.UserName == "admin@mail.com"))
            {
                // add roles
                // ---------
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                roleManager.Create(new IdentityRole { Name = "admin" });
                roleManager.Create(new IdentityRole { Name = "teacher" });
                roleManager.Create(new IdentityRole { Name = "student" });

                // add users
                // ---------
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user1 = new ApplicationUser { Email = "admin@mail.com", UserName = "admin@mail.com" };
                var user2 = new ApplicationUser { Email = "teacher1@mail.com", UserName = "teacher1@mail.com" };
                var user3 = new ApplicationUser { Email = "teacher1@mail.com", UserName = "teacher2@mail.com" };
                var user4 = new ApplicationUser { Email = "elev1@mail.com", UserName = "elev1@mail.com" };
                var user5 = new ApplicationUser { Email = "elev2@mail.com", UserName = "elev2@mail.com" };
                var user6 = new ApplicationUser { Email = "elev3@mail.com", UserName = "elev3@mail.com" };
                var user7 = new ApplicationUser { Email = "elev4@mail.com", UserName = "elev4@mail.com" };
                var user8 = new ApplicationUser { Email = "elev5@mail.com", UserName = "elev5@mail.com" };

                userManager.Create(user1, "Pass#1");
                userManager.Create(user2, "Pass#1");
                userManager.Create(user3, "Pass#1");
                userManager.Create(user4, "Pass#1");
                userManager.Create(user5, "Pass#1");
                userManager.Create(user6, "Pass#1");
                userManager.Create(user7, "Pass#1");
                userManager.Create(user8, "Pass#1");

                // add user to a role
                userManager.AddToRole(user1.Id, "admin");
                userManager.AddToRole(user2.Id, "teacher");
                userManager.AddToRole(user3.Id, "teacher");
                userManager.AddToRole(user4.Id, "student");
                userManager.AddToRole(user5.Id, "student");
                userManager.AddToRole(user6.Id, "student");
                userManager.AddToRole(user7.Id, "student");
                userManager.AddToRole(user8.Id, "student");
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Teachers
            // ----------------------------------------------------------------------------------------------
            #region Teachers
            System.Diagnostics.Debug.WriteLine("Seed of Teachers started");
            if (!context.Teachers.Any())
            {
                var teachers = new List<Teacher> 
				{ 
 					new Teacher { SSN = "19630101-1234", FirstName = "Lena", LastName = "Ek" },
					new Teacher { SSN = "19630202-2345", FirstName = "Kalle", LastName = "Ek" },
					new Teacher { SSN = "19641212-1111", FirstName = "Per", LastName = "Persson" }
				};

                teachers.ForEach(teacher => context.Teachers.AddOrUpdate(teacher));
                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Courses (dependent of Techers)
            // ----------------------------------------------------------------------------------------------
            #region Courses
            System.Diagnostics.Debug.WriteLine("Seed of Courses started");
            if (!context.Courses.Any())
            {
                var courses = new List<Course> 
				{ 
					new Course { Name = "Datakurs", StartDate = new DateTime(2016, 5, 15), EndDate = new DateTime(2016, 10, 15), Points = 15, TeacherId = 1 },
					new Course { Name = "dotNET", StartDate = new DateTime(2016, 6, 20), EndDate = new DateTime(2016, 12, 31), Points = 10, TeacherId = 1 },
					new Course { Name = "C++", StartDate = new DateTime(2016, 8, 20), EndDate = new DateTime(2016, 12, 31), Points = 7.5, TeacherId = 1 }
				};

                courses.ForEach(course => context.Courses.AddOrUpdate(course));
                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // TeacherShareds  (dependent of Techers amd Courses)
            // ----------------------------------------------------------------------------------------------
            #region TeacherShareds
            System.Diagnostics.Debug.WriteLine("Seed of TeacherShareds started");
            if (!context.TeacherShareds.Any())
            {
                var teacherShareds = new List<TeacherShared> 
				{ 
					new TeacherShared 
						{ CourseId = 1, TeacherId = 1, Description = "Inlämningsuppg 1", FileName = "Inlämningsupp1.pdf" }
				};

                teacherShareds.ForEach(teacherShared => context.TeacherShareds.AddOrUpdate(teacherShared));
                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Students
            // ----------------------------------------------------------------------------------------------
            #region Students
            System.Diagnostics.Debug.WriteLine("Seed of Students started");
            if (!context.Students.Any())
            {
                var students = new List<Student> 
				{ 
					new Student { SSN = "20050101-1234", FirstName = "Lotta", LastName = "Åhl" },
					new Student { SSN = "20050202-2345", FirstName = "Pelle", LastName = "Åhlen" },
					new Student { SSN = "20060112-1111", FirstName = "Christer", LastName = "Nilsson" },
					new Student { SSN = "20051231-2222", FirstName = "Lena", LastName = "Svensson" },
					new Student { SSN = "20060505-3333", FirstName = "Pia", LastName = "Karlsson" }
				};

                students.ForEach(student => context.Students.AddOrUpdate(student));
                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // StudentCourses (dependent to Students and Courses)
            // ----------------------------------------------------------------------------------------------
            #region StudentCourses
            System.Diagnostics.Debug.WriteLine("Seed of StudentCourses started");
            if (!context.StudentCourses.Any())
            {
                var studentCourses = new List<StudentCourse> 
				{ 
					new StudentCourse { CourseId = 1, StudentId = 1 },
					new StudentCourse { CourseId = 1, StudentId = 2 },
					new StudentCourse { CourseId = 2, StudentId = 1 },
					new StudentCourse { CourseId = 2, StudentId = 2 }
				};

                studentCourses.ForEach(studentCourse => context.StudentCourses.AddOrUpdate(studentCourse));
                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // ScheduleItems (dependent to Courses)
            // -- Uncomment, update fields and generate after CourseId is set in Courses
            // ----------------------------------------------------------------------------------------------
            #region ScheduleItems
            System.Diagnostics.Debug.WriteLine("Seed of ScheduleItems started");
            if (!context.ScheduleItems.Any())
            {
                var scheduleItems = new List<ScheduleItem> 
				{ 
					new ScheduleItem 
						{ CourseId = 1, Name = "Föreläsning: AngularJs", StartTime = new DateTime(2016,3,23, 12, 0, 0), EndTime = new DateTime(2016, 3, 23, 14, 45, 0), Mandatory = false },
					new ScheduleItem 
						{ CourseId = 1, Name = "Inlämning 1", StartTime = new DateTime(2016,3,26, 0, 0, 0), EndTime = new DateTime(2016, 3, 30, 23, 59, 59), Mandatory = true },
					new ScheduleItem 
						{ CourseId = 1, Name = "Föreläsning: DataBas grunder", StartTime = new DateTime(2016,3,24, 13, 30, 0), EndTime = new DateTime(2016, 3, 24, 14, 45, 0), Mandatory = true },
					new ScheduleItem 
						{ CourseId = 1, Name = "Föreläsning: C#", StartTime = new DateTime(2016,3,27, 8, 30, 0), EndTime = new DateTime(2016, 3, 27, 9, 45, 0), Mandatory = false },
				};
                scheduleItems.ForEach(scheduleItem => context.ScheduleItems.AddOrUpdate(scheduleItem));
                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // Assignments
            // -- Uncomment and generate after StudentId is set int students and and ScheduleItemIdCourseId in Courses
            // ----------------------------------------------------------------------------------------------
            #region Assignments
            System.Diagnostics.Debug.WriteLine("Seed of Assignments started");
            if (!context.Assignments.Any())
            {
                var assignments = new List<Assignment> 
				{ 
					new Assignment 
						{ StudentId = 1, ScheduleItemId = 2, Name = "Inlämningsuppgift 1", FileName = "Inlämningsuppgift1.pdf" },
					new Assignment 
						{ StudentId = 2, ScheduleItemId = 2, Name = "Inlämning 1", FileName = "Inlämning_1.pdf", Grade = Grade.VG, Comment = "Mycket bra !!!" }
				};

                assignments.ForEach(assignment => context.Assignments.AddOrUpdate(assignment));
                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // StudentCourses 
            // ----------------------------------------------------------------------------------------------
            #region StudentCourses
            System.Diagnostics.Debug.WriteLine("Seed of StudentCourses started");
            if (!context.StudentCourses.Any())
            {
                var studentCourses = new List<StudentCourse> 
				{ 
					new StudentCourse { CourseId = 1, StudentId = 1 },
					new StudentCourse { CourseId = 1, StudentId = 2 },
					new StudentCourse { CourseId = 2, StudentId = 1 },
					new StudentCourse { CourseId = 2, StudentId = 2 }
				};

                studentCourses.ForEach(studentCourse => context.StudentCourses.AddOrUpdate(studentCourse));
                context.SaveChanges();
            }
            #endregion

            // ----------------------------------------------------------------------------------------------
            // StudentShared (dependent to Courses and Students)
            // -- Uncomment, update fields and generate after StudentId is set int students and CourseId in Courses
            // ----------------------------------------------------------------------------------------------
            #region StudentShared
            System.Diagnostics.Debug.WriteLine("Seed of StudentShared started");
            if (!context.StudentShareds.Any())
            {
                var studentShareds = new List<StudentShared> 
				{ 
					new StudentShared 
						{ CourseId = 2, StudentId = 1, Description = "Bra artilkel om Angular", FileName = "Angular.pdf" },
					new StudentShared 
						{ CourseId = 2, StudentId = 2, Description = "Database 1", FileName = "Database.pdf" },
					new StudentShared 
						{ CourseId = 2, StudentId = 1 },
					new StudentShared 
						{ CourseId = 2, StudentId = 2 }
				};

                studentShareds.ForEach(studentShared => context.StudentShareds.AddOrUpdate(studentShared));
                context.SaveChanges();
            }
            #endregion

        }
    }
}
