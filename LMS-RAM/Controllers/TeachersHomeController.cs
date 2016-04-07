using LMS_RAM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LMS_RAM.Models;
using System.Net;

namespace LMS_RAM.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeachersHomeController : Controller
    {
        private BusinessLogic blogic;

        public TeachersHomeController()
        {
            this.blogic = new BusinessLogic();
        }

        public TeachersHomeController(IRepository Repository)
        {
            this.blogic = new BusinessLogic(Repository);
        }
        
        // GET: TeacherHHome
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();

            var theteacher = blogic.TeacherFromLogin(user);
            ViewBag.TeacherId = theteacher.Id.ToString();

            var tCourses = blogic.TeacherCourses(theteacher.Id);

            return View(tCourses);
        }

        // GET: TeacherHHome/Details/5
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

        // GET: TeacherHHome/Create
        public ActionResult Create()
        {
            var user = User.Identity.GetUserName();

            var theteacher = blogic.TeacherFromLogin(user);

            ViewBag.TeacherId = theteacher.Id.ToString();
            
            return View();
        }

        // POST: TeacherHHome/Create
        [HttpPost]
        public ActionResult Create(Course course)
        {
            try
            {
                // TODO: Add insert logic here
                blogic.CreateCourse(course);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherHHome/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: TeacherHHome/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(Course course)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    blogic.UpdateDbCourse(course);
                    return RedirectToAction("Index");
                }
                return View(course);
            }
            catch
            {
                return View(course);
            }
        }

        // GET: TeacherHHome/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: TeacherHHome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                // TODO: Add delete logic here
                blogic.DeleteCourse(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherHHome/Account/5
        public ActionResult Account(int? id)
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

        // POST: TeacherHHome/Account/5
        [HttpPost, ActionName("Account")]
        [ValidateAntiForgeryToken]
        public ActionResult AccountConfirm(Teacher teacher)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    blogic.UpdateDbTeacher(teacher);
                    return RedirectToAction("Index");
                }
                return View(teacher);
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseIndex
        public ActionResult CourseIndex(int? id)
        {
            Session["CourseID"] = id;

            var classstudents = blogic.StudentsInCourse(id);

            return View(classstudents);
        }

        // GET: TeacherHHome/DetailsStudent/5
        public ActionResult DetailsStudent(int? id)
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

        // GET: TeacherHHome/AddStudentCourse
        public ActionResult AddStudentCourse()
        {
            ViewBag.CourseId = Session["CourseID"];

            ViewBag.StudentID = blogic.GetSelectListStudenter(Convert.ToInt32(Session["CourseID"]));

            return View();
        }

        // POST: TeacherHHome/AddStudentCourse
        [HttpPost, ActionName("AddStudentCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudentCourseConfirm(StudentCourse studentcourse)
        {
            try
            {
                // TODO: Add insert logic here
                blogic.CreateStudentCourse(studentcourse);
                return RedirectToAction("CourseIndex", new { id = Session["CourseID"] });
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherHHome/EditStudentCourse/5
        public ActionResult EditStudentCourse(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int cId = Convert.ToInt32(Session["CourseID"]);

            var thestudentcourse = blogic.GetStudentCourse(id, cId);

            if (thestudentcourse == null)
            {
                return HttpNotFound();
            }

            return View(thestudentcourse);
        }

        // POST: TeacherHHome/EditStudentCourse/5
        [HttpPost, ActionName("EditStudentCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudentCourseConfirm(StudentCourse studentcourse)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    blogic.UpdateDbStudentCourse(studentcourse);
                    return RedirectToAction("CourseIndex", new { id = Session["CourseID"] });
                }
                return View(studentcourse);
            }
            catch
            {
                return View(studentcourse);
            }
        }

        // GET: TeacherHHome/DeleteStudentCourse/5
        public ActionResult DeleteStudentCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int cId = Convert.ToInt32(Session["CourseID"]);

            var thestudentcourse = blogic.GetStudentCourse(id, cId);

            if (thestudentcourse == null)
            {
                return HttpNotFound();
            }

            return View(thestudentcourse);
        }

        // POST: TeacherHHome/DeleteStudent/5
        [HttpPost, ActionName("DeleteStudentCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudentCourseConfirm(int id)
        {
            try
            {
                // TODO: Add delete logic here
                blogic.DeleteStudentCourse(id);
                return RedirectToAction("CourseIndex", new { id = Session["CourseID"] });
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherHHome/Assignments/5
        public ActionResult AssignmentsStudent(int? id)
        {

            Session["StudentID"] = id;

            return Redirect("/StudentAssignment/Index/" + Session["CourseID"]);

        }

        // GET: TeacherHHome/Assignments/5
        public ActionResult TeacherShared(int? id)
        {

           return Redirect("/TeacherShareds/Index/" + id.ToString()); // 

        }
    }
}
