using LMS_RAM.Models;
using LMS_RAM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeachersManageStudentsController : Controller
    {
        private BusinessLogic blogic;

        public TeachersManageStudentsController()
        {
            this.blogic = new BusinessLogic();
        }

        public TeachersManageStudentsController(IRepository Repository)
        {
            this.blogic = new BusinessLogic(Repository);
        }

        // GET: TeachersManageStudents
        public ActionResult Index()
        {
            var studentsAll = blogic.GetAllStudents();

            return View(studentsAll);
        }

        // GET: TeachersManageStudents/Details/5
        public ActionResult Details(int? id)
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

        // GET: TeachersManageStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeachersManageStudents/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                // TODO: Add insert logic here
                blogic.CreateStudent(student);

                var user = new ApplicationUser { Email = student.UserName, UserName = student.UserName };

                blogic.AddStudentUser(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TeachersManageStudents/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: TeachersManageStudents/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(Student student)
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
                return View(student);
            }
        }

        // GET: TeachersManageStudents/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: TeachersManageStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                // TODO: Add delete logic here
                blogic.DeleteStudent(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
