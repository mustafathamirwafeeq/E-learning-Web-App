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
using ELearning.Repo;

namespace ELearning.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class AttachmentsController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: Attachments
        /// <summary>
        /// View active attachments
        /// </summary>
        public ActionResult Index()
        {
            return View(Utilities.GetInstance().GetAttachments());
        }

        // GET: Attachments/Details/5
        /// <summary>
        /// View parameterized attachment
        /// </summary>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = await db.Attachment.FindAsync(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return View(attachment);
        }

        // GET: Attachments/Create
        /// <summary>
        /// Create course specific attachment
        /// </summary>
        public ActionResult Create(int? courseId)
        {
            if (User.IsInRole("Admin"))
            {
                List<SelectListItem> courseList;
                if (courseId.HasValue)
                    courseList = new SelectList(db.CourseClass.ToList(), "Id", "Title", courseId).ToList();
                else
                    courseList = new SelectList(db.CourseClass.ToList(), "Id", "Title").ToList();

                courseList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
                ViewBag.CourseClassId = new SelectList(courseList,"Value","Text");

                return View();
            }
            else
            {
                SelectList courseList;
                int instID = Utilities.GetInstance().GetCurrentUserId();
                if (courseId.HasValue)
                    courseList = new SelectList(db.CourseClass.Where(x => x.InstructorId == instID && x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", courseId);
                else
                    courseList = new SelectList(db.CourseClass.Where(x => x.InstructorId == instID && x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title");

                ViewBag.CourseList = courseList;

                return View();
            }

        }

        // POST: Attachments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create course specific attachment
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,FilePath,PubDate,Order,CourseClassId")] Attachment attachment)
        {
            if (ModelState.IsValid)
            {
                if (attachment.CourseClassId > 0)
                {
                    db.Attachment.Add(attachment);
                    await db.SaveChangesAsync();
                }
                else
                {
                    foreach(var cls in db.CourseClass.ToList())
                    {
                        attachment.CourseClassId = cls.Id;
                        db.Attachment.Add(attachment);
                        await db.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Index");
            }

            if (User.IsInRole("Admin"))
            {
                List<SelectListItem> courseList;
                courseList = new SelectList(db.CourseClass.ToList(), "Id", "Title", attachment.CourseClassId).ToList();
                courseList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
                ViewBag.CourseClassId = new SelectList(courseList,"Value","Text");
            }
            else
            {
                SelectList courseList;
                int instID = Utilities.GetInstance().GetCurrentUserId();
                courseList = new SelectList(db.CourseClass.Where(x => x.InstructorId == instID && x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", attachment.CourseClassId);

                ViewBag.CourseClassId = new SelectList(courseList,"Value","Text");
            }

            return View(attachment);
        }

        // GET: Attachments/Edit/5
        /// <summary>
        /// Edit attachment
        /// </summary> 
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = await db.Attachment.FindAsync(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            int instID = Utilities.GetInstance().GetCurrentUserId();
            ViewBag.CourseList = new SelectList(db.CourseClass.Where(x => x.InstructorId == instID && x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", attachment.CourseClassId);
            return View(attachment);
        }

        // POST: Attachments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,FilePath,PubDate,Order,CourseClassId")] Attachment attachment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attachment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            int instID = Utilities.GetInstance().GetCurrentUserId();
            ViewBag.CourseList = new SelectList(db.CourseClass.Where(x => x.InstructorId == instID && x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", attachment.CourseClassId);
            return View(attachment);
        }

        // GET: Attachments/Delete/5
        /// <summary>
        /// Delete parameterized attachment
        /// </summary> 
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = await db.Attachment.FindAsync(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return View(attachment);
        }

        // POST: Attachments/Delete/5
        /// <summary>
        /// Delete attachment from database
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Attachment attachment = await db.Attachment.FindAsync(id);
            db.Attachment.Remove(attachment);
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

    }
}
