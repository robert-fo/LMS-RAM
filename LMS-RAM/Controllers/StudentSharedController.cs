using LMS_RAM.Models;
using LMS_RAM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;

namespace LMS_RAM.Controllers
{
    public class StudentSharedController : Controller
    {
        private StudentRepository repository;

        public StudentSharedController()
        {
            this.repository = new StudentRepository();
        }

        public StudentSharedController(StudentRepository Repository)
        {
            this.repository = Repository;
        }


        // GET: StudentShared
        [Authorize(Roles = "student")]
        public ActionResult Index(int? id)
        {
            
            var studentsAll = repository.GetAllStudents();
            //var coursesAll = repository.GetAllCourses();
            var studentSharedAll = repository.GetAllStudentShared();

            int studentId;
            //var user = User.Identity.GetUserName();

            IEnumerable<Student> studenten;// = new List<Student>();

            Session["CourseID"] = id;

            if (Session["StudentID"] == null)
            {
                var user = User.Identity.GetUserName();

                studenten = from student in studentsAll
                            where student.UserName == user
                            select student;

                studentId = studenten.First().Id;

                Session["StudentID"] = studentId;
            }
            else
            {
                int sID = Convert.ToInt32(Session["StudentID"]);
                studenten = from student in studentsAll
                            where student.Id == sID
                            select student;

                studentId = studenten.First().Id;
            }

            List<StudentShared> sstudentShared = new List<StudentShared>();
            List<StudentShared> cstudentShared = new List<StudentShared>();
            //List<Course> courses = new List<Course>();

            foreach (var aitem in studentSharedAll)
            {
                if (aitem.StudentId == studentId)
                {
                    sstudentShared.Add(aitem);
                }
            }

            foreach (var sitem in sstudentShared)
            {
                if (sitem.CourseId == id)
                {
                    cstudentShared.Add(sitem);
                }
            }

            return View(cstudentShared);
        }

        // GET: StudentShared/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentShared/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentShared/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentShared/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentShared/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentShared/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentShared/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
