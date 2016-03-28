using LMS_RAM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
            var coursesAll = repository.GetAllCourses();
            var studentcoursesAll = repository.GetAllStudentCourses();
            var teachersAll = repository.GetAllTeachers();

            var user = User.Identity.GetUserName();

            var lararen = from teacher in teachersAll
                          where teacher.UserName == user
                          select teacher;

            var tCourses = from course in coursesAll
                          where course.TeacherId == lararen.First().Id
                          orderby course.Id
                          select course;

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
