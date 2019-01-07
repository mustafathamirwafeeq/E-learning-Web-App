using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ELearning.Repo
{
    public class CourseClassRepo
    {
         static CourseClassRepo _instance;
        private CourseClassRepo()
        {

        }
        public static CourseClassRepo GetInstance()
        {
            if(_instance == null)
            {
                _instance = new CourseClassRepo();
            }
            return _instance;
        }
        /// <summary>
        /// Return courses for dropdown-list
        /// </summary>
        public  IEnumerable<CourseClass> GetClassesForDDL()
        {
            if (HttpContext.Current.User.IsInRole("Admin"))
                return GetActiveClasses(0);
            else
                return GetActiveClasses(Utilities.GetInstance().GetCurrentUserId());
        }

        /// <summary>
        /// Return published and non-deleted courses list for provided user
        /// </summary>
        public  IEnumerable<CourseClass> GetActiveClasses(int userId = 0)
        {
            IEnumerable<CourseClass> classes = new List<CourseClass>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                if (userId == 0)
                    classes = db.CourseClass.Where(r=>r.Status == true).ToList();
                else
                    classes = db.CourseClass.Where(r => r.Status== true ).ToList();
            }
            return classes;
        }

        /// <summary>
        /// Return course list for provided user
        /// </summary>
        public  IEnumerable<CourseClass> GetClasses(int userId)
        {
            IEnumerable<CourseClass> classes = new List<CourseClass>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                classes = db.CourseClass.Where(r => r.Status== true).ToList();
            }
            return classes;
        }

        public  bool IsClassExist(string name, bool editMode = false)
        {
            int count = 0;
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                count = db.CourseClass.Count(r => r.Status == true && r.Title == name);
            }
            if (editMode)
                return count > 1;
            else
                return count > 0;
        }

    }
}