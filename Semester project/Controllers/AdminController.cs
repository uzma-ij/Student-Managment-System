using System.Web.Mvc;
using Semester_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Semester_project.Controllers
{
    public class AdminController : Controller
    {
        private StudentAttendenceDBEntities db = new StudentAttendenceDBEntities(); // Replace with your actual DbContext name

        // GET: Admin/Signup
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Admin/Signup
        [HttpPost]

     
        public ActionResult Signup(Admin admin, string Password, string ConfirmPassword)
        {
            if (Password != ConfirmPassword)
            {
                ViewBag.ErrorMessage = "Passwords do not match.";
                return View(admin); 
            }

            try
            {
                
                admin.PasswordHash = Password;
                admin.CreatedAt = DateTime.Now;

                db.Admins.Add(admin);
                db.SaveChanges();

                // Redirecting to Login page 
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while signing up. Please try again.";
                return View(admin); 
            }
        }

        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        
        public ActionResult Login(string Email, string PasswordHash)
        {
            var admin = db.Admins.FirstOrDefault(a => a.Email == Email && a.PasswordHash == PasswordHash);

            if (admin != null)
            {
                Session["AdminId"] = admin.AdminId;
                return RedirectToAction("Home", "Attendence"); 
            }

            ViewBag.Error = "Invalid login credentials.";
            return View();
        }

    }
}
