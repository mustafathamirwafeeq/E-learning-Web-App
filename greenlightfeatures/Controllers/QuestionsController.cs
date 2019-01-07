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
    public class QuestionsController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: Questions1
        /// <summary>
        /// View role based questions
        /// </summary>
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("Admin"))
                return View(await db.Question.Where(r => r.IsDeleted == false).ToListAsync());
            else
                return View(QuestionRepo.GetInstance().GetQuestions(Utilities.GetInstance().GetCurrentUserId()));
        }

        // GET: Questions1/Details/5
        /// <summary>
        /// View question
        /// </summary>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Question.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions1/Create
        /// <summary>
        /// Add question
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Course.Where(x=> x.IsPublished == true && x.IsDeleted == false), "Id", "Title");
            return View();
        }

        // POST: Questions1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Add question in to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,QuestionText,Option1,Option2,Option3,Option4,CorrectAnswer,AnswerData,UserId,CourseId")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.UserId = Utilities.GetInstance().GetCurrentUserId();
                db.Question.Add(question);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Course.Where(x => x.IsPublished == true && x.IsDeleted == false), "Id", "Title", question.CourseId);
            return View(question);
        }

        // GET: Questions1/Edit/5
        /// <summary>
        /// Edit question
        /// </summary>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Question.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseList = new SelectList(db.Course.Where(x => x.IsPublished == true && x.IsDeleted == false), "Id", "Title", question.CourseId);
            return View(question);
        }

        // POST: Questions1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Update question in to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,QuestionText,Option1,Option2,Option3,Option4,CorrectAnswer,AnswerData,UserId,CourseId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseList = new SelectList(db.Course.Where(x => x.IsPublished == true && x.IsDeleted == false), "Id", "Title", question.CourseId);
            return View(question);
        }

        // GET: Questions1/Delete/5
        /// <summary>
        /// Delete question
        /// </summary>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Question.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions1/Delete/5
        /// <summary>
        /// Mark existing questions' status as "soft delete"
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Question question = await db.Question.FindAsync(id);
            question.IsDeleted = true;
            //db.Questions.Remove(question);
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
        /// View all non-deleted questions
        /// </summary>
        [AllowAnonymous]
        public async Task<ActionResult> List()
        {
            var questions = db.Question.Where(r => r.IsDeleted == false);
            return View(await questions.ToListAsync());
        }

        /// <summary>
        /// Add questions to provided course
        /// </summary>
        public ActionResult AddQuestions(int courseId)
        {

            var course = db.Course.Where(r => r.Id == courseId).FirstOrDefault();
            ViewBag.CourseTitle = course.Title;
            ViewBag.UserId = Utilities.GetInstance().GetCurrentUserId();
            ViewBag.CourseId = course.Id;

            return View();
        }

        /// <summary>
        /// Save questions to provided course
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddQuestions(List<Question> QuestionList)
        {
            try
            {
                bool isSucc = true;
                string msg = "There are some errors:" + Environment.NewLine;
                foreach (var v in QuestionList)
                {
                    if (string.IsNullOrEmpty(v.QuestionText) || (v.CorrectAnswer == 0) || string.IsNullOrEmpty(v.AnswerData))
                    {
                        msg += "Some required fields are empty";
                        isSucc = false;
                        break;
                    }
                    else if (db.Question.FirstOrDefault(x => x.QuestionText.ToLower().Trim() == v.QuestionText.ToLower().Trim()) != null)
                    {
                        msg += "Question Text '" + v.QuestionText + "' already exists";
                        isSucc = false;
                        break;
                    }
                    else
                    {
                        db.Question.Add(v);
                    }
                }
                if (isSucc)
                {
                    db.SaveChanges();
                    return Json(new { Status = true, Message = "Questions Save Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError("", "Some required fields are empty");
                    return Json(new { Status = false, Message = msg }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json(new { Status = false, Message = "Please Contact System Administrator" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
