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
    public class StudentAssController : Controller
    {


        private StudentRepository repository;

        public StudentAssController()
        {
            this.repository = new StudentRepository();
        }

        public StudentAssController(StudentRepository Repository)
        {
            this.repository = Repository;
        }


        // GET: StudentAss
        public ActionResult Index(int courseId = 2)
        {
            var assignmentsAll = repository.GetAllAssignments();
            var studentsAll = repository.GetAllStudents();
            var scheduleItemsAll = repository.GetAllScheduleItems();

            //var user = User.Identity.GetUserName();

            IEnumerable<Student> studenten;// = new List<Student>();

            if (Session["CourseID"] == null)
                Session["CourseID"] = courseId;

            if (Session["StudentID"] == null)
            {
                var user = User.Identity.GetUserName();

                studenten = from student in studentsAll
                            where student.UserName == user
                            select student;

                Session["StudentID"] = studenten.First().Id;
            }
            else
            {
                int sID = Convert.ToInt32(Session["StudentID"]);
                studenten = from student in studentsAll
                            where student.Id == sID
                            select student;
            }

            //var studenten = from student in studentsAll
            //                where student.UserName == user
            //                select student;

            List<Assignment> sAssignments = new List<Assignment>();
            List<Assignment> cAssignments = new List<Assignment>();
            List<ScheduleItem> scheduleItems = new List<ScheduleItem>();

            foreach (var aitem in assignmentsAll)
            {
                if (aitem.StudentId == studenten.First().Id)
                {
                    sAssignments.Add(aitem);
                }
            }

            foreach (var sitem in scheduleItemsAll)
            {
                if (sitem.CourseId == courseId)
                {
                    scheduleItems.Add(sitem);
                }
            }

            foreach (var sitem in scheduleItems)
            {
                foreach (var aitem in sAssignments)
                {
                    if (sitem.Id == aitem.ScheduleItemId)
                    {
                        cAssignments.Add(aitem);
                    }
                }
            }

            return View(cAssignments);
        }

        // GET: StudentAss/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentAss/Create
        public ActionResult Create()
        {
            ViewBag.ScheduleItemId = new SelectList(repository.GetAllScheduleItems(), "Id", "Name");
            ViewBag.StudentId = new SelectList(repository.GetAllStudents(), "Id", "SSN");
            return View();
        }

        // POST: StudentAss/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,StudentId,ScheduleItemId,Name,Grade,Comment,FileName")] Assignment assignment, HttpPostedFileBase FileName)
        {
            try
            {
                // TODO: Add insert logic here
                assignment.FileName = FileName.FileName;

                if (ModelState.IsValid)
                {
                    repository.CreateAssignment(assignment);
                    //return RedirectToAction("Index");
                }

                if (FileName != null && FileName.ContentLength > 0)
                {
                    //string filePath = Path.Combine(Server.MapPath("~/Uploads/Assignments/"), Path.GetFileName(FileName.FileName));
                    string filepath1 = Path.GetFileName(FileName.FileName);
                    string filePath2 = Server.MapPath("~/Uploads/Assignments/" + Session["CourseID"] + "_" + assignment.ScheduleItemId + "/" + assignment.StudentId + "_" + assignment.Id + "_" + filepath1);
                    FileName.SaveAs(filePath2);
                }
                //if (ModelState.IsValid)
                //{
                //    db.Assignments.Add(assignment);
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public FileResult Download(Assignment assignment)
        {
            string fileName = "~/Uploads/Assignments/" + Session["CourseID"] + "_" + assignment.ScheduleItemId + "/" + assignment.StudentId +  "_" + assignment.Id + "_" + assignment.FileName;
            string contentType = "application/pdf";

            return new FilePathResult(fileName, contentType)
            {
                FileDownloadName = assignment.FileName
            };
        }

        // GET: StudentAss/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentAss/Edit/5
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

        // GET: StudentAss/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentAss/Delete/5
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
