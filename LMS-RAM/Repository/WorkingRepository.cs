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

        // Teachers
		public List<Teacher> GetAllTeachers()
		{
            var teachers = db.Teachers.ToList();

            return teachers;
		}

        public void UpdateDbTeacher(Teacher teacher)
        {
            db.Entry(teacher).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på retunr typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }

        
        
        // Students
        public List<Student> GetAllStudents()
        {
            var students = db.Students.ToList();

            return students;
        }

        public void AddStudentUser(ApplicationUser user)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.Create(user, "Pass#1");
            userManager.AddToRole(user.Id, "student");
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

        
        
        // Courses
		public List<Course> GetAllCourses()
		{
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
            db.Entry(course).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på return typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }



        // StudentCourse
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

        
        
        // Assignments
        public List<Assignment> GetAllAssignments()
        {
            var assignments = db.Assignments.ToList();

            return assignments;
        }

        
        
        // ScheduleItems
        public List<ScheduleItem> GetAllScheduleItems()
        {
            var scheduleItems = db.ScheduleItems.ToList();

            return scheduleItems;
        }

        public void CreateScheduleItem(ScheduleItem scheduleitem)
        {
            db.ScheduleItems.Add(scheduleitem);
            db.SaveChanges(); // Updates all changed objects
        }

        public void DeleteScheduleItem(int id)
        {
            ScheduleItem scheduleitem = db.ScheduleItems.Find(id);
            db.ScheduleItems.Remove(scheduleitem);
            db.SaveChanges(); // Updates all changed objects
        }

        public void UpdateDbScheduleItem(ScheduleItem scheduleitem)
        {
            db.Entry(scheduleitem).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på retunr typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }
    }
}