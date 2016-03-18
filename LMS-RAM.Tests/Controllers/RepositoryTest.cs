using LMS_RAM;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LMS_RAM.Repository;
using LMS_RAM.Models;
using System.Collections.Generic;

namespace LMS_RAM.Tests.Controllers
{
	[TestClass]
	public class RepositoryTest
	{
		[TestMethod]
		public void GetAllTeachers()
		{
			WorkingRepository repository = new WorkingRepository();

			List<Teacher> teachers = repository.GetAllTeachers();

			// Assert
			Assert.AreEqual(2, teachers.Count);

		}

		[TestMethod]
		public void GetAllStudentsForCourse()
		{
			WorkingRepository repository = new WorkingRepository();

			List<Student> students = repository.GetAllStudentsForCourse(3);

			// Assert
			Assert.AreEqual(2, students.Count);

		}
	}
}
