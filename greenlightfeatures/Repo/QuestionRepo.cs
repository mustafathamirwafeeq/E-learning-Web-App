using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ELearning.Repo
{
    public  class QuestionRepo
    {
        static QuestionRepo _instance;
        private QuestionRepo()
        {

        }
        public static QuestionRepo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new QuestionRepo();
            }
            return _instance;
        }
        /// <summary>
        /// Return question list by provided course
        /// </summary>
        public  List<Question> GetQuestionByCourse(int courseId)
        {
            List<Question> res = new List<Question>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                res = db.Question.Where(r => r.IsDeleted == false && r.CourseId == courseId).ToList();
            }
            return res;
        }

        /// <summary>
        /// Return question list by provided user
        /// </summary>
        public  IEnumerable<Question> GetQuestions(int userId)
        {
            IEnumerable<Question> res = new List<Question>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                res = db.Question.Where(r => r.IsDeleted == false && r.UserId == userId).Include(c=>c.Course).ToList();
            }
            return res;
        }

        public  string CorrectAnswer(int questionId, int optionId)
        {
            Question question = new Question();
            string res = string.Empty;

            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                question = db.Question.Where(r => r.Id == questionId).FirstOrDefault();
            }

            if (optionId == 1)
            {
                res = question.Option1;
            }
            else if (optionId == 2)
            {
                res = question.Option2;
            }
            else if (optionId == 3)
            {
                res = question.Option3;
            }
            else if (optionId == 4)
            {
                res = question.Option4;
            }
            return res;
        }

        
    }
}