using LMS_RAM.Models;
using LMS_RAM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Controllers
{
    [Authorize]
    public class StudentsHomeController : Controller
    {
 
        private IRepository repository;

        public StudentsHomeController()
        {
            this.repository = new WorkingRepository();
        }

        public StudentsHomeController(IRepository Repository)
        {
            this.repository = Repository;
        }
        
        // GET: StudentsHome
        public ActionResult Index()
        {
            var coursesAll = repository.GetAllCourses();
            var studentcoursesAll = repository.GetAllStudentCourses();

            var studentcourses = from course in studentcoursesAll
                          where course.StudentId == 1
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

            return View(sCourses);
        }

        // GET: StudentsHome/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentsHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsHome/Create
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

        // GET: StudentsHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentsHome/Edit/5
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

        // GET: StudentsHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentsHome/Delete/5
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
