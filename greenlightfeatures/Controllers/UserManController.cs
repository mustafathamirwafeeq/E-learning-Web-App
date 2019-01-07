using ELearning.Models;
using ELearning.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearning.Controllers
{
    public class UserManController : Controller
    {
        // GET: UserMan
        public ActionResult BaseHistory(bool isPDF = false)
        {
            ViewBag.IsPDF = isPDF;
            var model = Utilities.GetInstance().GetBasesWithDetails();
            return View(model);
        }
        public ActionResult BaseHistoryPDF()
        {
            return new Rotativa.ActionAsPdf("BaseHistory", new { isPDF = true }) { FileName = "Bases Statistics.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
        public ActionResult GeneralClassesHistory(bool isPDF = false)
        {
            ViewBag.IsPDF = isPDF;
            var model = Utilities.GetInstance().GetGeneralClassesHistory();
            return View(model);
        }
        public ActionResult GeneralClassesHistoryPDF()
        {
            return new Rotativa.ActionAsPdf("GeneralClassesHistory", new { isPDF = true }) { FileName = "Classes Statistics by Year/Month.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
        public ActionResult BarChart()
        {
            return View();
        }
        public JsonResult GetBarChartData()
        {
            return Json(ChartData.GetBarData(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult LineChart()
        {
            return View();
        }
        public JsonResult GetLineChartData()
        {
            return Json(ChartData.GetLineData(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BaseFullView(int id = 0, bool isPDF = false)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            SelectList baseList = new SelectList(db.TrainingBase.ToList(), "Id", "Name", id);
            ViewBag.Bases = baseList;
            ViewBag.ID = id;
            ViewBag.IsPDF = isPDF;
            var baseDetail = Utilities.GetInstance().GetBasesWithDetails(id).FirstOrDefault();
            return View(baseDetail);
        }
        public ActionResult BaseFullViewPDF(int id)
        {
            return new Rotativa.ActionAsPdf("BaseFullView", new { id = id, isPDF = true }) { FileName = "Base Full View.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
        public ActionResult BarChartForBase(int id, Models.Dto.BaseDetail model)
        {
            ViewBag.ID = id;
            return View(model);
        }
        public JsonResult GetBarChartDataForBase(int id)
        {
            return Json(ChartData.GetBarDataForBase(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ClassFullView(int id = 0, bool isPDF = false)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            SelectList classList = new SelectList(db.CourseClass.ToList(), "Id", "Title", id);
            ViewBag.Classes = classList;
            ViewBag.ID = id;
            ViewBag.IsPDF = isPDF;
            CourseClass cls = null;
            if (id != 0)
            {
                cls = Utilities.GetInstance().GetClassByID(id);
                ViewBag.Instructor = db.AspNetUsers.FirstOrDefault(x => x.UserId == cls.InstructorId);
                ViewBag.StudentDetails = Utilities.GetInstance().GetClassStudentsDetails(cls.Id);
            }
            return View(cls);
        }
        public ActionResult ClassFullViewPDF(int id)
        {
            return new Rotativa.ActionAsPdf("ClassFullView", new { id = id, isPDF = true }) { FileName = "Class Full View.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
        public ActionResult BarChartForClassEvaluation(int clsId)
        {
            ViewBag.ClassID = clsId;
            return View();
        }
        public JsonResult GetBarChartDataForClassEvaluation(int clsId)
        {
            var model = Utilities.GetInstance().GetClassStudentsDetails(clsId);

            var data = new List<ChartData>();
            foreach (var studentDetail in model)
            {
                data.Add(new ChartData(studentDetail.Student.FullName, double.Parse(studentDetail.OverAllEvaluationMarks.ToString())));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InstructorFullView(int id = 0, bool isPDF = false)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            SelectList instructorList = new SelectList(Utilities.GetInstance().GetUsersByRole("Moderator"), "UserId", "FullName", id);
            ViewBag.Instructors = instructorList;
            ViewBag.ID = id;
            ViewBag.IsPDF = isPDF;
            AspNetUsers instructor = null;
            if (id != 0)
            {
                instructor = db.AspNetUsers.FirstOrDefault(x => x.UserId == id);
                ViewBag.InstructorClasses = Utilities.GetInstance().GetInstructorClasses(id);
            }
            return View(instructor);
        }
        public ActionResult InstructorFullViewPDF(int id)
        {
            return new Rotativa.ActionAsPdf("InstructorFullView", new { id = id, isPDF = true }) { FileName = "Instructor Full View.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
        public ActionResult ExamFullDetails(int cid, int sid)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            var exams = db.CourseClass.FirstOrDefault(x => x.Id == cid).Exam.Select(x => x.Id).ToList();
            var evals = db.Evaluation.Where(x => !x.IsStudentEvaluation && x.StudentId == sid && exams.Contains(x.ExamId)).ToList();
            ViewBag.EvalTypes = db.EvaluationTypeSet.OrderBy(x => x.Id).ToList();
            List<ExamFullView> model = new List<ExamFullView>();
            foreach (var exam in exams)
            {
                var evals1 = evals.Where(x => x.ExamId == exam).OrderBy(x => x.EvaluationTypeId).ToList();
                model.Add(new ExamFullView { Exam = evals1.Count == 0 ? null : evals1.First().Exam, Evals = evals1 });
            }
            model.RemoveAll(x => x.Exam == null);
            ViewBag.Student = db.AspNetUsers.FirstOrDefault(x => x.UserId == sid);
            return View(model);
        }
        public ActionResult LineChartForExamEvaluation(int cid, int sid)
        {
            ViewBag.CID = cid;
            ViewBag.SID = sid;
            return View();
        }
        public JsonResult GetLineChartDataForExams(int cid, int sid)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            var exams = db.CourseClass.FirstOrDefault(x => x.Id == cid).Exam.Select(x => x.Id).ToList();
            var evals = db.Evaluation.Where(x => !x.IsStudentEvaluation && x.StudentId == sid && exams.Contains(x.ExamId)).ToList();
            List<ExamFullView> model = new List<ExamFullView>();
            foreach (var exam in exams)
            {
                var evals1 = evals.Where(x => x.ExamId == exam).OrderBy(x => x.EvaluationTypeId).ToList();
                if (evals1.Count() > 0)
                    model.Add(new ExamFullView { Exam = evals1.First().Exam, Evals = evals1 });
            }
            var data = new List<ChartData>();
            foreach (var item in model)
            {
                data.Add(new ChartData(item.Exam.Title, item.Evals.Select(x => double.Parse(x.Grade)).Average()));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PieChartForExamEvaluation(int cid, int sid)
        {
            ViewBag.CID = cid;
            ViewBag.SID = sid;
            return View();
        }
        public JsonResult GetPieChartDataForExams(int cid, int sid)
        {
            StudentClassDetail model = Utilities.GetInstance().GetClassStudentsDetails(cid).FirstOrDefault(x => x.Student.UserId == sid);
            return Json(new { Total = model.Total, Presents = model.Presents, Absents = model.Absents }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BarChartForClassDetails(int id)
        {
            ViewBag.ID = id;
            return View();
        }
        public JsonResult GetBarChartDataForClassDetails(int id)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            SelectList baseList = new SelectList(db.TrainingBase.ToList(), "Id", "Name", id);
            ViewBag.Bases = baseList;
            ViewBag.ID = id;
            var baseDetail = Utilities.GetInstance().GetBasesWithDetails(id).FirstOrDefault();

            var data = new List<ChartData>();
            foreach (var liveClass in baseDetail.ClassesLive)
            {
                List<DateTime> existingDates = new List<DateTime>();
                if (liveClass.ClassDates != null)
                {
                    existingDates = liveClass.ClassDates.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => string.IsNullOrEmpty(x) ? DateTime.MinValue : DateTime.Parse(x)).ToList();
                }
                int totalDays = existingDates.Count;
                int completedDays = existingDates.Where(x => x < DateTime.Now).Count();
                int remainingDays = existingDates.Where(x => x > DateTime.Now).Count();

                data.Add(new ChartData(liveClass.Title, double.Parse(string.IsNullOrEmpty(liveClass.StudentIds) ? "0" : liveClass.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count().ToString()), totalDays, completedDays, remainingDays));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Attendance()
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            SelectList courseList;
            courseList = new SelectList(db.CourseClass.ToList(), "Id", "Title");
            ViewBag.CourseList = courseList;

            return View();
        }
    }
}