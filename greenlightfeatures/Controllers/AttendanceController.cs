using ELearning.Models;
using ELearning.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearning.Controllers
{
    public class AttendanceController : Controller
    {
        GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
        // GET: Attendance
        [Authorize(Roles = "Admin,Moderator,Student,Management")]
        public ActionResult Index(int id, DateTime? from = null, DateTime? to = null,bool isPDF = false)
        {
            var cls = db.CourseClass.FirstOrDefault(x => x.Id == id);
            List<DateTime> existingDates = new List<DateTime>();
            if (cls.ClassDates != null)
                existingDates = cls.ClassDates.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => string.IsNullOrEmpty(x) ? DateTime.MinValue : DateTime.Parse(x)).ToList();
            List<AttendanceDTO> attendances = new List<AttendanceDTO>();
            for (DateTime date = cls.StartDate; date <= cls.EndDate; date = date.AddDays(1))
            {
                if (!existingDates.Contains(date))
                    continue;
                var attendance = db.Attendance.FirstOrDefault(x => x.CourseClassId == id && x.AttendanceDate == date);
                if (attendance != null && attendance.StudentIds != "null" && attendance.StudentIds != null)
                {
                    attendances.Add(new AttendanceDTO { Date = date, TotalStudents = attendance.CourseClass.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count(), PresentStudents = attendance.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count(), Attendance = attendance });
                }
                else
                {
                    attendances.Add(new AttendanceDTO { Date = date, Attendance = null });
                }
            }
            ViewBag.ClassName = cls.Title + " Attendance Details";
            ViewBag.ID = id;
            ViewBag.IsPDF = isPDF;
            ViewBag.From = from;
            ViewBag.To = to;
            if (from != null)
                attendances.RemoveAll(x => x.Date < from.Value);
            if (to != null)
                attendances.RemoveAll(x => x.Date > to.Value);
            return View(attendances);
        }
        [Authorize(Roles = "Moderator")]
        public ActionResult AttendancePDF(int id, DateTime? from = null, DateTime? to = null)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            return new Rotativa.ActionAsPdf("Index", new {id=id,from=from,to=to, isPDF = true }) { FileName = "Class Attendance Details.pdf", PageMargins = Utilities.GetInstance().PDFMargins, Cookies = cookieCollection };
        }
        [Authorize(Roles = "Student,Moderator")]
        public ActionResult StudentIndex(int id, int sid = 0,bool isPDF = false)
        {
            string stID = sid == 0 ? Utilities.GetInstance().GetCurrentUserId().ToString() : sid.ToString();
            var cls = db.CourseClass.FirstOrDefault(x => x.Id == id);
            List<DateTime> existingDates = cls?.ClassDates?.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => string.IsNullOrEmpty(x) ? DateTime.MinValue : DateTime.Parse(x)).ToList();
            List<AttendanceDTO> attendances = new List<AttendanceDTO>();
            for (DateTime date = cls.StartDate; date <= cls.EndDate; date = date.AddDays(1))
            {
                if (existingDates == null || !existingDates.Contains(date))
                    continue;
                var attendance = db.Attendance.FirstOrDefault(x => x.CourseClassId == id && x.AttendanceDate == date);
                if (attendance != null)
                {
                    List<string> presentStudents = attendance.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    bool isPresent = presentStudents.Contains(stID);
                    attendances.Add(new AttendanceDTO { Date = date, TotalStudents = attendance.CourseClass.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count(), PresentStudents = presentStudents.Count, Attendance = attendance, IsPresent = isPresent });
                }
                else
                {
                    attendances.Add(new AttendanceDTO { Date = date, Attendance = null, IsPresent = false });
                }
            }
            ViewBag.ClassName = (isPDF ? (cls.Title + " Attendance Details for " + Utilities.GetInstance().GetUserByID(sid).FullName) : (cls.Title + " Attendance Details"));
            ViewBag.ID = id;
            ViewBag.SID = sid;
            ViewBag.IsPDF = isPDF;
            ViewBag.Pendings = double.Parse(attendances.Where(x => x.Attendance == null).Count().ToString());
            ViewBag.Presents = double.Parse(attendances.Where(x => x.Attendance != null && x.IsPresent).Count().ToString());
            ViewBag.Absents = double.Parse(attendances.Where(x => x.Attendance != null && !x.IsPresent).Count().ToString());
            return View(attendances);
        }
        [Authorize(Roles = "Moderator")]
        public ActionResult StudentAttendancePDF(int id, int sid = 0)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            return new Rotativa.ActionAsPdf("StudentIndex", new { id = id, sid = sid, isPDF = true }) { FileName = "Student Attendance Details.pdf", PageMargins = Utilities.GetInstance().PDFMargins, Cookies = cookieCollection };
        }
        [Authorize(Roles = "Moderator")]
        public ActionResult MarkInstructorAttendance(int? id)
        {
            var lst = db.CourseClass.Where(x => x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).ToList();
            ViewBag.ClassID = new SelectList(lst.ToList(), "Id", "Title", id);
            id = id ?? (lst.Count > 0 ? lst.First().Id : 0);
            var cls = db.CourseClass.FirstOrDefault(x => x.Id == id);
            List<AttendanceDTO> attendances = new List<AttendanceDTO>();
            if (cls != null)
            {
                List<DateTime> existingDates = new List<DateTime>();
                if (cls.ClassDates != null)
                    existingDates = cls.ClassDates.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => string.IsNullOrEmpty(x) ? DateTime.MinValue : DateTime.Parse(x)).ToList();
                for (DateTime date = cls.StartDate; date <= cls.EndDate; date = date.AddDays(1))
                {
                    if (!existingDates.Contains(date))
                        continue;
                    var attendance = db.Attendance.FirstOrDefault(x => x.CourseClassId == id && x.AttendanceDate == date);
                    if (attendance != null && attendance.StudentIds != "null" && attendance.StudentIds != null)
                    {
                        attendances.Add(new AttendanceDTO { Date = date, TotalStudents = attendance.CourseClass.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count(), PresentStudents = attendance.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count(), Attendance = attendance });
                    }
                    else
                    {
                        attendances.Add(new AttendanceDTO { Date = date, Attendance = null });
                    }
                }
                ViewBag.ClassName = cls.Title + " attendance details";
            }
            ViewBag.ID = id;
            return View(attendances);
        }
        public ActionResult ChangeInstructorAttendance(int aid, int cid, DateTime? date)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            Attendance at = db.Attendance.FirstOrDefault(x => x.Id == aid);
            if (at != null)
            {
                at.IsInstructorPresent = !at.IsInstructorPresent;
                at.CreateDate = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                Attendance atNew = new Attendance { StudentIds = "", IsInstructorPresent = true, isAllPresent = false, CreateDate = DateTime.Now, AttendanceDate = date.Value, CourseClassId = cid };
                db.Attendance.Add(atNew);
                db.SaveChanges();
            }
            return RedirectToAction("MarkInstructorAttendance", new { id = cid });
        }
        [Authorize(Roles = "Admin,Moderator,Student")]
        public ActionResult Edit(DateTime date, int id)
        {
            var attendance = db.Attendance.FirstOrDefault(x => x.CourseClassId == id && x.AttendanceDate == date);
            var students = Utilities.GetInstance().GetUsersByRole("Student");
            List<int> addedIds = attendance.CourseClass.StudentIds.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();
            List<int> addedIds1 = attendance.StudentIds.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();
            var allStudents = students.Where(x => addedIds.Contains(x.UserId)).ToList();
            ViewBag.AllStudents = allStudents;
            ViewBag.AddedStudents = students.Where(x => addedIds1.Contains(x.UserId)).ToList();
            ViewBag.Students = new SelectList(allStudents.Select(x => new { UserId = x.UserId, UserName = x.UserName + "-" + x.FullName }), "UserId", "UserName");
            ViewBag.Title = attendance.CourseClass.Title + " attendance - " + attendance.AttendanceDate.ToShortDateString() + " - " + attendance.AttendanceDate.ToString("dddd");
            ViewBag.ID = attendance.Id;
            ViewBag.ClassID = attendance.CourseClassId;
            return View(attendance);
        }
        [Authorize(Roles = "Admin,Moderator")]
        public JsonResult SaveAttendance(int id, string studentIds, int? clsId, DateTime? date, bool IsInstructorPresent)
        {
            bool res = false;
            Attendance attendance = db.Attendance.FirstOrDefault(x => x.Id == id);
            if (attendance == null && clsId.HasValue && date.HasValue)
            {
                attendance = db.Attendance.FirstOrDefault(x => x.CourseClassId == clsId && x.AttendanceDate == date.Value);
            }
            try
            {
                if (attendance != null)
                {
                    attendance.StudentIds = studentIds;
                    attendance.IsInstructorPresent = IsInstructorPresent;
                    attendance.CreateDate = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    Attendance at = new Attendance { CreateDate = DateTime.Now, CourseClassId = clsId.Value, AttendanceDate = date.Value, StudentIds = studentIds, IsInstructorPresent = IsInstructorPresent };
                    db.Attendance.Add(at);
                    db.SaveChanges();
                }
                res = true;
            }
            catch
            {
                res = false;
            }
            if (res)
                return Json(new { status = "0", data = "Attendance has been updated" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "1", data = "Please contact system administrator" }, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Create(DateTime date, int id, string type = "All")
        {
            var crsClass = db.CourseClass.FirstOrDefault(x => x.Id == id);

            var students = Utilities.GetInstance().GetUsersByRole("Student");
            List<int> addedIds = crsClass.StudentIds.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();
            var allStudents = students.Where(x => addedIds.Contains(x.UserId)).ToList();
            ViewBag.AllStudents = allStudents;
            ViewBag.AddedStudents = new List<AspNetUsers>();
            ViewBag.Students = new SelectList(allStudents.Select(x => new { UserId = x.UserId, UserName = x.UserName + "-" + x.FullName }), "UserId", "UserName");
            ViewBag.Title = crsClass.Title + " attendance - " + date.ToShortDateString() + " - " + date.ToString("dddd");
            ViewBag.ID = id;
            ViewBag.ClassID = id;
            ViewBag.Date = date;
            return View();
        }
    }
}