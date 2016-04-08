using LMS_RAM.Models;
using LMS_RAM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Net;


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

        [Authorize(Roles = "student")]
        public ActionResult SharedIndex(int? id)
        {
            var studentSharedAll = repository.GetAllStudentShared();
            List<StudentShared> studentShared = new List<StudentShared>();

            foreach (var sitem in studentSharedAll)
            {
                if (sitem.CourseId == id)
                {
                    studentShared.Add(sitem);
                }
            }
            return View(studentShared);
        }

        // GET: StudentShared/Details/5
        [Authorize(Roles = "admin, teacher, student")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var studentShared = repository.getStudentShared((int)id);

            if (studentShared == null)
            {
                return HttpNotFound();
            }

            return View(studentShared);
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
        public ActionResult Edit([Bind(Include = "Id,CourseId,StudentId,Description,FileName")] StudentShared studentShared, HttpPostedFileBase FileName)
        {
            int length = FileName.FileName.Length;
            int index = FileName.FileName.LastIndexOf('\\') + 1;


            string path = FileName.FileName.Substring(0, index);
            string filename = FileName.FileName.Substring(index);

            //update database
            studentShared.FileName = filename;
           
            repository.UpdateDbStudentShared(studentShared);


            string oldFiles = studentShared.CourseId + "_" +
                    studentShared.StudentId + "_" +
                    studentShared.Id + "_*.pdf";

            try
            {
                // remove old file
                foreach (string DeleteFileName in Directory.EnumerateFiles
                    (Server.MapPath("~/Uploads/StudentsShared/"), oldFiles))
                {
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads/StudentsShared/"), DeleteFileName));
                }

                //repository.CreateFile()
                if (FileName != null && FileName.ContentLength > 0)
                {
                    string filePath = Path.Combine(Server.MapPath("~/Uploads/StudentsShared/"),
                        studentShared.CourseId.ToString() + "_" +
                        studentShared.StudentId.ToString() + "_" +
                        studentShared.Id + "_" +
                        Path.GetFileName(FileName.FileName));

                    // store new file
                    FileName.SaveAs(filePath);

                    return RedirectToAction("Index", studentShared.CourseId);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error(e.Message);
                return RedirectToAction("Index", studentShared.CourseId);
            }
            return View();
        }



        // GET: StudentShared/Delete/5
        [Authorize(Roles = "student")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            int sid = (int)id;
            StudentShared studentShared = repository.getStudentShared(sid);

            if (studentShared == null)
            {
                return HttpNotFound();
            }

            return View(studentShared);
        }

        // POST: StudentShared/Delete/5
        [Authorize(Roles = "student")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            
            StudentShared studentShared = repository.getStudentShared((int)id);
            
            int theCourseId = studentShared.CourseId;

            repository.DeleteStudentShared((int)id);


            // remove the file also
            string filePath = Path.Combine(Server.MapPath("~/Uploads/StudentsShared/"),
                studentShared.CourseId.ToString() + "_" +
                studentShared.StudentId.ToString() + "_" +
                studentShared.Id.ToString() + "_" +
                studentShared.FileName);

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error(e.Message);
            }

            return RedirectToAction("Index", theCourseId);
        }
    }
}
