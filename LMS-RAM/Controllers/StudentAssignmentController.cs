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
    public class StudentAssignmentController : Controller
    {


        private StudentRepository repository;

        public StudentAssignmentController()
        {
            this.repository = new StudentRepository();
        }

        public StudentAssignmentController(StudentRepository Repository)
        {
            this.repository = Repository;
        }


        // GET: StudentAssignment
        [Authorize(Roles = "teacher, student")]
        public ActionResult Index(int? id)
        {
            var assignmentsAll = repository.GetAllAssignments();
            var studentsAll = repository.GetAllStudents();
            var scheduleItemsAll = repository.GetAllScheduleItems();

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

            //var studenten = from student in studentsAll
            //                where student.UserName == user
            //                select student;

            List<Assignment> sAssignments = new List<Assignment>();
            List<Assignment> cAssignments = new List<Assignment>();
            List<ScheduleItem> scheduleItems = new List<ScheduleItem>();

            foreach (var aitem in assignmentsAll)
            {
                if (aitem.StudentId == studentId)
                {
                    sAssignments.Add(aitem);
                }
            }

            foreach (var sitem in scheduleItemsAll)
            {
                if (sitem.CourseId == id)
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

        // GET: StudentAssignment/Details/5
        [Authorize(Roles = "teacher, student")]
        public ActionResult Details(int? id)
        {
            List<Assignment> ListAssignments = repository.GetAllAssignments();
            Assignment assignment = ListAssignments.FirstOrDefault((i => i.Id == id));



            return View(assignment);
        }

        // GET: StudentAssignment/Create
        [Authorize(Roles = "student")]
        public ActionResult Create()
        {
            var temp = Session["CourseID"];
            
            ViewBag.ScheduleItemList = repository.GetScheduleItemList(Convert.ToInt32(Session["CourseID"]));

            //ViewBag.ScheduleItemId = new SelectList(repository.GetAllScheduleItems(), "Id", "Name");
            //ViewBag.StudentId = new SelectList(repository.GetAllStudents(), "Id", "SSN");
            
            

            return View();
        }

        // POST: StudentAssignment/Create
        [Authorize(Roles = "student")]
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
                    
                    string filepath1 = Path.GetFileName(FileName.FileName);
                    string subPath = "~/Uploads/Assignments/" + Session["CourseID"] + "_" + assignment.ScheduleItemId + "/";

                    Directory.CreateDirectory(Server.MapPath(subPath));
                   
                    string filePath2 = Server.MapPath(subPath + assignment.StudentId + "_" + assignment.Id + "_" + filepath1);
                    FileName.SaveAs(filePath2);
                }
                //if (ModelState.IsValid)
                //{
                //    db.Assignments.Add(assignment);
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}
                //string filePath = Path.Combine(Server.MapPath("~/Uploads/Assignments/"), Path.GetFileName(FileName.FileName));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "teacher, student")]
        public FileResult Download(Assignment assignment)
        {
            string fileName = "~/Uploads/Assignments/" + Session["CourseID"] + "_" + assignment.ScheduleItemId + "/" + assignment.StudentId +  "_" + assignment.Id + "_" + assignment.FileName;
            string contentType = "application/pdf";

            return new FilePathResult(fileName, contentType)
            {
                FileDownloadName = assignment.FileName
            };
        }

        // GET: StudentAssignment/Edit/5
        [Authorize(Roles = "teacher")]
        public ActionResult Edit(int? id)
        {
            List<Assignment> ListAssignments = repository.GetAllAssignments();
            Assignment assignment = ListAssignments.FirstOrDefault((i => i.Id == id));

            return View(assignment);
        }

        // POST: StudentAssignment/Edit/5
        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,StudentId,ScheduleItemId,Name,Grade,Comment,FileName")] Assignment assignment)
        {
            try
            {
                // TODO: Add update logic here
                repository.UpdateDbAssignment(assignment);
                return RedirectToAction("Index", new { id = Session["CourseID"] });
            }
            catch
            {
                return View();
            }
        }
    }
}
