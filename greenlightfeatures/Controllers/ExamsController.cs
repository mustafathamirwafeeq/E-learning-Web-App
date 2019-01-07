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
using System.Configuration;

namespace ELearning.Controllers
{
    [Authorize(Roles = "Admin,Moderator,Management")]
    public class ExamsController : Controller
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: Exams
        /// <summary>
        /// View role based exams
        /// </summary>
        public ActionResult Index()
        {
            return View(Utilities.GetInstance().GetExams());
        }

        // GET: Exams/Details/5
        /// <summary>
        /// View parameterized exam
        /// </summary>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exam.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Exams/Create
        /// <summary>
        /// Create new exam
        /// </summary>
        public ActionResult Create()
        {
            int instID = Utilities.GetInstance().GetCurrentUserId();
            ViewBag.CourseId = new SelectList(db.CourseClass.Where(x => x.InstructorId == instID && x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title");
            ViewBag.ExamTypeId = new SelectList(ExamRepo.GetInstance().GetActiveExamTypes(), "Id", "Type");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Add new exam in to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,CreationDate,IsPublished,CourseClassId,ExamTypeId,MaxGrade,UserId,Duration")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Exam.Add(exam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            int instID = Utilities.GetInstance().GetCurrentUserId();
            ViewBag.CourseId = new SelectList(db.CourseClass.Where(x => x.InstructorId == instID && x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", exam.CourseClassId);
            ViewBag.ExamTypeId = new SelectList(ExamRepo.GetInstance().GetActiveExamTypes(), "Id", "Type", exam.ExamTypeId);
            return View(exam);
        }

        // GET: Exams/Edit/5
        /// <summary>
        /// Edit existing exam
        /// </summary>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exam.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            int instID = Utilities.GetInstance().GetCurrentUserId();
            ViewBag.CourseId = new SelectList(db.CourseClass.Where(x => x.InstructorId == instID && x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", exam.CourseClassId);
            ViewBag.ExamTypeId = new SelectList(ExamRepo.GetInstance().GetActiveExamTypes(), "Id", "Type", exam.ExamTypeId);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Update existing exam in to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,CreationDate,IsPublished,CourseClassId,ExamTypeId,MaxGrade,UserId,Duration")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            int instID = Utilities.GetInstance().GetCurrentUserId();
            ViewBag.CourseId = new SelectList(db.CourseClass.Where(x => x.InstructorId == instID && x.Status && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now), "Id", "Title", exam.CourseClassId);
            ViewBag.ExamTypeId = new SelectList(ExamRepo.GetInstance().GetActiveExamTypes(), "Id", "Type", exam.ExamTypeId);
            return View(exam);
        }

        // GET: Exams/Delete/5
        /// <summary>
        /// Delete existing exam
        /// </summary>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exam.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Exams/Delete/5
        /// <summary>
        /// Update existing exam record as "soft delete"
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Exam exam = await db.Exam.FindAsync(id);
            exam.IsDeleted = true;
            //db.Exams.Remove(exam);
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
        /// View selected & non-selected question for provided course & exam
        /// </summary>
        public ActionResult ManageQuestions(int examId, int courseId)
        {
            int userId = Utilities.GetInstance().GetCurrentUserId();
            //ExamQuestion examQuestion = db.ExamQuestions.Where(e => e.ExamId == examId & e.Exam.UserId == userId).FirstOrDefault();
            int[] _examQuestions = Utilities.GetInstance().GetExamQuestion(examId);
            ViewBag.ExamQuestions = _examQuestions; // new SelectList(_examQuestions, "Id", "QuestionText");

            ViewBag.ExamId = examId;
            ViewBag.ExamTitle = db.Exam.Where(p => p.Id == examId).FirstOrDefault().Title;

            var cls = db.CourseClass.FirstOrDefault(p => p.Id == courseId);
            ViewBag.CourseTitle = cls.Title;

            ViewBag.Questions = new SelectList(QuestionRepo.GetInstance().GetQuestionByCourse(cls.CourseId), "Id", "QuestionText");
            return View();
        }

        /// <summary>
        /// Save exam questions for provided exam
        /// </summary>
        public JsonResult SaveExamQuestions(int examId, string questionIds)
        {

            bool res = false;
            ExamQuestion examQuestion;
            string[] _questions = questionIds.Split(new char[] { ',' });
            int cnt = 1;

            //remove old data
            var oldQuestions = db.ExamQuestion.Where(r => r.ExamId == examId);
            db.ExamQuestion.RemoveRange(oldQuestions);

            if (!questionIds.Equals("null"))
            {
                //add new data
                foreach (string q in _questions)
                {
                    examQuestion = new ExamQuestion();
                    examQuestion.ExamId = examId;
                    examQuestion.QuestionId = Convert.ToInt32(q);
                    examQuestion.OrderId = cnt++;

                    db.ExamQuestion.Add(examQuestion);
                }
            }

            try
            {
                db.SaveChanges();
                res = true;
            }
            catch
            {
                res = false;
            }
            if (res)
                return Json(new { status = "0", data = "Questions has been updated" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "1", data = "Please contact system administrator" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// View all exams
        /// </summary>
        [AllowAnonymous]
        public async Task<ActionResult> List()
        {
            var exams = db.Exam.Where(r => r.IsPublished == true).Include(e => e.CourseClass).Include(e => e.ExamType);
            return View(await exams.ToListAsync());
        }

        /// <summary>
        /// View related details while taking an exam
        /// </summary>
        [AllowAnonymous]
        public async Task<ActionResult> TakeExam(int id)
        {
            return View(await db.Exam.Where(r => r.Id == id).Include(e => e.CourseClass).Include(e => e.ExamType).FirstOrDefaultAsync());
        }

        /// <summary>
        /// View role based exam evalutions
        /// </summary>
        [AllowAnonymous]
        public ActionResult Evalution(int id)
        {
            ExamDto exam = new ExamDto();
            EvalutionDto evalution;
            List<Evaluation> evalutions = new List<Evaluation>();

            var _exam = db.Exam.Where(r => r.Id == id).Include(e => e.CourseClass).Include(e => e.ExamType).FirstOrDefault();

            exam.CoursTitle = _exam.CourseClass.Title;
            exam.ExamTitle = _exam.Title;
            exam.ExamType = _exam.ExamType.Type;

            if (User.IsInRole("Student"))
            {
                evalutions = EvalutionRepo.GetInstance().GetEvalutionByStudent(id, Utilities.GetInstance().GetCurrentUserId());
            }
            else
            {
                evalutions = EvalutionRepo.GetInstance().GetEvalutionByExam(id);
            }

            exam.Evalutions = new List<EvalutionDto>();
            foreach (var e in evalutions)
            {
                evalution = new EvalutionDto();
                evalution.Grade = e.Grade;
                evalution.Username = Utilities.GetInstance().GetUserName(e.StudentId);
                evalution.FullName = Utilities.GetInstance().GetUserFullName(e.StudentId);
                evalution.ExamDate = e.ExamDate;

                exam.Evalutions.Add(evalution);
            }
            return View(exam);
        }

        /// <summary>
        /// View latest 10 users evalution 
        /// </summary>
        [AllowAnonymous]
        public async Task<ActionResult> ReceantEvalution()
        {
            IQueryable<Evaluation> evalutions;
            if (User.IsInRole("Student"))
            {
                int userId = Utilities.GetInstance().GetCurrentUserId();
                evalutions = db.Evaluation.Where(r => r.StudentId == userId).OrderByDescending(r => r.Id).Take(10);
                return PartialView("_RecentEvalutions", await evalutions.ToListAsync());
            }
            else
            {
                evalutions = db.Evaluation.OrderByDescending(r => r.Id).Take(10);
                return PartialView("_RecentAllEvalutions", await evalutions.ToListAsync());
            }

        }

        /// <summary>
        /// Check the count of evalutions for provided student & exam
        /// </summary>
        [AllowAnonymous]
        public JsonResult ExceedEvaluations(int studentId, int examId)
        {
            string limit = ConfigurationManager.AppSettings["ExamTakenLimit"];
            var cnt = db.Evaluation.Count(e => e.StudentId == studentId && e.ExamId == examId) >= Convert.ToInt32(limit);
            return Json(new { status = "1", data = cnt }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Evaluation(int id)
        {
            var exam = db.Exam.FirstOrDefault(x => x.Id == id);
            var students = Utilities.GetInstance().GetStudentsFromIDs(exam.CourseClass.StudentIds);
            var evaluationTypes = db.EvaluationTypeSet.OrderBy(x => x.Id).ToList();
            EvalutionDto model = new EvalutionDto();
            model.Exam = exam;
            model.Students = students;
            model.EvalTypes = evaluationTypes;
            return View(model);
        }
        public ActionResult DoStudentEvaluation(int sid, int eid, bool isPDF = false)
        {
            var exam = db.Exam.FirstOrDefault(x => x.Id == eid);
            var student = db.AspNetUsers.FirstOrDefault(x => x.UserId == sid);
            var evaluationTypes = db.EvaluationTypeSet.OrderBy(x => x.Id).ToList();
            EvalutionDto model = new EvalutionDto();
            model.Exam = exam;
            model.Students = new List<AspNetUsers> { student };
            model.EvalTypes = evaluationTypes;
            var attendanceDetails = Utilities.GetInstance().GetClassStudentsDetails(exam.CourseClassId).FirstOrDefault(x => x.Student.UserId == student.UserId);
            ViewBag.SID = sid;
            ViewBag.EID = eid;
            ViewBag.IsPDF = isPDF;
            ViewBag.Attendance = (double.Parse(attendanceDetails.Presents.ToString()) / (attendanceDetails.Presents + attendanceDetails.Absents) * 100).ToString("0.00");
            return View(model);
        }
        public ActionResult DoStudentEvaluationPDF(int sid, int eid = 0)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            return new Rotativa.ActionAsPdf("DoStudentEvaluation", new { eid = eid, sid = sid, isPDF = true }) { FileName = "Student Exam Evaluation Details.pdf", PageMargins = Utilities.GetInstance().PDFMargins,Cookies = cookieCollection };
        }
        public JsonResult SaveEvaluation(string q)
        {
            string[] values = q.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            Utilities.GetInstance().SaveEvaluations(values);
            return Json(new { data = "Evaluations has been updated" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveStudentEvaluation(string q, int eid, int sid, int rating, string comments)
        {
            string[] values = q.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            bool isPending = false;
            foreach (string qq in values)
            {
                string[] values1 = qq.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (values1[1] == "0" && values1[2] == "0" && values1[3] == "0" && values1[4] == "0" && values1[5] == "0" && values1[6] == "0")
                    isPending = true;
            }
            if (!isPending)
            {
                Utilities.GetInstance().SaveStudentEvaluations(eid, sid, values, rating, comments);
                return Json(new { done = 1, data = "Evaluations has been updated" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { done = 0, data = "Please complete all categories to save evaluation" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
