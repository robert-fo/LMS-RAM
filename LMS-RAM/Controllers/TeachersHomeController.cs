﻿using LMS_RAM.Repository;
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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var coursesAll = repository.GetAllCourses();

            //coursesAll.Find

            var tCourses = from course in coursesAll
                           where course.Id == id
                           select course;

            var thecourse = tCourses.First();

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
            var teachersAll = repository.GetAllTeachers();

            var lararen = from teacher in teachersAll
                          where teacher.UserName == user
                          select teacher;

            ViewBag.TeacherId = lararen.First().Id.ToString();
            
            return View();
        }

        // POST: TeacherHHome/Create
        [HttpPost]
        public ActionResult Create(Course course)
        {
            try
            {
                // TODO: Add insert logic here
                repository.CreateCourse(course);
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

            var coursesAll = repository.GetAllCourses();

            var tCourses = from course in coursesAll
                           where course.Id == id
                           select course;

            var thecourse = tCourses.First();

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
                    repository.UpdateDbCourse(course);
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

            var coursesAll = repository.GetAllCourses();

            //coursesAll.Find

            var tCourses = from course in coursesAll
                           where course.Id == id
                           select course;

            var thecourse = tCourses.First();

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
                repository.DeleteCourse(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
