using LMS_RAM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Controllers
{
    [Authorize]
    public class TeachersHomeController : Controller
    {
        private IRepository repository;

        public TeachersHomeController()
        {
            this.repository = new WorkingRepository();
        }

        public TeachersHomeController(IRepository Repository)
        {
            this.repository = Repository;
        }
        
        // GET: TeacherHHome
        public ActionResult Index()
        {
            var courses = repository.GetAllCourses();

            var teacherCourses = from course in courses
                                 where course.TeacherId == 1
                                 orderby course.Id
                                 select course;

            var tCourses = teacherCourses.ToList();

            return View(tCourses);
        }

        // GET: TeacherHHome/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeacherHHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherHHome/Create
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

        // GET: TeacherHHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeacherHHome/Edit/5
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

        // GET: TeacherHHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeacherHHome/Delete/5
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
