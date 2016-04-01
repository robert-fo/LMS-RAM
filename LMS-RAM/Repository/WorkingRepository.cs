using LMS_RAM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Repository
{
    public class WorkingRepository : IRepository
    {
        private ApplicationDbContext db;
	
        public WorkingRepository()
        {
            db = new ApplicationDbContext();
        }

		public List<Teacher> GetAllTeachers()
		{
            //return (from row in db.Teachers
            //        select new Teacher
            //        {
            //            SSN = row.SSN,
            //            FirstName = row.FirstName,
            //            LastName = row.LastName
            //        }).ToList();

            var teachers = db.Teachers.ToList();

            return teachers;
		}

        public void UpdateDbTeacher(Teacher teacher)
        {
            db.Entry(teacher).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på retunr typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }

        public List<Student> GetAllStudents()
        {
            //return (from row in db.Teachers
            //        select new Teacher
            //        {
            //            SSN = row.SSN,
            //            FirstName = row.FirstName,
            //            LastName = row.LastName
            //        }).ToList();

            var students = db.Students.ToList();

            return students;
        }

        public void CreateStudent(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges(); // Updates all changed objects
        }

        public void DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges(); // Updates all changed objects
        }

        public void UpdateDbStudent(Student student)
        {
            db.Entry(student).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på retunr typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }

		public List<Course> GetAllCourses()
		{
            //return (from row in db.Courses
            //        where row.TeacherId == teacherId
            //        select new Course 
            //        { 
            //            Name = row.Name, 
            //            Points = row.Points, 
            //            StartDate = row.StartDate,
            //            EndDate = row.EndDate,
            //            TeacherId = row.TeacherId
            //        }).ToList();

            List<Course> courses = db.Courses.ToList();

            return courses;
		}

        public void CreateCourse(Course course)
        {
            db.Courses.Add(course); 
            db.SaveChanges(); // Updates all changed objects
        }

        public void DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges(); // Updates all changed objects
        }

        public void UpdateDbCourse(Course course)
        {
            db.Entry(course).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på retunr typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }

        public List<StudentCourse> GetAllStudentCourses()
        {
            var studentcourses = db.StudentCourses.ToList();

            return studentcourses;
        }

        public void CreateStudentCourse(StudentCourse studentcourse)
        {
            db.StudentCourses.Add(studentcourse);
            db.SaveChanges(); // Updates all changed objects
        }

        public void DeleteStudentCourse(int id)
        {
            StudentCourse studentcourse = db.StudentCourses.Find(id);
            db.StudentCourses.Remove(studentcourse);
            db.SaveChanges(); // Updates all changed objects
        }

        public void UpdateDbStudentCourse(StudentCourse studentcourse)
        {
            db.Entry(studentcourse).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på retunr typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }

        public void AddStudentUser(ApplicationUser user)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.Create(user, "Pass#1");
            userManager.AddToRole(user.Id, "student");
        }

        public List<Assignment> GetAllAssignments()
        {
            var assignments = db.Assignments.ToList();

            return assignments;
        }

        public List<ScheduleItem> GetAllScheduleItems()
        {
            var scheduleItems = db.ScheduleItems.ToList();

            return scheduleItems;
        }

        public IEnumerable<SelectListItem> GetSelectListStudenter(int? id)
        {
            var selectList = new List<SelectListItem>();

            // Get all values of the Industry enum
            var Students = GetAllStudents();
            var StudentCourses = GetAllStudentCourses();

            selectList.Add(new SelectListItem
            {
                Value = "NONE",
                Text = "None"
            });

            if (id == null)
            {
                foreach (var item in Students)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = item.Id.ToString(),
                        Text = item.SSN //item.FirstName + " " + item.LastName
                    });
                }
            }
            else
            {
                bool isInCourse = false;

                foreach (var sItem in Students)
                {
                    // Check if student is already in course
                    foreach (var scItem in StudentCourses) {
                        if (scItem.CourseId == id && scItem.StudentId == sItem.Id)
                        {
                            isInCourse = true;
                        }
                    }

                    if (isInCourse == false)
                    {
                        selectList.Add(new SelectListItem
                        {
                            Value = sItem.Id.ToString(),
                            Text = sItem.SSN //sItem.FirstName + " " + sItem.LastName
                        });
                    }

                    isInCourse = false;
                }
            }

            return selectList;
        }

    }
}