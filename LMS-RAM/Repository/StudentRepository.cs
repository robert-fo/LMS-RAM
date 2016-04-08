using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Repository
{
    public class StudentRepository
    {
        private ApplicationDbContext db;

        public StudentRepository()
        {
            db = new ApplicationDbContext();
        }

        public List<Student> GetAllStudents()
        {
            var students = db.Students.ToList();

            return students;
        }

        public List<StudentShared> GetAllStudentShared()
        {
            var studentShared = db.StudentShareds.ToList();

            return studentShared;
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

        public List<Course> GetAllCourses()
        {
            var courses = db.Courses.ToList();

            return courses;
        }

        public void CreateAssignment(Assignment assignment)
        {
            db.Assignments.Add(assignment);
            db.SaveChanges(); // Updates all changed objects
        }

        public void CreateStudentShared(StudentShared studentShared)
        {
            db.StudentShareds.Add(studentShared);
            db.SaveChanges(); // Updates all changed objects
        }

        public StudentShared getStudentShared(int id)
        {
            return db.StudentShareds.Find(id);
        }

        public void DeleteStudentShared(int id)
        {
            StudentShared studentShared = db.StudentShareds.Find(id);
            db.StudentShareds.Remove(studentShared);
            db.SaveChanges(); // Updates all changed objects
        }

        public void UpdateDbStudentShared(StudentShared studentShared)
        {
            db.Entry(studentShared).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på retunr typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }

        public void UpdateDbAssignment(Assignment assignment)
        {
            db.Entry(assignment).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på retunr typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }

        public IEnumerable<SelectListItem> GetScheduleItemList(int id)
        {
            return (from t in db.ScheduleItems
                    where t.CourseId == id
                    select new SelectListItem() { Text = t.Name, Value = t.Id.ToString() }).ToList();
        }
    }
}