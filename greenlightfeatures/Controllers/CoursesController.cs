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
using ELearning.Repo;

namespace ELearning.Controllers
{
    public class CoursesController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: Courses
        public async Task<ActionResult> Index()
        {
            return View(await db.Course.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Course.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,IsPublished,IsDeleted")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Course.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Course.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,IsPublished,IsDeleted")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Course.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Course course = await db.Course.FindAsync(id);
            db.Course.Remove(course);
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

        [AllowAnonymous]
        public ActionResult UserCourses()
        {
            if (User.Identity.IsAuthenticated)
            {
                UserCoursesDto dto;
                List<UserCoursesDto> uc = new List<UserCoursesDto>();
                int userId = Utilities.GetInstance().GetCurrentUserId();

                var _uc = Utilities.GetInstance().GetStudentClasses(userId.ToString());
                foreach (var v in _uc)
                {
                    dto = new UserCoursesDto();
                    dto.UserId = userId;
                    dto.FullName = Utilities.GetInstance().GetUserFullName(v.InstructorId);
                    dto.Course = v.Title;
                    dto.CourseId = v.Id;
                    dto.Status = v.Status;
                    dto.AddedDate = v.AddedDate;
                    dto.TotalExams = v.Exam.Count();
                    dto.ExamTaken = ExamRepo.GetInstance().UserExamsCount(userId, v.CourseId);
                    dto.StudentIds = v.StudentIds;
                    dto.EndDate = v.EndDate;
                    uc.Add(dto);
                }

                return PartialView("_UserCourses", uc);
            }
            else
                return View();
        }
    }
}
