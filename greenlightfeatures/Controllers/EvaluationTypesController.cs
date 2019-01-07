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
    public class EvaluationTypesController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: EvaluationTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.EvaluationTypeSet.ToListAsync());
        }

        // GET: EvaluationTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationType evaluationType = await db.EvaluationTypeSet.FindAsync(id);
            if (evaluationType == null)
            {
                return HttpNotFound();
            }
            return View(evaluationType);
        }

        // GET: EvaluationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EvaluationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,A1,A2,B1,B2,C1,C2")] EvaluationType evaluationType)
        {
            if (ModelState.IsValid)
            {
                GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
                var evalType = db.EvaluationTypeSet.FirstOrDefault(x => x.Name.ToLower().Trim() == evaluationType.Name.ToLower().Trim());
                if (evalType != null)
                {
                    ModelState.AddModelError("Name", "Name already exists");
                }
                else
                {
                    evaluationType.A1 = evaluationType.A1 ?? "";
                    evaluationType.A2 = evaluationType.A2 ?? "";
                    evaluationType.B1 = evaluationType.B1 ?? "";
                    evaluationType.B2 = evaluationType.B2 ?? "";
                    evaluationType.C1 = evaluationType.C1 ?? "";
                    evaluationType.C2 = evaluationType.C2 ?? "";
                    db.EvaluationTypeSet.Add(evaluationType);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(evaluationType);
        }

        // GET: EvaluationTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationType evaluationType = await db.EvaluationTypeSet.FindAsync(id);
            if (evaluationType == null)
            {
                return HttpNotFound();
            }
            return View(evaluationType);
        }

        // POST: EvaluationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,A1,A2,B1,B2,C1,C2")] EvaluationType evaluationType)
        {
            if (ModelState.IsValid)
            {
                GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
                var evalType = db.EvaluationTypeSet.FirstOrDefault(x => x.Id != evaluationType.Id && x.Name.ToLower().Trim() == evaluationType.Name.ToLower().Trim());
                if (evalType != null)
                {
                    ModelState.AddModelError("Name", "Name already exists");
                }
                else
                {
                    evaluationType.A1 = evaluationType.A1 ?? "";
                    evaluationType.A2 = evaluationType.A2 ?? "";
                    evaluationType.B1 = evaluationType.B1 ?? "";
                    evaluationType.B2 = evaluationType.B2 ?? "";
                    evaluationType.C1 = evaluationType.C1 ?? "";
                    evaluationType.C2 = evaluationType.C2 ?? "";
                    db.Entry(evaluationType).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(evaluationType);
        }

        // GET: EvaluationTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationType evaluationType = await db.EvaluationTypeSet.FindAsync(id);
            if (evaluationType == null)
            {
                return HttpNotFound();
            }
            return View(evaluationType);
        }

        // POST: EvaluationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EvaluationType evaluationType = await db.EvaluationTypeSet.FindAsync(id);
            db.EvaluationTypeSet.Remove(evaluationType);
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
