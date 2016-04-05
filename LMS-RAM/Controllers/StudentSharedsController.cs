using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS_RAM.Models;

namespace LMS_RAM.Controllers
{
    [Authorize(Roles = "admin")]
    public class StudentSharedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentShareds
        public ActionResult Index()
        {
            var studentShareds = db.StudentShareds.Include(s => s.Course).Include(s => s.Student);
            return View(studentShareds.ToList());
        }

        // GET: StudentShareds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentShared studentShared = db.StudentShareds.Find(id);
            if (studentShared == null)
            {
                return HttpNotFound();
            }
            return View(studentShared);
        }

        // GET: StudentShareds/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "SSN");
            return View();
        }

        // POST: StudentShareds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseId,StudentId,Description,FileName")] StudentShared studentShared)
        {
			// to include in code
			//bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

			//if (!exists)
			//	System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            if (ModelState.IsValid)
            {
                db.StudentShareds.Add(studentShared);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", studentShared.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "SSN", studentShared.StudentId);
            return View(studentShared);
        }

        // GET: StudentShareds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentShared studentShared = db.StudentShareds.Find(id);
            if (studentShared == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", studentShared.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "SSN", studentShared.StudentId);
            return View(studentShared);
        }

        // POST: StudentShareds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,StudentId,Description,FileName")] StudentShared studentShared)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentShared).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", studentShared.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "SSN", studentShared.StudentId);
            return View(studentShared);
        }

        // GET: StudentShareds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentShared studentShared = db.StudentShareds.Find(id);
            if (studentShared == null)
            {
                return HttpNotFound();
            }
            return View(studentShared);
        }

        // POST: StudentShareds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentShared studentShared = db.StudentShareds.Find(id);
            db.StudentShareds.Remove(studentShared);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
