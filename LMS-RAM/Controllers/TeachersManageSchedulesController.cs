using LMS_RAM.Models;
using LMS_RAM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeachersManageSchedulesController : Controller
    {
        private IRepository repository;

        public TeachersManageSchedulesController()
        {
            this.repository = new WorkingRepository();
        }

        public TeachersManageSchedulesController(IRepository Repository)
        {
            this.repository = Repository;
        }
        
        // GET: TeachersManageSchedules
        public ActionResult Index(int? id)
        {
            ViewBag.CourseId = id;

            var scheduleItemsAll = repository.GetAllScheduleItems();

            var tScheduleItems = from s in scheduleItemsAll
                           where s.CourseId == id
                           orderby s.StartTime
                           select s;

            return View(tScheduleItems);
        }

        // GET: TeachersManageSchedules/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                repository.CreateScheduleItems(scheduleitem);
                return RedirectToAction("Index/" + scheduleitem.CourseId.ToString());
            }
            catch
            {
                return View();
            }
        }

        // GET: TeachersManageSchedules/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeachersManageSchedules/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TeachersManageSchedules/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeachersManageSchedules/Delete/5
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
