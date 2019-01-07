using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELearning.Models;
using ELearning.Models.Dto;
using ELearning.Models.Enum;

namespace ELearning.Controllers
{
    public class CourseClassesController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: CourseClasses
        public async Task<ActionResult> Index(int? id,bool isPDF = false)
        {
            ViewBag.ID = id ?? 0;
            ViewBag.IsPDF = isPDF;
            if (id != null)
            {
                if (User.IsInRole("Student"))
                {
                    var courseClass = Utilities.GetInstance().GetStudentClasses(id.ToString());
                    return View(courseClass);
                }
                else
                {
                    var courseClass = db.CourseClass.Where(x => x.InstructorId == id && x.Status && x.StartDate < DateTime.Now).Include(c => c.Course).Include(c => c.TrainingBase).OrderByDescending(x => x.Status).ThenByDescending(x => x.StartDate).ThenByDescending(x => x.StartTime);
                    return View(await courseClass.ToListAsync());
                }
            }
            else
            {
                var courseClass = db.CourseClass.Include(c => c.Course).Include(c => c.TrainingBase).OrderByDescending(x => x.Status).ThenByDescending(x => x.StartDate).ThenByDescending(x => x.StartTime);
                return View(await courseClass.ToListAsync());
            }
        }
        public ActionResult ListOfClassesPDF()
        {
            return new Rotativa.ActionAsPdf("Index", new {id=Utilities.GetInstance().GetCurrentUserId(), isPDF = true }) { FileName = "List Of Classes.pdf", PageMargins = Utilities.GetInstance().PDFMargins};
        }
        // GET: CourseClasses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseClass courseClass = await db.CourseClass.FindAsync(id);
            if (courseClass == null)
            {
                return HttpNotFound();
            }
            return View(courseClass);
        }

        // GET: CourseClasses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Course.Where(x => x.IsPublished && !x.IsDeleted), "Id", "Title");
            ViewBag.TrainingBaseId = new SelectList(db.TrainingBase, "Id", "Name");
            ViewBag.InstructorId = new SelectList(Utilities.GetInstance().GetUsersByRole("Moderator"), "UserId", "UserName");
            return View();
        }

        // POST: CourseClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TrainingBaseId,CourseId,InstructorId,StudentId,Status,AddedDate,StartDate,EndDate,StartTime,EndTime,Title")] CourseClass courseClass)
        {
            if (ModelState.IsValid)
            {
                if (db.CourseClass.FirstOrDefault(x => x.Title.ToLower().Trim() == courseClass.Title.ToLower().Trim()) == null)
                {
                    courseClass.StudentIds = "";
                    courseClass.AddedDate = DateTime.Now;
                    db.CourseClass.Add(courseClass);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Title", "Title already exists.");
                }
            }

            ViewBag.CourseId = new SelectList(db.Course.Where(x => x.IsPublished && !x.IsDeleted), "Id", "Title", courseClass.CourseId);
            ViewBag.TrainingBaseId = new SelectList(db.TrainingBase, "Id", "Name", courseClass.TrainingBaseId);
            ViewBag.InstructorId = new SelectList(Utilities.GetInstance().GetUsersByRole("Moderator"), "UserId", "UserName");
            return View(courseClass);
        }

        // GET: CourseClasses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseClass courseClass = await db.CourseClass.FindAsync(id);
            if (courseClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Course, "Id", "Title", courseClass.CourseId);
            ViewBag.TrainingBaseId = new SelectList(db.TrainingBase, "Id", "Name", courseClass.TrainingBaseId);
            ViewBag.InstructorId = new SelectList(Utilities.GetInstance().GetUsersByRole("Moderator"), "UserId", "UserName", courseClass.InstructorId);
            return View(courseClass);
        }

        // POST: CourseClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TrainingBaseId,CourseId,InstructorId,StudentIds,Status,AddedDate,StartDate,EndDate,StartTime,EndTime,Title")] CourseClass courseClass)
        {
            if (ModelState.IsValid)
            {
                courseClass.StudentIds = courseClass.StudentIds ?? "";
                db.Entry(courseClass).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Course, "Id", "Title", courseClass.CourseId);
            ViewBag.TrainingBaseId = new SelectList(db.TrainingBase, "Id", "Name", courseClass.TrainingBaseId);
            ViewBag.InstructorId = new SelectList(Utilities.GetInstance().GetUsersByRole("Moderator"), "UserId", "UserName", courseClass.InstructorId);
            return View(courseClass);
        }

        // GET: CourseClasses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseClass courseClass = await db.CourseClass.FindAsync(id);
            if (courseClass == null)
            {
                return HttpNotFound();
            }
            return View(courseClass);
        }

        // POST: CourseClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseClass courseClass = await db.CourseClass.FindAsync(id);
            db.CourseClass.Remove(courseClass);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// View latest 10 users evalution 
        /// </summary>
        [AllowAnonymous]
        public async Task<ActionResult> RecentClasses()
        {
            IQueryable<CourseClass> classes;
            if (User.IsInRole("Moderator"))
            {
                int instructorID = Utilities.GetInstance().GetCurrentUserId();
                classes = db.CourseClass.Where(x => x.InstructorId == instructorID && x.Status).OrderByDescending(r => r.Id).Take(10);
            }
            else
                classes = db.CourseClass.OrderByDescending(r => r.Id).Take(10);
            return PartialView("_RecentAllClasses", await classes.ToListAsync());

        }

        /// <summary>
        /// View selected & non-selected question for provided course & exam
        /// </summary>
        public ActionResult AddStudents(int id, FormCollection form)
        {
            bool isInactive = form["isInactive"] != null && form["isInactive"].ToString() == "on" ? true : false;
            int userId = Utilities.GetInstance().GetCurrentUserId();
            var students = Utilities.GetInstance().GetUsersByRole("Student");
            if (!isInactive)
                students.RemoveAll(x => !x.Status);
            var courseClass = db.CourseClass.FirstOrDefault(x => x.Id == id);
            ViewBag.Title = courseClass.Title;
            ViewBag.Instructor = Utilities.GetInstance().GetUserFullName(courseClass.InstructorId);
            List<int> addedIds = courseClass.StudentIds.Split(',').Where(x => !string.IsNullOrEmpty(x) && x.ToLower() != "null").Select(x => int.Parse(x)).ToList();
            ViewBag.AddedStudents = students.Where(x => addedIds.Contains(x.UserId)).ToList();
            ViewBag.AllStudents = students.Where(x => !addedIds.Contains(x.UserId)).ToList();
            ViewBag.ID = id;
            ViewBag.Students = new SelectList(students.Select(x => new { UserId = x.UserId, UserName = ((x.Status == true) ? x.UserName + "-" + x.FullName : "[" + x.UserName + "-" + x.FullName + "]") }), "UserId", "UserName");
            ViewBag.IsInactive = isInactive;
            return View();
        }

        /// <summary>
        /// Save exam questions for provided exam
        /// </summary>
        public JsonResult SaveStudents(int id, string studentIds)
        {

            bool res = false;
            var courseClass = db.CourseClass.FirstOrDefault(x => x.Id == id);
            try
            {
                courseClass.StudentIds = studentIds.ToLower() == "null" ? "" : studentIds;
                db.SaveChanges();
                res = true;
            }
            catch
            {
                res = false;
            }
            if (res)
                return Json(new { status = "0", data = "Student list has been updated" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "1", data = "Please contact system administrator" }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult Resume(int id)
        {
            ViewBag.StudentId = Utilities.GetInstance().GetCurrentUserId();
            var model = db.CourseClass.Where(r => r.Id == id).Include(a => a.Attachment).Include(e => e.Exam).FirstOrDefault();
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult CourseLeftBar(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var course = db.CourseClass.Where(r => r.Id == id).Include(a => a.Attachment).Include(e => e.Exam).FirstOrDefault();
                //ViewBag.AttachmentList = course.Attachments.ToList();
                //ViewBag.ForumList = course.Forums.Where(f => f.IsPublished == true && f.IsDeleted == false).ToList();

                return PartialView("_StudentSidebarPartial", course);
            }
            else
                return View();
        }
        [AllowAnonymous]
        public ActionResult FinishCourse(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = Utilities.GetInstance().GetCurrentUserId();
                CourseClass uc = db.CourseClass.Where(r => r.Id == id).FirstOrDefault();
                if (uc != null)
                {
                    List<string> ids = uc.StudentIds.Split(',').ToList();
                    ids.Remove(userId.ToString());
                    uc.StudentIds = string.Join(",", ids);
                    db.SaveChanges();
                }
                return RedirectToActionPermanent("Dashboard", "Home");
            }
            else
                return RedirectToActionPermanent("Login", "Account");
        }
        [AllowAnonymous]
        public ActionResult List()
        {
            string uid = Utilities.GetInstance().GetCurrentUserId().ToString();
            var courses = db.CourseClass.Where(r => r.Status == true && r.StartDate < DateTime.Now && r.EndDate > DateTime.Now && !r.StudentIds.Contains(uid));
            return PartialView(courses.ToList());
        }
        [AllowAnonymous]
        public ActionResult StartCourse(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = Utilities.GetInstance().GetCurrentUserId();
                var cls = db.CourseClass.FirstOrDefault(x => x.Id == id);
                if (cls != null)
                {
                    List<string> ids = cls.StudentIds.Split(',').ToList();
                    ids.Add(userId.ToString());
                    cls.StudentIds = string.Join(",", ids);
                    db.SaveChanges();
                }
                return RedirectToActionPermanent("Dashboard", "Home");
            }
            else
                return RedirectToActionPermanent("Login", "Account");
        }
        public ActionResult GetStudentNames(int id)
        {
            var crsClass = db.CourseClass.FirstOrDefault(x => x.Id == id);
            List<int> ids = crsClass.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            var students = Utilities.GetInstance().GetUsersByRole("Student");
            students.RemoveAll(x => !ids.Contains(x.UserId));
            return PartialView("GetStudentNames", students);
        }
        public ActionResult SetClassDates(int id, bool isHolidays = false, bool isWeekHolidays = false,string scrollTo = "",bool isPDF = false)
        {
            var cls = db.CourseClass.FirstOrDefault(x => x.Id == id);
            if (cls.ClassDates == null)
                cls.ClassDates = "";
            List<DateTime> existingDates = cls.ClassDates.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => string.IsNullOrEmpty(x) ? DateTime.MinValue : DateTime.Parse(x)).ToList();
            List<ClassDate> classDates = new List<ClassDate>();
            for (DateTime date = cls.StartDate; date <= cls.EndDate; date = date.AddDays(1))
            {
                if (existingDates.Contains(date))
                {
                    classDates.Add(new ClassDate { Date = date, IsClass = true, HolidayType = "" });
                }
                else if (isHolidays)
                {
                    string holidayType = (date.ToString("dddd") == WeeklyHoliday.Friday.ToString() || date.ToString("dddd") == WeeklyHoliday.Saturday.ToString()) ? "Weekly" : "No Class";
                    classDates.Add(new ClassDate { Date = date, IsClass = false, HolidayType = holidayType });
                }
            }
            if (!isWeekHolidays)
            {
                classDates.RemoveAll(x => x.HolidayType == "Weekly");
            }
            ViewBag.ClassName = cls.Title + " Schedule Details";
            ViewBag.ID = id;
            ViewBag.IsHolidays = isHolidays;
            ViewBag.IsWeekHolidays = isWeekHolidays;
            ViewBag.ScrollTo = scrollTo;
            ViewBag.IsPDF = isPDF;
            return View(classDates);
        }
        public ActionResult ClassSchedulePDF(int id, bool isHolidays = false, bool isWeekHolidays = false)
        {
            return new Rotativa.ActionAsPdf("SetClassDates", new { id=id, isHolidays = isHolidays, isWeekHolidays = isWeekHolidays, isPDF = true }) { FileName = "Class Schedule Details.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
        [HttpPost]
        public ActionResult SetClassDates(int id,FormCollection form)
        {
            bool isHolidays = form["isHolidays"] != null && form["isHolidays"].ToString() == "on" ? true : false;
            bool isWeekHolidays = form["isWeekHolidays"] != null && form["isWeekHolidays"].ToString() == "on" ? true : false;
            return RedirectToAction("SetClassDates", new { id = id, isHolidays = isHolidays, isWeekHolidays = isWeekHolidays });
        }
        public ActionResult ScheduleClassDate(int id, DateTime date, bool isHolidays = false, bool isWeekHolidays = false)
        {
            var cls = db.CourseClass.FirstOrDefault(x => x.Id == id);
            if (cls.ClassDates == null)
                cls.ClassDates = "";
            List<DateTime> existingDates = cls.ClassDates.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => string.IsNullOrEmpty(x) ? DateTime.MinValue : DateTime.Parse(x)).ToList();
            if (existingDates.Contains(date))
                existingDates.Remove(date);
            else
                existingDates.Add(date);
            cls.ClassDates = string.Join(",", existingDates.Select(x => x.ToString()).ToArray());
            db.SaveChanges();
            return RedirectToAction("SetClassDates", new { id = id, isHolidays = isHolidays, isWeekHolidays = isWeekHolidays, scrollTo=date.ToShortDateString() });
        }
    }
}
