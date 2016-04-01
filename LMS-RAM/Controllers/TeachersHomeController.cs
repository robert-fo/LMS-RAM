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

            ViewBag.TeacherId = lararen.First().Id.ToString();

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

        // GET: TeacherHHome/Account/5
        public ActionResult Account(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teachersAll = repository.GetAllTeachers();

            var lararen = from teacher in teachersAll
                            where teacher.Id == id
                            select teacher;

            var theteacher = lararen.First();

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
                    repository.UpdateDbTeacher(teacher);
                    return RedirectToAction("Index");
                }
                return View(teacher);
            }
            catch
            {
                return View();
            }
        }

        // GET: ClassIndex
        public ActionResult ClassIndex(int? id)
        {
            Session["CourseID"] = id;

            var studentcoursesAll = repository.GetAllStudentCourses();
            var studentsAll = repository.GetAllStudents();

            var sCourses = from sCourse in studentcoursesAll
                           where sCourse.CourseId == id
                           orderby sCourse.Id
                           select sCourse;

            List<Student> classstudents = new List<Student>();

            foreach (var item in sCourses)
            {
                foreach (var sitem in studentsAll)
                {
                    if (sitem.Id == item.StudentId)
                    {
                        classstudents.Add(sitem);
                    }
                }
            }

            return View(classstudents);
        }

        // GET: TeacherHHome/DetailsStudent/5
        public ActionResult DetailsStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var studentsAll = repository.GetAllStudents();

            //coursesAll.Find

            var tstudents = from s in studentsAll
                           where s.Id == id
                           select s;

            var thestudent = tstudents.First();

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
            ViewBag.StudentID = repository.GetSelectListStudenter(Convert.ToInt32(Session["CourseID"]));

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
                repository.CreateStudentCourse(studentcourse);
                return RedirectToAction("ClassIndex", new { id = Session["CourseID"] });
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

            var studentCoursesAll = repository.GetAllStudentCourses();
            int cId = Convert.ToInt32(Session["CourseID"]);

            var tCourses = from s in studentCoursesAll
                           where s.StudentId == id
                           && s.CourseId == cId
                           select s;

            var thestudentcourse = tCourses.First();

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
                    repository.UpdateDbStudentCourse(studentcourse);
                    return RedirectToAction("ClassIndex", new { id = Session["CourseID"] });
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

            var studentCoursesAll = repository.GetAllStudentCourses();
            int cId = Convert.ToInt32(Session["CourseID"]);

            var tsCourses = from s in studentCoursesAll
                           where s.StudentId == id
                           && s.CourseId == cId
                           select s;

            var thestudentcourse = tsCourses.First();

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
                repository.DeleteStudentCourse(id);
                return RedirectToAction("ClassIndex", new { id = Session["CourseID"] });
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

            return Redirect("/StudentAss/Index/" + Session["CourseID"]);

        }

        // GET: TeacherHHome/Assignments/5
        public ActionResult TeacherShared(int? id)
        {

           return Redirect("/TeacherShareds/Index/" + id.ToString()); // 

        }
    }
}
