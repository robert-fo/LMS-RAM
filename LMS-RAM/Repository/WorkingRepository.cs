using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

            var courses = db.Courses.ToList();

            return courses;
		}

        public List<StudentCourse> GetAllStudentCourses()
        {
 
            //var queryStudentCourses = (from row in db.StudentCourses
            //                           join S in db.Students on row.StudentId equals S.Id
            //                           where row.CourseId == courseId
            //                           select new Student {
            //                               FirstName = S.FirstName, 
            //                               LastName = S.LastName, 
            //                               SSN = S.SSN 
            //                           }).ToList();

            //return queryStudentCourses;

            var studentcourses = db.StudentCourses.ToList();

            return studentcourses;
        }

    }
}