using Semester_project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semester_project.Controllers
{
    public class DepartmentController : Controller
    {
        private StudentAttendenceDBEntities db = new StudentAttendenceDBEntities();
        // GET: Department
        public ActionResult Department()
        {
            var department = db.Departments.ToList();
            return View(department);
           
        }
        //GET
        public ActionResult Create()
        {
            return View();
        }
        //POST 
        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Department");
            }
            return View(department);
        }
        public ActionResult Edit(int id)
        {
            var department = db.Departments.Find(id);
            return View(department);
        }

        [HttpPost]
      
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {

                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Department");
            }
            return View(department);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var department = db.Departments.Find(id); 
            if (department != null)
            {
                db.Departments.Remove(department);
                db.SaveChanges();
            }
            return RedirectToAction("Department"); 
        }


    }
}