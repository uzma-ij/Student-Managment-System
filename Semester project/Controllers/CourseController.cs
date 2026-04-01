using Semester_project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Semester_project.Controllers
{
    public class CourseController : Controller
    {
        public StudentAttendenceDBEntities db = new StudentAttendenceDBEntities(); 
        // GET: Course
        public ActionResult Course()
        {

          var courses= db.Courses
         .Include(e => e.Department)
         .ToList();
            
            return View(courses);
        }
        public ActionResult Create()
        {
            
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseName,CreditHours,DepartmentId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Course");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }


        public ActionResult Edit(int id)
        {
            var course = db.Courses.Find(id);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        [HttpPost]

        public ActionResult Edit([Bind(Include = "CourseId,CourseName,CreditHours,DepartmentId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Course");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var course = db.Courses.Find(id); 
            if (course != null)
            {
                db.Courses.Remove(course);
                db.SaveChanges();
            }
            return RedirectToAction("Course"); 
        }

    }
}
