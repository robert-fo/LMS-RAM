﻿using LMS_RAM.Models;
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
            var studentsAll = repository.GetAllStudents();

            var user = User.Identity.GetUserName();

            var studenten = from student in studentsAll
                      where student.UserName == user
                      select student;

            ViewBag.StudentId = studenten.First().Id.ToString();

            var studentcourses = from course in studentcoursesAll
                                 where course.StudentId == studenten.First().Id
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

            var studentsAll = repository.GetAllStudents();

            var studenten = from student in studentsAll
                            where student.Id == id
                            select student;

            var thestudent = studenten.First();

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
                    repository.UpdateDbStudent(student);
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
    }
}