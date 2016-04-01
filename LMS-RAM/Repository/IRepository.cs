using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Repository
{
    public interface IRepository
    {
        List<Teacher> GetAllTeachers();
        List<Student> GetAllStudents();
        List<Course> GetAllCourses();
        List<StudentCourse> GetAllStudentCourses();
        List<Assignment> GetAllAssignments();
        List<ScheduleItem> GetAllScheduleItems();

        void CreateCourse(Course course);
        void DeleteCourse(int id);
        void UpdateDbCourse(Course course);

        void CreateStudent(Student student);
        void DeleteStudent(int id);
        void UpdateDbStudent(Student student);

        void AddStudentUser(ApplicationUser user);

        void CreateStudentCourse(StudentCourse studentcourse);
        void DeleteStudentCourse(int id);
        void UpdateDbStudentCourse(StudentCourse studentcourse);

        void UpdateDbTeacher(Teacher teacher);

        IEnumerable<SelectListItem> GetSelectListStudenter(int? id);

    }
}