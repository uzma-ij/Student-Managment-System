using Semester_project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Semester_project.Controllers
{
    
    public class EnrollmentController : Controller
    {

        private StudentAttendenceDBEntities db = new StudentAttendenceDBEntities();
       
        // GET: Enrollment
        public ActionResult Enrollment()
        {
            var enrollments = db.Enrollments
        .Include(e => e.Student)
        .Include(e => e.Course)
        .ToList();
           return View(enrollments);
        }

        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FullName");
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,CourseId,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Enrollment");
            }

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FullName", enrollment.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", enrollment.CourseId);
            return View(enrollment);
        }

        public ActionResult Edit(int? id)
        {
   
          
            Enrollment enrollment = db.Enrollments.Find(id);
            

            // For dropdowns
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FullName", enrollment.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", enrollment.CourseId);

            return View(enrollment);
        }

        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "EnrollmentId,StudentId,CourseId,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Enrollment");
            }

            // If model is invalid, refill dropdowns
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FullName", enrollment.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", enrollment.CourseId);

            return View(enrollment);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var enrollment = db.Enrollments.Find(id); 
            if (enrollment != null)
            {
                db.Enrollments.Remove(enrollment);
                db.SaveChanges();
            }
            return RedirectToAction("Enrollment"); 
        }


    }
}
