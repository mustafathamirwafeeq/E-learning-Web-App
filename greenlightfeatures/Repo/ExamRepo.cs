using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ELearning.Repo
{
    public  class ExamRepo
    {
        static ExamRepo _instance;
        private ExamRepo()
        {

        }
        public static ExamRepo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ExamRepo();
            }
            return _instance;
        }
        /// <summary>
        /// Return count of exam taken for provided exam and user/student
        /// </summary>
        public  int ExamTaken(int examId, int userId)
        {
            int count = 0;
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                count = (from e in db.Evaluation
                         where e.StudentId == userId && e.ExamId == examId
                         select new { e.ExamId, e.StudentId }).Distinct().Count();

            }
            return count;
        }

        /// <summary>
        /// Return count of exam for provided course and user/student
        /// </summary>
        public  int UserExamsCount(int userId, int courseId)
        {
            int count = 0;
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                count = (from ev in db.Evaluation
                            join ex in db.Exam on ev.ExamId equals ex.Id
                            join c in db.CourseClass on ex.CourseClassId equals c.Id
                         where ev.StudentId == userId && c.Id == courseId
                         select new { ev.StudentId, ev.ExamId, c.Id }).Distinct().Count();

            }
            return count;
        }

        /// <summary>
        /// Return non-deleted list of exam-types
        /// </summary>
        public  IEnumerable<ExamType> GetActiveExamTypes()
        {
            IEnumerable<ExamType> examTypes = new List<ExamType>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                examTypes = db.ExamType.Where(r => r.IsDeleted == false).ToList();
            }
            return examTypes;
        }

        
    }
}