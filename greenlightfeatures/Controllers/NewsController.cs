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
using ELearning.Models.Enum;

namespace ELearning.Controllers
{
    public class NewsController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: News
        /// <summary>
        /// View all news
        /// </summary>
        [Authorize(Roles = "Admin,Moderator,Student,Management")]
        public async Task<ActionResult> Index(bool isPDF = false)
        {
            ViewBag.IsPDF = isPDF;
            if (User.IsInRole("Moderator"))
            {
                int instID = (int)NewsAudience.Instructors;
                int allID = (int)NewsAudience.All;
                List<int> classes = Utilities.GetInstance().GetInstructorClasses(Utilities.GetInstance().GetCurrentUserId()).Select(x => x.Id).ToList();
                return View(await db.News.Where(x => x.PubDate < DateTime.Now && (x.Audience == instID || x.Audience == allID || classes.Contains(x.ClassId.Value))).ToListAsync());
            }
            else if (User.IsInRole("Student"))
            {
                int studentID = (int)NewsAudience.Students;
                int allID = (int)NewsAudience.All;
                List<int> classes = Utilities.GetInstance().GetStudentClasses(Utilities.GetInstance().GetCurrentUserId().ToString()).Select(x => x.Id).ToList();
                return View(await db.News.Where(x => x.PubDate < DateTime.Now && (x.Audience == studentID || x.Audience == allID || classes.Contains(x.ClassId.Value))).ToListAsync());
            }
            else
                return View(await db.News.Where(x => x.Audience > 0).ToListAsync());
        }
        [Authorize(Roles = "Moderator")]
        public ActionResult ListOfNews()
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            return new Rotativa.ActionAsPdf("Index", new { isPDF = true }) { FileName = "List Of Announcements.pdf", PageMargins = Utilities.GetInstance().PDFMargins, Cookies = cookieCollection};
        }
        // GET: News/Details/5
        /// <summary>
        /// View news
        /// </summary>
        [Authorize(Roles = "Admin,Moderator,Student,Management")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        /// <summary>
        /// Add news
        /// </summary>
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Create()
        {
            List<SelectListItem> audience = new List<SelectListItem>();
            if (User.IsInRole("Admin"))
            {
                audience.Add(new SelectListItem { Text = "All", Value = "1" });
                audience.Add(new SelectListItem { Text = "Instructors", Value = "2" });
                audience.Add(new SelectListItem { Text = "Students", Value = "3" });
            }
            audience.Add(new SelectListItem { Text = "Class", Value = "4" });
            ViewBag.Audience = audience;
            ViewBag.ClassId = new SelectList(db.CourseClass.Where(x => x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title");
            ViewBag.IsClassId = false;
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Add news in to database
        /// </summary>
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,PubDate,Details,ClassId,Audience")] News news)
        {
            if (ModelState.IsValid)
            {
                if (news.Audience == 4 && news.ClassId == null)
                {
                    ModelState.AddModelError("", "Please select target Class");
                }
                else
                {
                    news.CreatedBy = Utilities.GetInstance().GetCurrentUserId();
                    db.News.Add(news);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            List<SelectListItem> audience = new List<SelectListItem>();
            if (User.IsInRole("Admin"))
            {
                audience.Add(new SelectListItem { Text = "All", Value = "1" });
                audience.Add(new SelectListItem { Text = "Instructors", Value = "2" });
                audience.Add(new SelectListItem { Text = "Students", Value = "3" });
            }
            audience.Add(new SelectListItem { Text = "Class", Value = "4" });
            if (news != null && news.Audience != 0)
                audience.First(x => x.Value == news.Audience.ToString()).Selected = true;
            ViewBag.Audience = audience;
            ViewBag.ClassId = new SelectList(db.CourseClass.Where(x => x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", news.ClassId);
            ViewBag.IsClassId = news.Audience == 4 ? true : false;
            return View(news);
        }

        // GET: News/Edit/5
        /// <summary>
        /// Edit existing news
        /// </summary>
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> audience = new List<SelectListItem>();
            if (User.IsInRole("Admin"))
            {
                audience.Add(new SelectListItem { Text = "All", Value = "1" });
                audience.Add(new SelectListItem { Text = "Instructors", Value = "2" });
                audience.Add(new SelectListItem { Text = "Students", Value = "3" });
            }
            audience.Add(new SelectListItem { Text = "Class", Value = "4" });
            audience.First(x => x.Value == news.Audience.ToString()).Selected = true;
            ViewBag.Audience = audience;
            ViewBag.ClassId = new SelectList(db.CourseClass.Where(x => x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", news.ClassId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Update existing news in to database
        /// </summary>
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,PubDate,Details,ClassId,Audience")] News news)
        {
            if (ModelState.IsValid)
            {
                if (news.Audience == 4 && news.ClassId == null)
                {
                    ModelState.AddModelError("ClassId", "Please select target Class");
                }
                else
                {
                    db.Entry(news).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            List<SelectListItem> audience = new List<SelectListItem>();
            if (User.IsInRole("Admin"))
            {
                audience.Add(new SelectListItem { Text = "All", Value = "1" });
                audience.Add(new SelectListItem { Text = "Instructors", Value = "2" });
                audience.Add(new SelectListItem { Text = "Students", Value = "3" });
            }
            audience.Add(new SelectListItem { Text = "Class", Value = "4" });
            audience.First(x => x.Value == news.Audience.ToString()).Selected = true;
            ViewBag.Audience = audience;
            ViewBag.ClassId = new SelectList(db.CourseClass.Where(x => x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", news.ClassId);
            return View(news);
        }

        // GET: News/Delete/5
        /// <summary>
        /// Delete news
        /// </summary>
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        /// <summary>
        /// Remove news from database 
        /// </summary>
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            News news = await db.News.FindAsync(id);
            db.News.Remove(news);
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
        /// View all news for visitors
        /// </summary>
        [AllowAnonymous]
        public async Task<ActionResult> List()
        {
            if (User.IsInRole("Moderator"))
            {
                int instID = (int)NewsAudience.Instructors;
                int allID = (int)NewsAudience.All;
                List<int> classes = Utilities.GetInstance().GetInstructorClasses(Utilities.GetInstance().GetCurrentUserId()).Select(x => x.Id).ToList();
                var news = db.News.Where(x => x.PubDate < DateTime.Now && (x.Audience == instID || x.Audience == allID || classes.Contains(x.ClassId.Value))).OrderByDescending(r => r.PubDate).Take(5);
                if (Request.IsAjaxRequest())
                    return PartialView("_List", await news.ToListAsync());
                else
                    return View(await news.ToListAsync());
            }
            else if (User.IsInRole("Student"))
            {
                int studentID = (int)NewsAudience.Students;
                int allID = (int)NewsAudience.All;
                List<int> classes = Utilities.GetInstance().GetStudentClasses(Utilities.GetInstance().GetCurrentUserId().ToString()).Select(x => x.Id).ToList();
                var news = db.News.Where(x => x.PubDate < DateTime.Now && (x.Audience == studentID || x.Audience == allID || classes.Contains(x.ClassId.Value))).OrderByDescending(r => r.PubDate).Take(5);
                if (Request.IsAjaxRequest())
                    return PartialView("_List", await news.ToListAsync());
                else
                    return View(await news.ToListAsync());
            }
            else
            {
                var news = db.News.Where(x => x.Audience > 0).OrderByDescending(r => r.PubDate).Take(5);
                if (Request.IsAjaxRequest())
                    return PartialView("_List", await news.Where(x => x.Audience > 0).ToListAsync());
                else
                    return View(await news.Where(x => x.Audience > 0).ToListAsync());
            }
        }

    }
}
