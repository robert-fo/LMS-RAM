using LMS_RAM.DataAccessLayer;
using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_RAM.Repository
{
    public class WorkingRepository
    {
        private DataContext db;
	
        public WorkingRepository()
        {
            db = new DataContext();
        }

		#region Get all Teachers.
		public List<Teacher> GetAllTeachers()
		{
			return (from row in db.Teachers
					select new Teacher
					{
						SSN = row.SSN,
						FirstName = row.FirstName,
						LastName = row.LastName
					}).ToList();
		}
		#endregion

		public List<Course> GetAllCoursesForTeacher(int teacherId)
		{
			return (from row in db.Courses
					where row.TeacherId == teacherId
					select new Course 
					{ 
						Name = row.Name, 
						Points = row.Points, 
						StartDate = row.StartDate,
 						EndDate = row.EndDate,
						TeacherId = row.TeacherId
					}).ToList();
		}

        public List<Student> GetAllStudentsForCourse(int courseId)
        {
 
			var queryStudentCourses = (from row in db.StudentCourses
									   join S in db.Students on row.StudentId equals S.Id
									   where row.CourseId == courseId
									   select new Student {
										   FirstName = S.FirstName, 
										   LastName = S.LastName, 
										   SSN = S.SSN 
									   }).ToList();

            return queryStudentCourses;
        }

    }
}