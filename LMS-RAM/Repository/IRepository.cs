using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_RAM.Repository
{
    public interface IRepository
    {
        List<Teacher> GetAllTeachers();
        List<Student> GetAllStudents();
        List<Course> GetAllCourses();
        List<StudentCourse> GetAllStudentCourses();

    }
}