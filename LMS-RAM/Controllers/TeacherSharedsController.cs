using LMS_RAM.Models;
using LMS_RAM.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace LMS_RAM.Controllers
{

    public class TeacherSharedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
		private IRepository repository;

		public TeacherSharedsController ()
		{
			repository = new WorkingRepository();
		}

		public TeacherSharedsController (IRepository rep)
		{
			repository = rep;
		}

        // GET: TeacherShareds
		[Authorize(Roles = "admin, teacher, student")]
		// inparameter is a courseId
        public ActionResult Index(int? id) 
        {
			// Authenticate the User and Role
			// ---------------------
			int theUserId = 0;
			var user = User.Identity.GetUserName();
			bool isTeacher = User.IsInRole("teacher");
			bool isStudent = User.IsInRole("student");
			bool isAdmin = User.IsInRole("admin");

			// select the teachers all Shared files
			// the courseId (id is null)
			if (id == null)
			{
				// check Session and ViewBag data
				if (id == null)
				{
					// check if Session variable is set
					try
					{
						id = int.Parse(Session["CourseId"].ToString());
						if (id == null)
						{
							id = ViewBag.CourseId;
						}
					}
					catch (Exception e)
					{
						ViewBag.Error(e.Message);
					}
					
				}
				if (isTeacher)
				{
					var teachersAll = repository.GetAllTeachers();

					var theUser = from teacher in teachersAll
								  where teacher.UserName == user
								  select teacher;
					ViewBag.TeacherId = theUser.First().Id.ToString();
					theUserId = theUser.First().Id;
				}
				else if (isStudent)
				{
					var studentsAll = repository.GetAllStudents();

					var theUser = from student in studentsAll
								  where student.UserName == user
								  select student;
					ViewBag.StudentId = theUser.First().Id.ToString();
					theUserId = theUser.First().Id;
				}
			}

			//var tShareds = repository.GetTeacherShareds(theUserId);
            var teacherShareds = 
				db.TeacherShareds.Include(t => t.Course).Include(t => t.Teacher);

			IEnumerable<TeacherShared> tShareds;

			if (isTeacher || isStudent)
			{
				if (id != null )
				{
					tShareds = from teacherShared in teacherShareds
							   where teacherShared.CourseId == id
							   orderby teacherShared.CourseId, teacherShared.Id
							   select teacherShared;
					ViewBag.CourseId = id.ToString();
				}
				else 
				{
					tShareds = from teacherShared in teacherShareds
							   where teacherShared.TeacherId == theUserId
							   orderby teacherShared.CourseId, teacherShared.Id
							   select teacherShared;			
				}
			}
			else 
			{
				if (isAdmin)
				{
					tShareds = from teacherShared in teacherShareds
							   orderby teacherShared.CourseId, teacherShared.Id
							   select teacherShared;
				}
				else
				{
					tShareds = new List<TeacherShared> ();
					ViewBag.Error = "No courseId found";
				}
			}
			
			return View(tShareds.ToList());
        }

        // GET: TeacherShareds/Details/5
		[Authorize(Roles = "admin, teacher, student")]
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
		[Authorize(Roles = "teacher")]
        public ActionResult Create()
        {
			int theUserId = 0;

			// Authenticate the User and Role
			// ---------------------
			var user = User.Identity.GetUserName();
			bool isTeacher = User.IsInRole("teacher");
	
			// select the teachers all Shared files
			if (isTeacher)
			{
				var teachersAll = repository.GetAllTeachers();

				var theUser = from teacher in teachersAll
							  where teacher.UserName == user
							  select teacher;
	
				theUserId = theUser.First().Id;
				ViewBag.TeacherId = new SelectList(theUser, "Id", "SSN");
			}

			var coursesAll = repository.GetAllCourses();
			var teachersCourses = from course in coursesAll
                                  where course.TeacherId == theUserId
								  orderby course.Id
                                  select course;

            //ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
			ViewBag.CourseId = new SelectList(teachersCourses, "Id", "Name");
			
            return View();
        }

        // POST: TeacherShareds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
		//[ValidateAntiForgeryToken]
		[Authorize(Roles = "teacher")]
		public ActionResult Create([Bind(Include = "Id,CourseId,TeacherId,Description,FileName")] TeacherShared teacherShared, HttpPostedFileBase FileName)
        {
			try
			{
				//repository.CreateFile()
				if (FileName != null && FileName.ContentLength > 0)
				{		
					if (ModelState.IsValid)
					{	
						teacherShared.FileName = Path.GetFileName(FileName.FileName);
						db.TeacherShareds.Add(teacherShared);
						db.SaveChanges();

						// store file
						string filePath = Path.Combine(Server.MapPath("~/Uploads/TeachersShared/"), teacherShared.TeacherId.ToString() + "_" + 
							teacherShared.CourseId.ToString() + "_" + 
							teacherShared.Id + "_"+ 
							Path.GetFileName(FileName.FileName));

						FileName.SaveAs(filePath);
						
						return RedirectToAction("Index");
					}
					
				}
			}
			catch
			{
				return View();
				//return RedirectToAction("Index");
			}
			return RedirectToAction("Index", teacherShared.CourseId);
        }

        // GET: TeacherShareds/Edit/5
		[Authorize(Roles = "teacher")]
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
			var coursesAll = repository.GetAllCourses();
			var teachersCourse = from course in coursesAll
								 where course.Id == id
								 select course;
			
            ViewBag.CourseId = new SelectList(teachersCourse, "Id", "Name", teacherShared.CourseId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "SSN", teacherShared.TeacherId);
            return View(teacherShared);
        }

        // POST: TeacherShareds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "teacher")]
		public ActionResult Edit([Bind(Include = "Id,CourseId,TeacherId,Description,FileName")] TeacherShared teacherShared, HttpPostedFileBase FileName)
        {
   			try
			{
				//repository.CreateFile()
				if (FileName != null && FileName.ContentLength > 0)
				{
					string filePath = Path.Combine(Server.MapPath("~/Uploads/TeachersShared/"), 
						teacherShared.TeacherId.ToString() + "_" +
						teacherShared.CourseId.ToString() + "_" + 
						teacherShared.Id + "_" + 
						Path.GetFileName(FileName.FileName));
					
					if (!System.IO.File.Exists(filePath))
					{
						if (ModelState.IsValid)
						{						
							// remove old file
							if (System.IO.File.Exists(filePath))
							{
								System.IO.File.Delete(filePath);
							}
							// store new file
							FileName.SaveAs(filePath);
							teacherShared.FileName = Path.GetFileName(FileName.FileName);
							
							// update database
							db.Entry(teacherShared).State = EntityState.Modified;
							db.SaveChanges();
							return RedirectToAction("Index", teacherShared.CourseId);
						}

					}
				}
			}
			catch
			{
				return RedirectToAction("Index", teacherShared.CourseId);
			}
			return RedirectToAction("Index", teacherShared.CourseId);

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", teacherShared.CourseId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "SSN", teacherShared.TeacherId);
            return RedirectToAction("Index", teacherShared.CourseId );
        }

        // GET: TeacherShareds/Delete/5
		[Authorize(Roles = "admin, teacher")]
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
		[Authorize(Roles = "admin, teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {	
			// remove from db
            TeacherShared teacherShared = db.TeacherShareds.Find(id);
			// store courseId in viewbag
			int theCourseId = teacherShared.CourseId;
			
            db.TeacherShareds.Remove(teacherShared);
            db.SaveChanges();

			// remove the file also
			string filePath = Path.Combine(Server.MapPath("~/Uploads/TeachersShared/"), teacherShared.TeacherId.ToString() + "_" + teacherShared.CourseId.ToString() + "_" + teacherShared.FileName);

			try
			{
				if (System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}
			}
			catch (Exception e)
			{
				int ett = 1;
			}

            return RedirectToAction("Index", theCourseId);
        }

		[Authorize(Roles = "admin, teacher, student")]
		public ActionResult Download(int id)
		{
			TeacherShared teacherShared = db.TeacherShareds.Find(id);
			string filePath = Path.Combine(Server.MapPath("~/Uploads/TeachersShared/"), teacherShared.TeacherId.ToString() + "_" + teacherShared.CourseId.ToString() + "_" + teacherShared.FileName);
			
			try
			{
				if (System.IO.File.Exists(filePath))
				{
					return File(filePath, "application/pdf");
				}
				else 
				{
					return HttpNotFound("File not found");
				}
			}
			catch (Exception e)
			{
				return RedirectToAction("Index");
			}		
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
