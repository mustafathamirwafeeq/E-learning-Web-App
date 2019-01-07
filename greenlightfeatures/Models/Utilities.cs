using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Entity;
using ELearning.Models.Enum;
using ELearning.Repo;
using ELearning.Models.Dto;

namespace ELearning.Models
{
    public class Utilities
    {
        static Utilities _instance;
        ApplicationDbContext db = new ApplicationDbContext();

        private Utilities()
        {

        }
        public Rotativa.Options.Margins PDFMargins = new Rotativa.Options.Margins(10, 5, 10, 5);
        public static Utilities GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Utilities();
            }
            return _instance;
        }

        public int GetUserCount()
        {
            return db.Users.Count() - 1;
        }
        public int GetUserCountIncludingAdmin()
        {
            return db.Users.Count();
        }

        public string GetCurrentUserName()
        {
            var user = db.Users.Where(r => r.UserName == HttpContext.Current.User.Identity.Name).FirstOrDefault();
            return user == null ? "" : user.FullName;
        }

        public string GetUserName(int userId)
        {
            var user = db.Users.Where(r => r.UserId == userId).FirstOrDefault();
            if (UserRepo.GetInstance().GetUserRole(userId) == "Visitor")
                return string.Empty;
            else
                return user.UserName;
        }

        public string GetUserFullName(int userId)
        {
            var user = db.Users.Where(r => r.UserId == userId).FirstOrDefault();
            if (UserRepo.GetInstance().GetUserRole(userId) == "Visitor")
                return string.Empty;
            else
                return user.FullName;
        }

        public int GetCurrentUserId()
        {
            var user = db.Users.Where(r => r.UserName == HttpContext.Current.User.Identity.Name).FirstOrDefault();
            return user == null ? 0 : user.UserId;
        }

        public string GetCurrentUserRole()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = db.Users.Where(r => r.UserName == HttpContext.Current.User.Identity.Name).FirstOrDefault();
                var roleId = user.Roles.Where(u => u.UserId.Equals(user.Id)).FirstOrDefault().RoleId;
                return db.Roles.Where(r => r.Id.Equals(roleId)).FirstOrDefault().Name;
            }
            else
            {
                return string.Empty;
            }
        }

        public List<Attachment> GetAttachments()
        {
            List<Attachment> attachments = new List<Attachment>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                if (HttpContext.Current.User.IsInRole("Admin"))
                {
                    attachments = db.Attachment.Include(a => a.CourseClass).Include(x => x.CourseClass.Course).ToList();
                }
                else
                {
                    int userId = GetCurrentUserId();
                    attachments = db.Attachment.Include(a => a.CourseClass).Include(x => x.CourseClass.Course).Where(x => x.CourseClass.InstructorId == userId).ToList();
                }
            }
            return attachments;
        }

        public List<Exam> GetExams()
        {
            List<Exam> exams = new List<Exam>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                if (HttpContext.Current.User.IsInRole("Admin"))
                {
                    exams = db.Exam.Where(r => r.IsDeleted == false).Include(e => e.CourseClass).Include(e => e.ExamType).Include(e => e.Evaluation).ToList();
                }
                else
                {
                    int userId = GetCurrentUserId();
                    exams = db.Exam.Where(r => r.UserId == userId && r.IsDeleted == false).Include(e => e.CourseClass).Include(e => e.ExamType).Include(e => e.Evaluation).ToList();
                }
            }
            return exams;
        }

        public int[] GetExamQuestion(int examId)
        {
            int userId = GetCurrentUserId();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                if (HttpContext.Current.User.IsInRole("Admin"))
                {
                    return (from i in db.ExamQuestion
                            join o in db.Question on i.QuestionId equals o.Id
                            where i.ExamId == examId
                            select o.Id).ToArray();
                }
                else
                {
                    return (from i in db.ExamQuestion
                            join o in db.Question on i.QuestionId equals o.Id
                            where i.ExamId == examId & o.UserId == userId
                            select o.Id).ToArray();
                }
            }
        }

        public string UserCourseStatus(string studentIds, DateTime endDate)
        {
            string id = GetCurrentUserId().ToString();
            string res = string.Empty;
            if (studentIds.Split(',').Contains(id) && endDate > DateTime.Now)
                res = "Started";
            else if (studentIds.Split(',').Contains(id) && endDate < DateTime.Now)
                res = "Finished";

            return res;
        }

        public string CMSPageName(int id)
        {
            string res = string.Empty;
            if (id == (int)CMSPageEnum.Welcome)
                res = "Welcome";
            else if (id == (int)CMSPageEnum.AboutUs)
                res = "About Us";

            return res;
        }

        public CMSPage PageContent(CMSPageEnum pageId)
        {
            CMSPage page = new CMSPage();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                page = db.CMSPage.Where(r => r.Id == (int)pageId).FirstOrDefault();
            }
            page = page == null ? new CMSPage() : page;
            return page;
        }

        public List<AspNetUsers> GetUsersByRole(string roleName)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            return (from x in db.AspNetUsers
                    join y in db.AspNetUserRoles on x.Id equals y.UserId
                    join z in db.AspNetRoles on y.RoleId equals z.Id
                    where z.Name == roleName
                    select x).ToList();
        }
        public AspNetUsers GetUserByID(int id)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            return db.AspNetUsers.FirstOrDefault(x => x.UserId == id);
        }
        public List<CourseClass> GetStudentClasses(string id)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            var classes = db.CourseClass.Where(x => x.Status && x.StartDate < DateTime.Now).Include(c => c.Course).Include(c => c.TrainingBase).OrderBy(x => x.Status).ThenByDescending(x => x.StartDate).ThenByDescending(x => x.StartTime).ToList();
            List<CourseClass> studentClasses = new List<CourseClass>();
            foreach (var cls in classes)
            {
                if (!cls.StudentIds.Split(',').Contains(id))
                    continue;
                studentClasses.Add(cls);
            }
            return studentClasses;
        }
        public List<CourseClass> GetInstructorClasses(int id)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            var classes = db.CourseClass.Where(x => x.Status && x.StartDate < DateTime.Now && x.InstructorId == id).Include(c => c.Course).Include(c => c.TrainingBase).OrderBy(x => x.Status).ThenByDescending(x => x.StartDate).ThenByDescending(x => x.StartTime).ToList();
            return classes;
        }
        public bool IsInstructorAttendanceAuthorized()
        {
            int id = GetCurrentUserId();
            string role = GetCurrentUserRole();
            bool result = false;
            if (role == "Moderator")
            {
                GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
                var user = db.AspNetUsers.FirstOrDefault(x => x.UserId == id);
                if (user != null)
                {
                    result = user.IsAttendanceAuthorized;
                }
            }
            return result;
        }
        public CourseClass GetClassByID(int id)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            return db.CourseClass.FirstOrDefault(x => x.Id == id);
        }
        public List<string> GetStatistics()
        {
            List<string> stats = new List<string>();
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            stats.Add(db.TrainingBase.Count().ToString());
            stats.Add(db.CourseClass.Count().ToString());
            stats.Add(GetUsersByRole("Moderator").Count.ToString());
            stats.Add(GetUsersByRole("Student").Count.ToString());
            return stats;
        }
        public List<BaseDetail> GetBasesWithDetails(int id = 0)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            var bases = db.TrainingBase.ToList();
            if (id != 0)
                bases.RemoveAll(x => x.Id != id);
            List<BaseDetail> bdList = new List<BaseDetail>();
            foreach (var item in bases)
            {
                BaseDetail bd = new BaseDetail();
                bd.Base = item;
                var classes = db.CourseClass.Where(x => x.TrainingBaseId == item.Id).ToList();
                bd.TotalInstructors = classes.Select(x => x.InstructorId).Distinct().Count();
                bd.ClassesCompleted = classes.Where(x => x.EndDate < DateTime.Now).ToList();
                bd.ClassesLive = classes.Where(x => x.EndDate > DateTime.Now && x.Status).ToList();
                bd.ClassesInitiated = classes;
                List<int> stIDs = new List<int>();
                List<int> stIDsLive = new List<int>();
                classes.ForEach(x => stIDs.AddRange(x.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(y => int.Parse(y))));
                bd.TotalStudents = stIDs.Distinct().Count();
                bd.ClassesLive.ForEach(x => stIDsLive.AddRange(x.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(y => int.Parse(y))));
                bd.LiveStudents = stIDsLive.Distinct().Count();

                bdList.Add(bd);
            }
            return bdList;
        }
        internal List<YearMonthClass> GetGeneralClassesHistory()
        {
            List<YearMonthClass> yearMonthClasses = new List<YearMonthClass>();
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            List<CourseClass> classes = db.CourseClass.ToList();
            foreach (var item in classes)
            {
                yearMonthClasses.Add(new YearMonthClass { Year = item.StartDate.Year, Month = item.StartDate.Month, ClassesCompleted = 0, ClassesInitiated = 1, MonthString = item.StartDate.ToString("MMMM") });
                yearMonthClasses.Add(new YearMonthClass { Year = item.EndDate.Year, Month = item.EndDate.Month, ClassesCompleted = 1, ClassesInitiated = 0, StudentEnrolled = item.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count(), MonthString = item.EndDate.ToString("MMMM") });
            }

            List<YearMonthClass> returnClasses = (from x in yearMonthClasses
                                                  group x by new
                                                  {
                                                      x.Year,
                                                      x.Month
                                                  } into gp
                                                  select new YearMonthClass()
                                                  {
                                                      Year = gp.Key.Year,
                                                      Month = gp.Key.Month,
                                                      ClassesInitiated = gp.Select(x => x.ClassesInitiated).Sum(),
                                                      ClassesCompleted = gp.Select(x => x.ClassesCompleted).Sum(),
                                                      StudentEnrolled = gp.Select(x => x.StudentEnrolled).Sum(),
                                                      MonthString = gp.Select(x => x.MonthString).FirstOrDefault()
                                                  }).OrderBy(x => x.Year).ThenBy(y => y.Month).ToList();
            return returnClasses;
        }
        public List<StudentClassDetail> GetClassStudentsDetails(int classID)
        {
            List<StudentClassDetail> lstData = new List<StudentClassDetail>();
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            var cls = db.CourseClass.FirstOrDefault(x => x.Id == classID);
            List<string> students = cls.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var item in students)
            {
                StudentClassDetail data = new StudentClassDetail();
                int studentID = int.Parse(item);
                data.Class = cls;
                data.Student = db.AspNetUsers.FirstOrDefault(x => x.UserId == studentID);
                var attendances = db.Attendance.Where(x => x.CourseClassId == data.Class.Id).ToList();
                foreach (var attendance in attendances)
                {
                    if (attendance.StudentIds != null && attendance.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Contains(data.Student.UserId.ToString()))
                        data.Presents++;
                    else
                        data.Absents++;

                }
                data.Total = data.Class.ClassDates == null ? 0 : data.Class.ClassDates.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count();
                data.OverAllEvaluationMarks = GetOverAllEvaluationMarks(cls.Id, data.Student.UserId);
                lstData.Add(data);
            }
            return lstData;
        }
        public List<AspNetUsers> GetStudentsFromIDs(string studentIDs)
        {
            List<AspNetUsers> students = new List<AspNetUsers>();
            if (string.IsNullOrEmpty(studentIDs))
                return students;
            List<int> ids = studentIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            students = db.AspNetUsers.Where(x => ids.Contains(x.UserId)).ToList();
            return students;
        }
        public bool SaveEvaluations(string[] values)
        {
            int examId = int.Parse(values[0]);
            int studentId = int.Parse(values[1]);
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            var evaluationTypes = db.EvaluationTypeSet.OrderBy(x => x.Id).ToList();
            int i = 2;
            foreach (var evalType in evaluationTypes)
            {
                var existingEval = db.Evaluation.FirstOrDefault(x => !x.IsStudentEvaluation && x.ExamId == examId && x.StudentId == studentId && x.EvaluationTypeId == evalType.Id);
                if (existingEval == null)
                {
                    Evaluation eval = new Evaluation();
                    eval.EvaluationTypeId = evalType.Id;
                    eval.ExamId = examId;
                    eval.StudentId = studentId;
                    eval.Grade = i < values.Count() ? values[i] : "0";
                    eval.ExamDate = DateTime.Now;
                    eval.IsStudentEvaluation = false;
                    db.Evaluation.Add(eval);
                    db.SaveChanges();
                }
                else
                {
                    existingEval.EvaluationTypeId = evalType.Id;
                    existingEval.ExamId = examId;
                    existingEval.StudentId = studentId;
                    existingEval.Grade = values[i];
                    existingEval.ExamDate = DateTime.Now;
                    db.SaveChanges();
                }
                i++;
            }
            return true;
        }
        public double GetOverAllEvaluationMarks(int classId, int studentId)
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            var classes = db.CourseClass.FirstOrDefault(x => x.Id == classId);
            List<double> marks = new List<double>();
            foreach (var exam in classes.Exam)
            {
                var evals = exam.Evaluation.Where(x => x.StudentId == studentId);
                double mark = evals.Count() > 0 ? evals.Select(x => double.Parse(x.Grade)).Average() : 0;
                marks.Add(mark);
            }
            return marks.Count > 0 ? marks.Average() : 0;
        }
        public int GetEvaluationTypes()
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            return db.EvaluationTypeSet.Count();
        }

        internal bool SaveStudentEvaluations(int eid, int sid, string[] valuesOuter, int rating, string comments)
        {
            foreach (string q in valuesOuter)
            {
                string[] values = q.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                int evalTypeId = int.Parse(values[0]);
                GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
                var existingEval = db.Evaluation.FirstOrDefault(x => x.IsStudentEvaluation && x.ExamId == eid && x.StudentId == sid && x.EvaluationTypeId == evalTypeId);
                if (existingEval == null)
                {
                    Evaluation eval = new Evaluation();
                    eval.Efforts = rating;
                    eval.Comments = comments;
                    eval.EvaluationTypeId = evalTypeId;
                    eval.ExamId = eid;
                    eval.StudentId = sid;
                    eval.Grade = "0";
                    eval.ExamDate = DateTime.Now;
                    eval.IsStudentEvaluation = true;
                    eval.A1 = values[1] == "1" ? true : false;
                    eval.A2 = values[2] == "1" ? true : false;
                    eval.B1 = values[3] == "1" ? true : false;
                    eval.B2 = values[4] == "1" ? true : false;
                    eval.C1 = values[5] == "1" ? true : false;
                    eval.C2 = values[6] == "1" ? true : false;
                    db.Evaluation.Add(eval);
                    db.SaveChanges();
                }
                else
                {
                    existingEval.Efforts = rating;
                    existingEval.Comments = comments;
                    existingEval.ExamId = eid;
                    existingEval.StudentId = sid;
                    existingEval.ExamDate = DateTime.Now;
                    existingEval.A1 = values[1] == "1" ? true : false;
                    existingEval.A2 = values[2] == "1" ? true : false;
                    existingEval.B1 = values[3] == "1" ? true : false;
                    existingEval.B2 = values[4] == "1" ? true : false;
                    existingEval.C1 = values[5] == "1" ? true : false;
                    existingEval.C2 = values[6] == "1" ? true : false;
                    db.SaveChanges();
                }
            }
            return true;
        }
    }

    public class ChartData
    {
        public static List<ChartData> GetBarData()
        {
            var data = new List<ChartData>();
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            List<YearMonthClass> classes = Utilities.GetInstance().GetGeneralClassesHistory().Take(12).ToList();
            foreach (var cls in classes)
            {
                data.Add(new ChartData(cls.MonthString, cls.ClassesInitiated, cls.ClassesCompleted, cls.StudentEnrolled));
            }
            return data;
        }
        public static List<ChartData> GetBarDataForBase(int id)
        {
            var data = new List<ChartData>();
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            List<CourseClass> classes = db.CourseClass.Where(x => x.TrainingBaseId == id).ToList();
            foreach (var cls in classes)
            {
                data.Add(new ChartData(cls.Title, double.Parse(cls.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count().ToString())));
            }
            return data;
        }
        public static List<ChartData> GetLineData()
        {
            GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
            List<CourseClass> classes = db.CourseClass.Where(x => x.EndDate > DateTime.Now && x.Status).OrderBy(x => x.AddedDate).ToList();
            var data = new List<ChartData>();
            foreach (var cls in classes)
            {
                data.Add(new ChartData(cls.TrainingBase.Name + "-" + cls.Title, cls.StudentIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count()));
            }
            return data;
        }



        public ChartData(string label, double value1)
        {
            this.Label = label;
            this.Value1 = value1;
        }

        public ChartData(string label, double value1, double value2)
        {
            this.Label = label;
            this.Value1 = value1;
            this.Value2 = value2;
        }
        public ChartData(string label, double value1, double value2, double value3)
        {
            this.Label = label;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
        }
        public ChartData(string label, double value1, double value2, double value3, double value4)
        {
            this.Label = label;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
            this.Value4 = value4;
        }
        public string Label { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }
        public double Value4 { get; set; }
    }

}