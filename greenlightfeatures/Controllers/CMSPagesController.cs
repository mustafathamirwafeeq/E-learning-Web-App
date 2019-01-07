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

namespace ELearning.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CMSPagesController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: CMSPages
        /// <summary>
        /// View all CMS pages
        /// </summary>
        public async Task<ActionResult> Index()
        {
            return View(await db.CMSPage.ToListAsync());
        }

        // GET: CMSPages/Details/5
        /// <summary>
        /// View parameterized CMS page
        /// </summary>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMSPage cMSPage = await db.CMSPage.FindAsync(id);
            if (cMSPage == null)
            {
                return HttpNotFound();
            }
            return View(cMSPage);
        }

        // GET: CMSPages/Create
        /// <summary>
        /// Create new CMS page
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        // POST: CMSPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create new CMS page
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Body")] CMSPage cMSPage)
        {
            if (ModelState.IsValid)
            {
                db.CMSPage.Add(cMSPage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cMSPage);
        }

        // GET: CMSPages/Edit/5
        /// <summary>
        /// Edit existing CMS page
        /// </summary>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMSPage cMSPage = await db.CMSPage.FindAsync(id);
            if (cMSPage == null)
            {
                return HttpNotFound();
            }
            return View(cMSPage);
        }

        // POST: CMSPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit existing CMS page
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Body")] CMSPage cMSPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMSPage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cMSPage);
        }

        // GET: CMSPages/Delete/5
        /// <summary>
        /// Delete existing CMS page
        /// </summary>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMSPage cMSPage = await db.CMSPage.FindAsync(id);
            if (cMSPage == null)
            {
                return HttpNotFound();
            }
            return View(cMSPage);
        }

        // POST: CMSPages/Delete/5
        /// <summary>
        /// Delete existing CMS page from database
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CMSPage cMSPage = await db.CMSPage.FindAsync(id);
            db.CMSPage.Remove(cMSPage);
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
