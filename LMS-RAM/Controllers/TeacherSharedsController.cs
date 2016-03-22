﻿using System;
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
    public class TeacherSharedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeacherShareds
        public ActionResult Index()
        {
            var teacherShareds = db.TeacherShareds.Include(t => t.Course).Include(t => t.Student);
            return View(teacherShareds.ToList());
        }

        // GET: TeacherShareds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherShared teacherShared = db.TeacherShareds.Find(id);
            if (teacherShared == null)
            {
                return HttpNotFound();
            }
            return View(teacherShared);
        }

        // GET: TeacherShareds/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            ViewBag.TeacherId = new SelectList(db.Students, "Id", "SSN");
            return View();
        }

        // POST: TeacherShareds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseId,TeacherId,Description,FileName")] TeacherShared teacherShared)
        {
            if (ModelState.IsValid)
            {
                db.TeacherShareds.Add(teacherShared);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", teacherShared.CourseId);
            ViewBag.TeacherId = new SelectList(db.Students, "Id", "SSN", teacherShared.TeacherId);
            return View(teacherShared);
        }

        // GET: TeacherShareds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherShared teacherShared = db.TeacherShareds.Find(id);
            if (teacherShared == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", teacherShared.CourseId);
            ViewBag.TeacherId = new SelectList(db.Students, "Id", "SSN", teacherShared.TeacherId);
            return View(teacherShared);
        }

        // POST: TeacherShareds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,TeacherId,Description,FileName")] TeacherShared teacherShared)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherShared).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", teacherShared.CourseId);
            ViewBag.TeacherId = new SelectList(db.Students, "Id", "SSN", teacherShared.TeacherId);
            return View(teacherShared);
        }

        // GET: TeacherShareds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherShared teacherShared = db.TeacherShareds.Find(id);
            if (teacherShared == null)
            {
                return HttpNotFound();
            }
            return View(teacherShared);
        }

        // POST: TeacherShareds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherShared teacherShared = db.TeacherShareds.Find(id);
            db.TeacherShareds.Remove(teacherShared);
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