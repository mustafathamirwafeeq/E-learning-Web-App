using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.Repo
{
    public  class NewsRepo
    {
        static NewsRepo _instance;
        private NewsRepo()
        {

        }
        public NewsRepo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NewsRepo();
            }
            return _instance;
        }
        /// <summary>
        /// return top 5 news list in desending order
        /// </summary>
        public  List<News> GetNews()
        {
            List<News> res = new List<News>();
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                res = db.News.OrderByDescending(r => r.PubDate).Take(5).ToList();
            }
            return res;
        }

        
    }
}