using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LMS_RAM.Repository
{
    public class BusinessLogic
    {
        private IRepository repository;
 
        public BusinessLogic()
        {
            this.repository = new WorkingRepository();
        }

        public BusinessLogic(IRepository Repository)
        {
            this.repository = Repository;
        }

        public IEnumerable<SelectListItem> GetSelectListStudenter(int? id)
        {
            var selectList = new List<SelectListItem>();

            // Get all values of the Industry enum
            var Students = repository.GetAllStudents();
            var StudentCourses = repository.GetAllStudentCourses();

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
                    foreach (var scItem in StudentCourses)
                    {
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

        public List<Student> StudentsInCourse(int? id)
        {
            List<Student> classstudents = new List<Student>();
            
            var studentcoursesAll = repository.GetAllStudentCourses();
            var studentsAll = repository.GetAllStudents();

            var sCourses = from sCourse in studentcoursesAll
                           where sCourse.CourseId == id
                           orderby sCourse.Id
                           select sCourse;

            foreach (var item in sCourses)
            {
                foreach (var sitem in studentsAll)
                {
                    if (sitem.Id == item.StudentId)
                    {
                        classstudents.Add(sitem);
                    }
                }
            }

            return classstudents;
        }

        public IEnumerable<ScheduleItem> CourseSchedule(int? id)
        {
            var scheduleItemsAll = repository.GetAllScheduleItems();

            var tScheduleItems = from s in scheduleItemsAll
                                 where s.CourseId == id
                                 orderby s.StartTime
                                 select s;

            return tScheduleItems;
        }

        public ScheduleItem GetScheduleItem(int? id)
        {
            var ScheduleItemsAll = repository.GetAllScheduleItems();

            var tScheduleItems = from s in ScheduleItemsAll
                                 where s.Id == id
                                 select s;

            var theScheduleItem = tScheduleItems.First();

            return theScheduleItem;
        }

        public Course CourseDetails(int? id)
        {
            var coursesAll = repository.GetAllCourses();

            var tCourses = from course in coursesAll
                           where course.Id == id
                           select course;

            var thecourse = tCourses.First();

            return thecourse;
        }

        public Student StudentDetails(int? id)
        {
            var studentsAll = repository.GetAllStudents();

            var studenten = from student in studentsAll
                            where student.Id == id
                            select student;

            var thestudent = studenten.First();

            return thestudent;
        }

        public Teacher TeacherDetails(int? id)
        {
            var teachersAll = repository.GetAllTeachers();

            var tTeachers = from s in teachersAll
                            where s.Id == id
                            select s;

            var theteacher = tTeachers.First();

            return theteacher;
        }

        public Teacher TeacherFromLogin(string user)
        {
            var teachersAll = repository.GetAllTeachers();

            var tTeachers = from teacher in teachersAll
                          where teacher.UserName == user
                          select teacher;

            var theteacher = tTeachers.First();

            return theteacher;
        }

        public Student StudentFromLogin(string user)
        {
            var studentsAll = repository.GetAllStudents();

            var tStudents = from student in studentsAll
                            where student.UserName == user
                            select student;

            var thestudent = tStudents.First();

            return thestudent;
        }

        public IEnumerable<Course> TeacherCourses(int? id)
        {
            var coursesAll = repository.GetAllCourses();
            
            var tCourses = from course in coursesAll
                           where course.TeacherId == id
                           orderby course.Id
                           select course;

            return tCourses;
        }

        public StudentCourse GetStudentCourse(int? sid, int? cid)
        {
            var studentCoursesAll = repository.GetAllStudentCourses();

            var tCourses = from s in studentCoursesAll
                           where s.StudentId == sid
                           && s.CourseId == cid
                           select s;

            var thestudentcourse = tCourses.First();

            return thestudentcourse;
        }

        public IEnumerable<Course> StudentsCourses(int? id)
        {
            var coursesAll = repository.GetAllCourses();
            var studentcoursesAll = repository.GetAllStudentCourses();

            var studentcourses = from course in studentcoursesAll
                                 where course.StudentId == id
                                 orderby course.Id
                                 select course;

            List<Course> sCourses = new List<Course>();

            foreach (var sitem in studentcourses)
            {
                foreach (var citem in coursesAll)
                {
                    if (sitem.CourseId == citem.Id)
                    {
                        sCourses.Add(citem);
                    }
                }
            }

            return sCourses;
        }

        public void UpdateStudent(Student student){
            repository.UpdateDbStudent(student);
        }

        public void DeleteStudent(int id)
        {
            repository.DeleteStudent(id);
        }

        public List<Student> GetAllStudents()
        {
            return repository.GetAllStudents();
        }

        public void CreateStudent(Student student)
        {
            repository.CreateStudent(student);
        }

        public void AddStudentUser(ApplicationUser user)
        {
            repository.AddStudentUser(user);
        }

        public void DeleteScheduleItem(int id)
        {
            repository.DeleteScheduleItem(id);
        }

        public void CreateScheduleItem(ScheduleItem scheduleitem)
        {
            repository.CreateScheduleItem(scheduleitem);
        }

        public void UpdateDbScheduleItem(ScheduleItem scheduleitem) 
        {
            repository.UpdateDbScheduleItem(scheduleitem);
        }

        public void UpdateDbCourse(Course course) 
        {
            repository.UpdateDbCourse(course);
        }

        public void DeleteCourse(int id)
        {
            repository.DeleteCourse(id);
        }

        public void UpdateDbTeacher(Teacher teacher)
        {
            repository.UpdateDbTeacher(teacher);
        }

        public void CreateStudentCourse(StudentCourse studentcourse)
        {
            repository.CreateStudentCourse(studentcourse);
        }

        public void UpdateDbStudentCourse(StudentCourse studentcourse)
        {
            repository.UpdateDbStudentCourse(studentcourse);
        }

        public void DeleteStudentCourse(int id)
        {
            repository.DeleteStudentCourse(id);
        }

        public void CreateCourse(Course course)
        {
            repository.CreateCourse(course);
        }
    }
}