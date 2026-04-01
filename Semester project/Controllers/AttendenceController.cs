using Semester_project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semester_project.Controllers
{
    public class AttendenceController : Controller
    {

        public StudentAttendenceDBEntities db = new StudentAttendenceDBEntities(); // Replace with your actual DbContext name

        // GET: Attendence
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Student()
        {
            var students = db.Students.ToList(); 
            return View(students);
           
        }
      
      
        public ActionResult Create()
        {
            var departments = db.Departments.ToList(); 
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
         
            return View();
        }
   
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Student");
            }
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "DepartmentId", "DepartmentName", student.DepartmentId);
            return View(student);
        }
        public ActionResult Edit(int id)
        {
            var student = db.Students.Find(id);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            return View(student);
        }

        // POST: Edit
        [HttpPost]
     
        public ActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {

                db.Entry(std).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Student");
            }
            return View(std);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var student = db.Students.Find(id); 
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
            return RedirectToAction("Student"); 
        }

    }
}
