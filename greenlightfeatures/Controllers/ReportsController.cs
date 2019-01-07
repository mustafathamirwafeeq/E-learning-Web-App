using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearning.Controllers
{
    public class ReportsController : Controller
    {
        GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
        // GET: Reports
        public ActionResult ListOfUsers(string role = "", bool isPDF = false)
        {
            List<SelectListItem> roles = new List<SelectListItem>();
            roles.Add(new SelectListItem { Text = "All", Value = "", Selected = true });
            roles.Add(new SelectListItem { Text = "Students", Value = "Student", Selected = true });
            roles.Add(new SelectListItem { Text = "Staffs", Value = "Moderator", Selected = true });
            roles.Add(new SelectListItem { Text = "Management", Value = "Management", Selected = true });
            ViewBag.Role = role;
            ViewBag.IsPDF = isPDF;
            ViewBag.Roles = new SelectList(roles, "Value", "Text", role);
            if (string.IsNullOrEmpty(role))
            {
                var model = db.AspNetUsers.OrderBy(x => x.UserName).ToList();
                return View(model);
            }
            else
            {
                var model = Utilities.GetInstance().GetUsersByRole(role).OrderBy(x => x.UserName).ToList();
                return View(model);
            }
        }
        public ActionResult ListOfUsersPDF(string role)
        {
            return new Rotativa.ActionAsPdf("ListOfUsers", new { role = role, isPDF = true }) { FileName = "List Of Users.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
        public ActionResult ListOfBases(bool isPDF = false)
        {
            ViewBag.IsPDF = isPDF;
            var model = db.TrainingBase.OrderBy(x => x.Name).ToList();
            return View(model);
        }
        public ActionResult ListOfBasesPDF()
        {
            return new Rotativa.ActionAsPdf("ListOfBases", new { isPDF = true }) { FileName = "List Of Bases.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
        public ActionResult ListOfCourses(bool isPDF = false)
        {
            ViewBag.IsPDF = isPDF;
            var model = db.Course.OrderBy(x => x.Title).ToList();
            return View(model);
        }
        public ActionResult ListOfCoursesPDF()
        {
            return new Rotativa.ActionAsPdf("ListOfCourses", new { isPDF = true }) { FileName = "List Of Courses.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
        public ActionResult ClassSchedule()
        {
            ViewBag.CourseClasses = new SelectList(db.CourseClass.ToList(), "Id", "Title");
            return View();
        }
        public ActionResult AttendanceDetails()
        {
            long id = Utilities.GetInstance().GetCurrentUserId();
            ViewBag.CourseClasses = new SelectList(db.CourseClass.Where(x => x.InstructorId == id).ToList(), "Id", "Title");
            return View();
        }
        public ActionResult StudentAttendanceDetails(int cid = 0)
        {
            long id = Utilities.GetInstance().GetCurrentUserId();
            var crsClasses = db.CourseClass.Where(x => x.InstructorId == id).ToList();
            cid = (cid == 0 && crsClasses.Count > 0) ? crsClasses.First().Id : cid;
            ViewBag.CourseClasses = new SelectList(crsClasses, "Id", "Title", cid);
            CourseClass cls = db.CourseClass.FirstOrDefault(x => x.Id == cid);
            if (cls != null)
            {
                List<int> stIDs = cls.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                ViewBag.Students = new SelectList(db.AspNetUsers.Where(x => stIDs.Contains(x.UserId)).OrderBy(x => x.FullName).ToList(), "UserId", "FullName");
            }
            ViewBag.CID = cid;
            return View();
        }
        public ActionResult StudentExamEvaluation(int sid = 0)
        {
            var students = Utilities.GetInstance().GetUsersByRole("Student");
            sid = (sid == 0 && students.Count > 0) ? students.First().UserId : sid;
            ViewBag.Students = new SelectList(students, "UserId", "FullName", sid);
            List<int> classIDs = Utilities.GetInstance().GetStudentClasses(sid.ToString()).ToList().Select(x => x.Id).ToList();
            ViewBag.Exams = new SelectList(db.Exam.Where(x => classIDs.Contains(x.CourseClassId)), "Id", "Title");
            ViewBag.SID = sid;
            return View();
        }
    }
}