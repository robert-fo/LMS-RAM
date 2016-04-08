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
		private IRepository repository;
		private ITeacherSharedRepository tsRepository;

		public TeacherSharedsController()
		{
			tsRepository = new TeacherSharedRepository();
			repository = new WorkingRepository();
		}

		public TeacherSharedsController(IRepository rep,
										ITeacherSharedRepository tsrep)
		{
			repository = rep;
			tsRepository = tsrep;
		}

		// ----------------------------------------------------------
		// Index
		//
		// inparameter is a courseId
		// ----------------------------------------------------------
		// GET: TeacherShareds
		[Authorize(Roles = "admin, teacher, student")]
		public ActionResult Index(int? id)
		{
			// Authenticate the User and Role
			// ---------------------
			int theUserId = 0;
			var user = User.Identity.GetUserName();
			bool isTeacher = User.IsInRole("teacher");
			bool isStudent = User.IsInRole("student");
			bool isAdmin = User.IsInRole("admin");

			var teacherShareds = tsRepository.GetAllTeacherShareds();
			IEnumerable<TeacherShared> tShareds = null;

			if (id != null)
			{
				if (isAdmin)
				{ 
					tShareds = from teacherShared in teacherShareds
							   where teacherShared.CourseId == id
							   orderby teacherShared.TeacherId,
									   teacherShared.CourseId,
									   teacherShared.Id
							   select teacherShared;
				}
				else if (isTeacher)
				{
					theUserId = this.getUserIdForStudentAndTecher();
					
					tShareds = from teacherShared in teacherShareds
							   where teacherShared.CourseId == id && 
								     teacherShared.TeacherId == theUserId
							   orderby teacherShared.CourseId,
									   teacherShared.Id
							   select teacherShared;
				}
				else if (isStudent)
				{
					theUserId = this.getUserIdForStudentAndTecher();

					var allSCourses = repository.GetAllStudentCourses();

					var sCourses = from sCourse in allSCourses
								   where sCourse.StudentId == theUserId &&
								   		 sCourse.CourseId == id
								   orderby sCourse.CourseId
								   select sCourse;

					tShareds = from teacherShared in teacherShareds
							   join sc in sCourses
							   on teacherShared.CourseId equals sc.CourseId
							   orderby teacherShared.CourseId, 
									   teacherShared.Id
							   select teacherShared;
				}
			}
			else // id == null
			{
				if (isAdmin)
				{
					tShareds = from teacherShared in teacherShareds
							   orderby teacherShared.CourseId, teacherShared.Id
							   select teacherShared;
				}
				else if (isTeacher)
				{
					theUserId = this.getUserIdForStudentAndTecher();

					tShareds = from teacherShared in teacherShareds
							   where teacherShared.TeacherId == theUserId
							   orderby teacherShared.CourseId,
									   teacherShared.Id
							   select teacherShared;
				} 
				else if (isStudent)
				{
					theUserId = this.getUserIdForStudentAndTecher();

					var allSCourses = repository.GetAllStudentCourses();
					var sCourses = from sCourse in allSCourses
								   where sCourse.StudentId == theUserId
								   orderby sCourse.CourseId
								   select sCourse;

					tShareds = from teacherShared in teacherShareds
							   join sc in sCourses
							   on teacherShared.CourseId equals sc.CourseId
							   orderby teacherShared.CourseId, teacherShared.Id
							   select teacherShared;
				} 
			}
			return View(tShareds.ToList());
		}

		// ----------------------------------------------------------
		// Details
		//
		// inparameter is a teacherSharedId
		// ----------------------------------------------------------
		// GET: TeacherShareds/Details/5
		[Authorize(Roles = "admin, teacher, student")]
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var teacherShared = tsRepository.getTeacherShared((int)id);

			if (teacherShared == null)
			{
				return HttpNotFound();
			}

			return View(teacherShared);
		}

		// ----------------------------------------------------------
		// Create
		//
		// ----------------------------------------------------------
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
				
				ViewBag.TeacherId = theUserId;
			}

			var coursesAll = repository.GetAllCourses();
			var teachersCourses = from course in coursesAll
								  where course.TeacherId == theUserId
								  orderby course.Id
								  select course;

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
						
						//db.TeacherShareds.Add(teacherShared);
						//db.SaveChanges();
						tsRepository.CreateTeacherShared(teacherShared);

						// store file
						string filePath = Path.Combine(Server.MapPath("~/Uploads/TeachersShared/"), teacherShared.TeacherId.ToString() + "_" +
							teacherShared.CourseId.ToString() + "_" +
							teacherShared.Id + "_" +
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

		// ----------------------------------------------------------
		// Edit
		//
		// inparameter is a teacherSharedId
		// ----------------------------------------------------------
		// GET: TeacherShareds/Edit/5
		[Authorize(Roles = "teacher")]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			int tsid = (int)id;
			//TeacherShared teacherShared = db.TeacherShareds.Find(id);
			TeacherShared teacherShared = tsRepository.getTeacherShared(tsid);
			if (teacherShared == null)
			{
				return HttpNotFound();
			}
			var coursesAll = repository.GetAllCourses();
			var teachersCourse = from course in coursesAll
								 where course.Id == id
								 select course;

			ViewBag.CourseId = new SelectList(teachersCourse, "Id", "Name", teacherShared.CourseId);
			// amh
			var teachers = tsRepository.GetAllTeacherShareds();
			ViewBag.TeacherId = new SelectList(teachers, "Id", "Teacher name", teacherShared.TeacherId);
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
			int length = FileName.FileName.Length;
			int index = FileName.FileName.LastIndexOf('\\') + 1;


			string path = FileName.FileName.Substring(0, index);
			string filename = FileName.FileName.Substring(index);

			//update database
			teacherShared.FileName = filename;
			//amh
			//db.Entry(teacherShared).State = System.Data.Entity.EntityState.Modified;
			//db.SaveChanges();
			tsRepository.UpdateTeacherShared(teacherShared);

			string oldFiles = teacherShared.TeacherId + "_" +
					teacherShared.CourseId + "_" +
					teacherShared.Id + "_*.pdf";

			try
			{
				// remove old file
				foreach (string DeleteFileName in Directory.EnumerateFiles
					(Server.MapPath("~/Uploads/TeachersShared/"), oldFiles))
				{
					System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads/TeachersShared/"), DeleteFileName));
				}

				//repository.CreateFile()
				if (FileName != null && FileName.ContentLength > 0)
				{
					string filePath = Path.Combine(Server.MapPath("~/Uploads/TeachersShared/"),
						teacherShared.TeacherId.ToString() + "_" +
						teacherShared.CourseId.ToString() + "_" +
						teacherShared.Id + "_" +
						Path.GetFileName(FileName.FileName));

					// store new file
					FileName.SaveAs(filePath);

					return RedirectToAction("Index", teacherShared.CourseId);
				}
			}
			catch (Exception e)
			{
				ViewBag.Error(e.Message);
				return RedirectToAction("Index", teacherShared.CourseId);
			}

			// amh
			//ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", teacherShared.CourseId);
			var courses = repository.GetAllCourses();
			ViewBag.CourseId = new SelectList(courses, "Id", "Name", teacherShared.CourseId);

			//amh
			//return RedirectToAction("Index", teacherShared.CourseId);
			var teachers = repository.GetAllTeachers();
			ViewBag.TeacherId = new SelectList(teachers, "Id", "SSN", teacherShared.TeacherId);
			return RedirectToAction("Index", teacherShared.CourseId);
		}

		// GET: TeacherShareds/Delete/5
		[Authorize(Roles = "admin, teacher")]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			//amh
			//TeacherShared teacherShared = db.TeacherShareds.Find(id);
			int tsid = (int)id;
			TeacherShared teacherShared = tsRepository.getTeacherShared(tsid);

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
			//TeacherShared teacherShared = db.TeacherShareds.Find(id);
			TeacherShared teacherShared = tsRepository.getTeacherShared((int)id);
			// store courseId in viewbag
			int theCourseId = teacherShared.CourseId;

			//amh
			//db.TeacherShareds.Remove(teacherShared);
			//db.SaveChanges();
			tsRepository.DeleteTeacherShared((int)id);
			

			// remove the file also
			string filePath = Path.Combine(Server.MapPath("~/Uploads/TeachersShared/"),
				teacherShared.TeacherId.ToString() + "_" +
				teacherShared.CourseId.ToString() + "_" +
				teacherShared.Id.ToString() + "_" +
				teacherShared.FileName);

			try
			{
				if (System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}
			}
			catch (Exception e)
			{
				ViewBag.Error(e.Message);
			}

			return RedirectToAction("Index", theCourseId);
		}

		[Authorize(Roles = "admin, teacher, student")]
		public ActionResult Download(int id)
		{
			//amh
			//TeacherShared teacherShared = db.TeacherShareds.Find(id);
			TeacherShared teacherShared = tsRepository.getTeacherShared((int)id);
			string filePath = Path.Combine(Server.MapPath("~/Uploads/TeachersShared/"),
											teacherShared.TeacherId.ToString() + "_" +
											teacherShared.CourseId.ToString() + "_" +
											teacherShared.Id + "_" +
											teacherShared.FileName);

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
				ViewBag.Error = e.Message;
				return RedirectToAction("Index");
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				tsRepository.Dispose();
			}
			base.Dispose(disposing);
		}


		protected int getUserIdForStudentAndTecher()
		{
			int theUserId = 0;
			var user = User.Identity.GetUserName();
			bool isTeacher = User.IsInRole("teacher");
			bool isStudent = User.IsInRole("student");
			
			if (isTeacher)
			{
				var teachersAll = repository.GetAllTeachers();
				var theUser = from teacher in teachersAll
								where teacher.UserName == user
								select teacher;
				theUserId = theUser.First().Id;
			}
			if (isStudent)
			{
				var studentsAll = repository.GetAllStudents();

				var theUser = from student in studentsAll
							  where student.UserName == user
							  select student;
				theUserId = theUser.First().Id;
			}

			return (theUserId);
		}
	}
}
