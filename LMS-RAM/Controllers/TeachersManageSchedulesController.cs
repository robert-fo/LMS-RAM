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
    public class TeachersManageSchedulesController : Controller
    {
        private BusinessLogic blogic;

        public TeachersManageSchedulesController()
        {
            this.blogic = new BusinessLogic();
        }

        public TeachersManageSchedulesController(IRepository Repository)
        {
            this.blogic = new BusinessLogic(Repository);
        }
        
        // GET: TeachersManageSchedules
        public ActionResult Index(int? id)
        {
            ViewBag.CourseId = id;

            var tScheduleItems = blogic.CourseSchedule(id);

            return View(tScheduleItems);
        }

        // GET: TeachersManageSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var theScheduleItem = blogic.GetScheduleItem(id);

            if (theScheduleItem == null)
            {
                return HttpNotFound();
            }

            return View(theScheduleItem);
        }

        // GET: TeachersManageSchedules/Create
        public ActionResult Create(int? id)
        {
            ViewBag.CourseId = id;

            return View();
        }

        // POST: TeachersManageSchedules/Create
        [HttpPost]
        public ActionResult Create(ScheduleItem scheduleitem)
        {
            try
            {
                // TODO: Add insert logic here
                blogic.CreateScheduleItem(scheduleitem);
                return RedirectToAction("Index/" + scheduleitem.CourseId.ToString());
            }
            catch
            {
                return View();
            }
        }

        // GET: TeachersManageSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var theScheduleItem = blogic.GetScheduleItem(id);

            if (theScheduleItem == null)
            {
                return HttpNotFound();
            }

            return View(theScheduleItem);
        }

        // POST: TeachersManageSchedules/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(ScheduleItem scheduleitem)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    blogic.UpdateDbScheduleItem(scheduleitem);
                    return RedirectToAction("Index/" + scheduleitem.CourseId.ToString());
                }
                return View(scheduleitem);
            }
            catch
            {
                return View(scheduleitem);
            }
        }

        // GET: TeachersManageSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var theScheduleItem = blogic.GetScheduleItem(id);

            if (theScheduleItem == null)
            {
                return HttpNotFound();
            }

            ViewBag.CourseId = theScheduleItem.CourseId.ToString();

            return View(theScheduleItem);
        }

        // POST: TeachersManageSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var theScheduleItem = blogic.GetScheduleItem(id);

            var CourseID = theScheduleItem.CourseId.ToString();

            try
            {
                // TODO: Add delete logic here
                blogic.DeleteScheduleItem(id);
                return RedirectToAction("Index/" + CourseID);
            }
            catch
            {
                return View();
            }
        }
    }
}
