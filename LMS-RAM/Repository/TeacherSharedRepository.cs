using LMS_RAM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Repository
{
   public class TeacherSharedRepository : ITeacherSharedRepository
    {
		private ApplicationDbContext db;

        public TeacherSharedRepository()
        {
			db = new ApplicationDbContext();
        }

		public TeacherShared getTeacherShared(int id)
		{
			return db.TeacherShareds.Find(id);
		}

        public List<TeacherShared> GetAllTeacherShareds()
        {
			//var teacherShareds = db.TeacherShareds.Include(t => t.Course).Include(t => t.Teacher);
			var teacherShareds = db.TeacherShareds.ToList();

			return teacherShareds;
        }

		public void CreateTeacherShared(TeacherShared teacherShared)
		{
			db.TeacherShareds.Add(teacherShared);
			db.SaveChanges(); // Updates all changed objects
		}

		public void DeleteTeacherShared(int id)
		{
			TeacherShared teacherShared = db.TeacherShareds.Find(id);
			db.TeacherShareds.Remove(teacherShared);
			db.SaveChanges(); // Updates all changed objects
		}

		public void UpdateTeacherShared(TeacherShared teacherShared)
		{
			// Ej using för då blir det knas på retunr typerna i get metoderna...
			db.Entry(teacherShared).State = System.Data.Entity.EntityState.Modified; 
			db.SaveChanges();  // Updates all changed objects  
		}
	}
}
