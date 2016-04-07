using LMS_RAM.Models;
using LMS_RAM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;

namespace LMS_RAM.Controllers
{
     [Authorize(Roles = "student")]
    public class StudentsHomeController : Controller
    {
        private BusinessLogic blogic;

        public StudentsHomeController()
        {
            this.blogic = new BusinessLogic();
        }

        public StudentsHomeController(IRepository Repository)
        {
            this.blogic = new BusinessLogic(Repository);
        }
        
        // GET: StudentsHome
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();

            var thestudent = blogic.StudentFromLogin(user);

            //ViewBag.StudentId = thestudent.Id.ToString();
            //Session["StudentID"] = thestudent.Id;

            var sCourses = blogic.StudentsCourses(thestudent.Id);

            return View(sCourses);
        }

        // GET: StudentsHome/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thecourse = blogic.CourseDetails(id);

            if (thecourse == null)
            {
                return HttpNotFound();
            }

            return View(thecourse);
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

        // GET: StudentsHome/Account/5
        public ActionResult Account(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thestudent = blogic.StudentDetails(id);

            if (thestudent == null)
            {
                return HttpNotFound();
            }

            return View(thestudent);
        }

        // POST: StudentsHome/Account/5
        [HttpPost, ActionName("Account")]
        [ValidateAntiForgeryToken]
        public ActionResult AccountConfirm(Student student)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    blogic.UpdateStudent(student);
                    return RedirectToAction("Index");
                }
                return View(student);
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

        // GET: TeacherHHome/Assignments/5
        public ActionResult TeacherShared(int? id)
        {

            return Redirect("/TeacherShareds/Index/" + id.ToString()); // 

        }

        public ActionResult CourseStudents(int? id)
        {
            Session["StudentCourseID"] = id;

            var classstudents = blogic.StudentsInCourse(id);

            return View(classstudents);
        }

        // GET: StudentsHome/StudentDetails/5
        public ActionResult StudentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thestudent = blogic.StudentDetails(id);

            if (thestudent == null)
            {
                return HttpNotFound();
            }

            return View(thestudent);
        }

       // GET: StudentsHome/StudentDetails/5
        public ActionResult TeacherDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var theteacher = blogic.TeacherDetails(id);

            if (theteacher == null)
            {
                return HttpNotFound();
            }

            return View(theteacher);
        }

        public ActionResult CourseSchedule(int? id)
        {
            var tScheduleItems = blogic.CourseSchedule(id);

            return View(tScheduleItems);
        }

    }
}
