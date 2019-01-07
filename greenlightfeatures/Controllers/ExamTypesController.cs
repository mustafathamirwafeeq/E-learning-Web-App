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
    public class ExamTypesController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: ExamTypes
        /// <summary>
        /// View active exam types
        /// </summary>
        public ActionResult Index()
        {
            return View(ExamRepo.GetInstance().GetActiveExamTypes());
        }

        // GET: ExamTypes/Details/5
        /// <summary>
        /// View parameterized exam type
        /// </summary>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamType examType = await db.ExamType.FindAsync(id);
            if (examType == null)
            {
                return HttpNotFound();
            }
            return View(examType);
        }

        // GET: ExamTypes/Create
        /// <summary>
        /// Create new exam type
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExamTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Add new exam type in to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Type")] ExamType examType)
        {
            if (ModelState.IsValid)
            {
                db.ExamType.Add(examType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(examType);
        }

        // GET: ExamTypes/Edit/5
        /// <summary>
        /// Edit existing exam type
        /// </summary>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamType examType = await db.ExamType.FindAsync(id);
            if (examType == null)
            {
                return HttpNotFound();
            }
            return View(examType);
        }

        // POST: ExamTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Update existing exam type in to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Type")] ExamType examType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(examType);
        }

        // GET: ExamTypes/Delete/5
        /// <summary>
        /// Delete existing exam type
        /// </summary>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamType examType = await db.ExamType.FindAsync(id);
            if (examType == null)
            {
                return HttpNotFound();
            }
            return View(examType);
        }

        // POST: ExamTypes/Delete/5
        /// <summary>
        /// Mark existing exam type status as "soft delete"
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ExamType examType = await db.ExamType.FindAsync(id);
            examType.IsDeleted = true;
            //db.ExamTypes.Remove(examType);
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
