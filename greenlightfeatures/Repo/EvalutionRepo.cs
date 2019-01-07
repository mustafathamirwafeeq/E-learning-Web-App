using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.Repo
{
    public  class EvalutionRepo
    {
        static EvalutionRepo _instance;
        private EvalutionRepo()
        {

        }
        public static EvalutionRepo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EvalutionRepo();
            }
            return _instance;
        }
        /// <summary>
        /// Return evalution list for provided exam
        /// </summary>
        public  List<Evaluation> GetEvalutionByExam(int id)
        {
            List<Evaluation> res = new List<Evaluation>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                res = db.Evaluation.Where(r => r.ExamId == id).OrderByDescending(r=>r.Id).ToList();
            }
            return res;
        }

        /// <summary>
        /// Return evalution list for provided exam and student
        /// </summary>
        public  List<Evaluation> GetEvalutionByStudent(int examid, int studentId)
        {
            List<Evaluation> res = new List<Evaluation>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                res = db.Evaluation.Where(r => r.ExamId == examid & r.StudentId == studentId).OrderByDescending(r => r.Id).ToList();
            }
            return res;
        }
    }
}