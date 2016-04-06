﻿using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Repository
{
    public class StudentRepository
    {
        private ApplicationDbContext db;

        public StudentRepository()
        {
            db = new ApplicationDbContext();
        }

        public List<Student> GetAllStudents()
        {
            var students = db.Students.ToList();

            return students;
        }

        public List<Assignment> GetAllAssignments()
        {
            var assignments = db.Assignments.ToList();

            return assignments;
        }

        public List<ScheduleItem> GetAllScheduleItems()
        {
            var scheduleItems = db.ScheduleItems.ToList();

            return scheduleItems;
        }

        public void CreateAssignment(Assignment assignment)
        {
            db.Assignments.Add(assignment);
            db.SaveChanges(); // Updates all changed objects
        }

        public void UpdateDbAssignment(Assignment assignment)
        {
            db.Entry(assignment).State = System.Data.Entity.EntityState.Modified; // Ej using för då blir det knas på retunr typerna i get metoderna...
            db.SaveChanges();  // Updates all changed objects  
        }

        public IEnumerable<SelectListItem> GetScheduleItemList(int id)
        {
            return (from t in db.ScheduleItems
                    where t.CourseId == id
                    select new SelectListItem() { Text = t.Name, Value = t.Id.ToString() }).ToList();
        }
    }
}