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
        [Authorize(Roles = "student")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentShared/Create
        [Authorize(Roles = "student")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,CourseId,StudentId,Description,FileName")] StudentShared studentShared, HttpPostedFileBase FileName)
        {
            try
            {
                // TODO: Add insert logic here
                studentShared.FileName = FileName.FileName;

                if (ModelState.IsValid)
                {
                    repository.CreateStudentShared(studentShared);                   
                }

                if (FileName != null && FileName.ContentLength > 0)
                {
                    string subPath1 = "~/Uploads/StudentsShared/" + studentShared.CourseId + "_" + studentShared.StudentId + "_" + studentShared.Id + "_";
                    string subPath2 = Path.GetFileName(FileName.FileName);
                    string filePath = Server.MapPath(subPath1 + subPath2);
                    FileName.SaveAs(filePath);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "student")]
        public FileResult Download(StudentShared studentShared)
        {
            string fileName = "~/Uploads/StudentsShared/" + studentShared.CourseId + "_" + studentShared.StudentId + "_" + studentShared.Id + "_" + studentShared.FileName;
            string contentType = "application/pdf";

            return new FilePathResult(fileName, contentType)
            {
                FileDownloadName = studentShared.FileName
            };
        }

        // GET: StudentShared/Edit/5
        [Authorize(Roles = "student")]
        public ActionResult Edit(int? id)
        {
            List<StudentShared> ListStudentShared = repository.GetAllStudentShared();
            StudentShared studentShared = ListStudentShared.FirstOrDefault((i => i.Id == id));

            return View(studentShared);
        }

        // POST: StudentShared/Edit/5
        [Authorize(Roles = "student")]
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
