using QuickCode.Models;
using QuickCode.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickCode.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            using (var ctx = new ApplicationDbContext())
            {
                var weeklyProdTime = ctx.Productions.SqlQuery("Select *  from Productions").ToList();
                ViewBag.weeklyProdTime = weeklyProdTime;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}