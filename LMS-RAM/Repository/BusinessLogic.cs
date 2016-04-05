using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Repository
{
    public class BusinessLogic
    {
        private WorkingRepository wrepository;
 
        public BusinessLogic()
        {
            this.wrepository = new WorkingRepository();
        }

        public IEnumerable<SelectListItem> GetSelectListStudenter(int? id)
        {
            var selectList = new List<SelectListItem>();

            // Get all values of the Industry enum
            var Students = wrepository.GetAllStudents();
            var StudentCourses = wrepository.GetAllStudentCourses();

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
            
            var studentcoursesAll = wrepository.GetAllStudentCourses();
            var studentsAll = wrepository.GetAllStudents();

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
   

    }
}