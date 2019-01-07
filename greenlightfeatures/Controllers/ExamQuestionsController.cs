using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELearning.Models;

namespace ELearning.Controllers
{
    [Authorize]
    public class ExamQuestionsController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: ExamQuestions
        /// <summary>
        /// View list of exams questions
        /// </summary>
        public ActionResult Index()
        {
            var examQuestions = db.ExamQuestion.Include(e => e.Question).Include(e => e.Exam);
            return View(examQuestions.ToList());
        }

        // GET: ExamQuestions/Details/5
        /// <summary>
        /// View parameterized exam question
        /// </summary>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamQuestion examQuestion = db.ExamQuestion.Find(id);
            if (examQuestion == null)
            {
                return HttpNotFound();
            }
            return View(examQuestion);
        }

        // GET: ExamQuestions/Create
        /// <summary>
        /// Create new exam question
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(db.Question, "Id", "QuestionText");
            ViewBag.ExamId = new SelectList(db.Exam, "Id", "Title");
            return View();
        }

        // POST: ExamQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Add new exam question in to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamId,QuestionId,OrderId")] ExamQuestion examQuestion)
        {
            if (ModelState.IsValid)
            {
                db.ExamQuestion.Add(examQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.Question, "Id", "QuestionText", examQuestion.QuestionId);
            ViewBag.ExamId = new SelectList(db.Exam, "Id", "Title", examQuestion.ExamId);
            return View(examQuestion);
        }

        // GET: ExamQuestions/Edit/5
        /// <summary>
        /// Edit existing exam question
        /// </summary>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamQuestion examQuestion = db.ExamQuestion.Find(id);
            if (examQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Question, "Id", "QuestionText", examQuestion.QuestionId);
            ViewBag.ExamId = new SelectList(db.Exam, "Id", "Title", examQuestion.ExamId);
            return View(examQuestion);
        }

        // POST: ExamQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Update existing exam question in to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamId,QuestionId,OrderId")] ExamQuestion examQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.Question, "Id", "QuestionText", examQuestion.QuestionId);
            ViewBag.ExamId = new SelectList(db.Exam, "Id", "Title", examQuestion.ExamId);
            return View(examQuestion);
        }

        // GET: ExamQuestions/Delete/5
        /// <summary>
        /// Delete parameterized exam question
        /// </summary>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamQuestion examQuestion = db.ExamQuestion.Find(id);
            if (examQuestion == null)
            {
                return HttpNotFound();
            }
            return View(examQuestion);
        }

        // POST: ExamQuestions/Delete/5
        /// <summary>
        /// Remove parameterized exam question from database
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamQuestion examQuestion = db.ExamQuestion.Find(id);            
            db.ExamQuestion.Remove(examQuestion);
            db.SaveChanges();
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
