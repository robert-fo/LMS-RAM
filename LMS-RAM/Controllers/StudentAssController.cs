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
        public ActionResult Index()
        {
            var assignmentsAll = repository.GetAllAssignments();
            var studentsAll = repository.GetAllStudents();

            var user = User.Identity.GetUserName();

            var studenten = from student in studentsAll
                            where student.UserName == user
                            select student;

            List<Assignment> sAssignments = new List<Assignment>();

            foreach (var aitem in assignmentsAll)
            {
                if (aitem.StudentId == studenten.First().Id)
                {
                    sAssignments.Add(aitem);
                }
            }

            return View(sAssignments);
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
        //public ActionResult Create(HttpPostedFileBase FileName)
        {
            try
            {
                // TODO: Add insert logic here
                if (FileName != null && FileName.ContentLength > 0)
                {
                    string filePath = Path.Combine(Server.MapPath("~/App_Data/Uploads"), Path.GetFileName(FileName.FileName));
                    FileName.SaveAs(filePath);
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
