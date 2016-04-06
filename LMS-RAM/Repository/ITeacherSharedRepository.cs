using LMS_RAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_RAM.Repository
{
   public interface ITeacherSharedRepository
    {
        List<TeacherShared> GetAllTeacherShareds();
		TeacherShared getTeacherShared(int id);
		void CreateTeacherShared(TeacherShared teacherShared);
		void DeleteTeacherShared(int id);
		void UpdateTeacherShared(TeacherShared teacherShared);
	}
}
