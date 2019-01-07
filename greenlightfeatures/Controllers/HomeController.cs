using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearning.Controllers
{

    public class HomeController : Controller
    {
        /// <summary>
        /// View Default home page
        /// </summary>
        public ActionResult Index()
        {
            
            var content = Utilities.GetInstance().PageContent(Models.Enum.CMSPageEnum.Welcome);
            return View(content);
        }

        /// <summary>
        /// View About us page
        /// </summary>
        public ActionResult About()
        {
            var content = Utilities.GetInstance().PageContent(Models.Enum.CMSPageEnum.AboutUs);
            return View(content);
        }

        /// <summary>
        /// View Contact us page
        /// </summary>
        public ActionResult Contact()
        {
            var content = Utilities.GetInstance().PageContent(Models.Enum.CMSPageEnum.ContactUs);
            return View(content);
        }

        /// <summary>
        /// View Role based Dashboard
        /// </summary>
        public ActionResult Dashboard(bool isPDF = false)
        {
            ViewBag.IsPDF = isPDF;
            return View();
        }
        public ActionResult UserManDashboardPDF()
        {
            return new Rotativa.ViewAsPdf("Dashboard", new { isPDF = true }) { FileName = "Management Dashboard.pdf", PageMargins = Utilities.GetInstance().PDFMargins };
        }
    }
}