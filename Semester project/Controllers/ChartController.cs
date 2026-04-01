using Semester_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semester_project.Controllers
{
    public class ChartController : Controller
    {
        private StudentAttendenceDBEntities db = new StudentAttendenceDBEntities(); // Replace with your actual DbContext name
        // GET: Chart
        public ActionResult Chart()
        {
            return View();
        }

        public JsonResult GetChartData()
        {
            var data = new[]
            {
                new { Label = "Admins", Count = db.Admins.Count() },
                new { Label = "Students", Count = db.Students.Count() },
                new { Label = "Departments", Count = db.Departments.Count() },
                new { Label = "Courses", Count = db.Courses.Count() },
                new { Label = "Enrollments", Count = db.Enrollments.Count() }
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}