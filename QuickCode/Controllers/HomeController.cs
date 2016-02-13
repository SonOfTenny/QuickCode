using QuickCode.Models;
using QuickCode.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickCode.Controllers
{
    public class HomeController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();
       

        public ActionResult Index()
        {
            var dt = DateTime.Now.AddDays(-7);
            //var dashboard = from s in db.Productions
            //                where (s.Date > dt)
            //                select s;
            var dash = new DashboardData();
           
            var prodVM = new ProductionVM();
            // Batch 
            prodVM.BatchTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select s ).Sum(s => s.Cmp_Waste);
            prodVM.BatchTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select s).Sum(s => s.CrumbWaste);
            prodVM.BatchTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select s).Sum(s => s.Gen_Pack_Waste);
            prodVM.BatchTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select s).Sum(s => s.Pack_Waste);
            prodVM.BatchTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select s).Sum(s => s.ActualMix);
            prodVM.BatchTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select s).Sum(s => s.TotalProdMins);
            //prodVM.BatchTotalManning = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select s).Sum(s => s.Manning);
            prodVM.BatchTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Batch" select s).Sum(s => (s.TotalDownMins));
            //-------------------------------------------------------------------------------------------------------------------
            // PAN 
            prodVM.PanTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select s).Sum(s => s.Cmp_Waste);
            prodVM.PanTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select s).Sum(s => s.CrumbWaste);
            prodVM.PanTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select s).Sum(s => s.Gen_Pack_Waste);
            prodVM.PanTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select s).Sum(s => s.Pack_Waste);
            prodVM.PanTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select s).Sum(s => s.ActualMix);
            prodVM.PanTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select s).Sum(s => s.TotalProdMins);
            //prodVM.PanTotalManning = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select s).Sum(s => s.Manning);
            prodVM.PanTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Pan" select s).Sum(s => s.TotalDownMins);
            //-------------------------------------------------------------------------------------------------------------------
            // ROLL 
            prodVM.RollTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select s).Sum(s => s.Cmp_Waste);
            prodVM.RollTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select s).Sum(s => s.CrumbWaste);
            prodVM.RollTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select s).Sum(s => s.Gen_Pack_Waste);
            prodVM.RollTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select s).Sum(s => s.Pack_Waste);
            prodVM.RollTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select s).Sum(s => s.ActualMix);
            prodVM.RollTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select s).Sum(s => s.TotalProdMins);
            //prodVM.RollTotalManning = (from s in db.Productions where (s.Date > dt)  && s.Plant.Name == "Roll" select s).Sum(s => s.Manning);
            prodVM.RollTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Roll" select s).Sum(s => s.TotalDownMins);
            //-------------------------------------------------------------------------------------------------------------------
            // WHEATEN 
            prodVM.WheatenTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select s).Sum(s => s.Cmp_Waste);
            prodVM.WheatenTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select s).Sum(s => s.CrumbWaste);
            prodVM.WheatenTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select s).Sum(s => s.Gen_Pack_Waste);
            prodVM.WheatenTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select s).Sum(s => s.Pack_Waste);
            prodVM.WheatenTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select s).Sum(s => s.ActualMix);
            prodVM.WheatenTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select s).Sum(s => s.TotalProdMins);
            //prodVM.WheatenTotalManning = (from s in db.Productions where (s.Date > dt)  && s.Plant.Name == "Wheaten" select s).Sum(s => s.Manning);
            prodVM.WheatenTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Wheaten" select s).Sum(s => s.TotalDownMins);
            //-------------------------------------------------------------------------------------------------------------------
            // SODA 
            prodVM.SodaTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select s).Sum(s => s.Cmp_Waste);
            prodVM.SodaTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select s).Sum(s => s.CrumbWaste);
            prodVM.SodaTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select s).Sum(s => s.Gen_Pack_Waste);
            prodVM.SodaTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select s).Sum(s => s.Pack_Waste);
            prodVM.SodaTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select s).Sum(s => s.ActualMix);
            prodVM.SodaTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select s).Sum(s => s.TotalProdMins);
            //prodVM.SodaTotalManning = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select s).Sum(s => s.Manning);
            prodVM.SodaTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Soda" select s).Sum(s => s.TotalDownMins);
            //-------------------------------------------------------------------------------------------------------------------
            // PANCAKES 
            prodVM.PancakesTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Cmp_Waste);
            prodVM.PancakesTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.CrumbWaste);
            prodVM.PancakesTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Gen_Pack_Waste);
            prodVM.PancakesTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Pack_Waste);
            prodVM.PancakesTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.ActualMix);
            prodVM.PancakesTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.TotalProdMins);
            //prodVM.PancakesTotalManning = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Manning);
            prodVM.PancakesTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.TotalDownMins);
            //-------------------------------------------------------------------------------------------------------------------
            // PANCAKES 
            prodVM.PancakesTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Cmp_Waste);
            prodVM.PancakesTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.CrumbWaste);
            prodVM.PancakesTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Gen_Pack_Waste);
            prodVM.PancakesTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Pack_Waste);
            prodVM.PancakesTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.ActualMix);
            prodVM.PancakesTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.TotalProdMins);
            //prodVM.PancakesTotalManning = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Manning);
            prodVM.PancakesTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Pancakes" select s).Sum(s => s.TotalDownMins);
            //-------------------------------------------------------------------------------------------------------------------
            // POTATO 
            prodVM.PotatoTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select s).Sum(s => s.Cmp_Waste);
            prodVM.PotatoTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select s).Sum(s => s.CrumbWaste);
            prodVM.PotatoTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select s).Sum(s => s.Gen_Pack_Waste);
            prodVM.PotatoTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select s).Sum(s => s.Pack_Waste);
            prodVM.PotatoTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select s).Sum(s => s.ActualMix);
            prodVM.PotatoTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select s).Sum(s => s.TotalProdMins);
            //prodVM.PotatoTotalManning = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select s).Sum(s => s.Manning);
            prodVM.PotatoTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Potato" select s).Sum(s => s.TotalDownMins);

            return View(prodVM);
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