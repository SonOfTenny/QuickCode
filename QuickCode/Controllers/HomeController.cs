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

            DateTime input = DateTime.Now;
            int delta = DayOfWeek.Monday - input.DayOfWeek;
            DateTime monday = input.AddDays(delta);
            DateTime fiscalWk = monday; // current week
            //var dashboard = from s in db.Productions
            //                where (s.Date > dt)
            //                select s;
            var dash = new DashboardData();
            var tw = DateTime.Now.DayOfWeek;
            var prodVM = new ProductionVM();
            prodVM.FiscalWeek = monday.Date;
            DateTime m = monday.Date;
            var fdate = from s in db.FiscalWeeks
                        where (s.WeekStart == m)
                        select s.WeekNumber;
          
                prodVM.FiscalWeekNo = (fdate).FirstOrDefault();

            // takes current week - 1 to get next (future) week
            int futureWeek = fdate.FirstOrDefault() + 1;
            // find last weeks start date
            var futureWeeksDate = from s in db.FiscalWeeks
                                where (s.WeekNumber == futureWeek)
                                select s.WeekStart;
            DateTime fm = futureWeeksDate.FirstOrDefault();
            prodVM.FutureFiscalWeekNo = futureWeek;
            prodVM.FutreFiscalWeek = fm;
            // takes current week - 1 to get last week
            int lastWeek = fdate.FirstOrDefault() - 1;
            // find last weeks start date
            var lastWeeksDate = from s in db.FiscalWeeks
                                where (s.WeekNumber == lastWeek)
                                select s.WeekStart;
            // last weeks date
            DateTime lm = lastWeeksDate.FirstOrDefault();
            prodVM.LastFiscalWeekNo = lastWeek;
            prodVM.LastFiscalWeek = lm;
            // two weeks backs from now                                 LM = LAST WEEKS DATE MONDAY = CURRENT WEEK
            int last2Weeks = fdate.FirstOrDefault() - 2;
            var twoWeeksAgo = from s in db.FiscalWeeks
                              where (s.WeekNumber == last2Weeks)
                              select s.WeekStart;
            // Week date calc
            DateTime zm = twoWeeksAgo.FirstOrDefault();
            prodVM.Last2FiscalWeeks = zm.ToShortDateString();
            // create a list of production dates
            var prodDates = from s in db.FiscalWeeks
                            select s;

           
            // Batch ------------------------------------------------------------------------------------------------------------
            prodVM.BatchTotalComp = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Batch") select (double?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.BatchTotalCrumb = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Batch") select (double?)s.CrumbWaste).Sum() ?? 0;
            prodVM.BatchTotalGenPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Batch") select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.BatchTotalPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Batch") select (double?)s.Pack_Waste).Sum() ?? 0;
            prodVM.BatchTotalMixes = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Batch") select (double?)s.ActualMix).Sum() ?? 0;
            prodVM.BatchTotalTime = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Batch") select (double?)s.TotalProdMins).Sum() ?? 0;
            prodVM.BatchTotalManning = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Batch") select (double?)s.Manning).Sum() ?? 0;
            prodVM.BatchTotalDowntime = (from s in db.Downtime where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Batch") select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // PAN 
            prodVM.PanTotalComp = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pan") select (double?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.PanTotalCrumb = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pan") select (double?)s.CrumbWaste).Sum() ?? 0;
            prodVM.PanTotalGenPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pan") select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.PanTotalPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pan") select (double?)s.Pack_Waste).Sum() ?? 0;
            prodVM.PanTotalMixes = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pan") select (double?)s.ActualMix).Sum() ?? 0;
            prodVM.PanTotalTime = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pan") select (double?)s.TotalProdMins).Sum() ?? 0;
            prodVM.PanTotalManning = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pan") select (double?)s.Manning).Sum() ?? 0;
            prodVM.PanTotalDowntime = (from s in db.Downtime where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pan") select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // ROLL 
            prodVM.RollTotalComp = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Roll") select (double?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.RollTotalCrumb = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Roll") select (double?)s.CrumbWaste).Sum() ?? 0;
            prodVM.RollTotalGenPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Roll") select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.RollTotalPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Roll") select (double?)s.Pack_Waste).Sum() ?? 0;
            prodVM.RollTotalMixes = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Roll") select (double?)s.ActualMix).Sum() ?? 0;
            prodVM.RollTotalTime = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Roll") select (double?)s.TotalProdMins).Sum() ?? 0;
            prodVM.RollTotalManning = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Roll") select (double?)s.Manning).Sum() ?? 0;
            prodVM.RollTotalDowntime = (from s in db.Downtime where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Roll") select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // WHEATEN 
            prodVM.WheatenTotalComp = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Wheaten") select (double?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.WheatenTotalCrumb = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Wheaten") select (double?)s.CrumbWaste).Sum() ?? 0;
            prodVM.WheatenTotalGenPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Wheaten") select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.WheatenTotalPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Wheaten") select (double?)s.Pack_Waste).Sum() ?? 0;
            prodVM.WheatenTotalMixes = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Wheaten") select (double?)s.ActualMix).Sum() ?? 0;
            prodVM.WheatenTotalTime = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Wheaten") select (double?)s.TotalProdMins).Sum() ?? 0;
            prodVM.WheatenTotalManning = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Wheaten") select (double?)s.Manning).Sum() ?? 0;
            prodVM.WheatenTotalDowntime = (from s in db.Downtime where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Wheaten") select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // SODA 
            prodVM.SodaTotalComp = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Soda") select (double?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.SodaTotalCrumb = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Soda") select (double?)s.CrumbWaste).Sum() ?? 0;
            prodVM.SodaTotalGenPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Soda") select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.SodaTotalPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Soda") select (double?)s.Pack_Waste).Sum() ?? 0;
            prodVM.SodaTotalMixes = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Soda") select (double?)s.ActualMix).Sum() ?? 0;
            prodVM.SodaTotalTime = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Soda") select (double?)s.TotalProdMins).Sum() ?? 0;
            prodVM.SodaTotalManning = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Soda") select (double?)s.Manning).Sum() ?? 0;
            prodVM.SodaTotalDowntime = (from s in db.Downtime where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Soda") select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            //// PANCAKES 
            //prodVM.PancakesTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.Cmp_Waste).Sum() ?? 0;
            //prodVM.PancakesTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.CrumbWaste).Sum() ?? 0;
            //prodVM.PancakesTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
            //prodVM.PancakesTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.Pack_Waste).Sum() ?? 0;
            //prodVM.PancakesTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.ActualMix).Sum() ?? 0;
            //prodVM.PancakesTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (double?)s.TotalProdMins).Sum() ?? 0;
            ////prodVM.PancakesTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Manning);
            //prodVM.PancakesTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Pancakes" select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // PANCAKES 
            prodVM.PancakesTotalComp = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pancakes") select (double?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.PancakesTotalCrumb = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pancakes") select (double?)s.CrumbWaste).Sum() ?? 0;
            prodVM.PancakesTotalGenPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pancakes") select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.PancakesTotalPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pancakes") select (double?)s.Pack_Waste).Sum() ?? 0;
            prodVM.PancakesTotalMixes = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pancakes") select (double?)s.ActualMix).Sum() ?? 0;
            prodVM.PancakesTotalTime = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pancakes") select (double?)s.TotalProdMins).Sum() ?? 0;
            prodVM.PancakesTotalManning = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pancakes") select (double?)s.Manning).Sum() ?? 0;
            prodVM.PancakesTotalDowntime = (from s in db.Downtime where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Pancakes") select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // POTATO 
            prodVM.PotatoTotalComp = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Potato") select (double?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.PotatoTotalCrumb = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Potato") select (double?)s.CrumbWaste).Sum() ?? 0;
            prodVM.PotatoTotalGenPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Potato") select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.PotatoTotalPack = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Potato") select (double?)s.Pack_Waste).Sum() ?? 0;
            prodVM.PotatoTotalMixes = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Potato") select (double?)s.ActualMix).Sum() ?? 0;
            prodVM.PotatoTotalTime = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Potato") select (double?)s.TotalProdMins).Sum() ?? 0;
            prodVM.PotatoTotalManning = (from s in db.Productions where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Potato") select (double?)s.TotalProdMins).Sum() ?? 0;
            prodVM.PotatoTotalDowntime = (from s in db.Downtime where (s.StartDate >= monday) && (s.StartDate <= fm) && (s.Plant.Name == "Potato") select (double?)s.TotalDownMins).Sum() ?? 0;


            return View(prodVM);
        }

        public ActionResult Dashboard(DateTime? DateFrom, DateTime? DateTo, String PlantName, String Downtypes, String Shift)
        {

            // Date config:
            DateTime df, dt;
            if (DateFrom.HasValue)
                df = DateFrom.Value;
            else
                df = DateFrom ?? DateTime.Now;
            if (DateTo.HasValue)
                dt = DateTo.Value;
            else
                dt = DateTo ?? DateTime.Now;
            string a = df.ToString("HH:mm");
            string b = dt.ToString("HH:mm");
            DateTime st = DateTime.Parse(a); /*Convert.ToDateTime(startTime);*/
            DateTime et = DateTime.Parse(b);
            //--- Custom date range dashboard alternative ------
            var dash = new DashboardData();

            var prodVM = new ProductionVM();
          
            if (DateFrom.HasValue)
            {
                prodVM.BatchTotalComp = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == PlantName select (double?)s.Cmp_Waste).Sum() ?? 0;
                prodVM.BatchTotalCrumb = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == PlantName select (double?)s.CrumbWaste).Sum() ?? 0;
                prodVM.BatchTotalGenPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == PlantName select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
                prodVM.BatchTotalPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == PlantName select (double?)s.Pack_Waste).Sum() ?? 0;
                prodVM.BatchTotalMixes = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == PlantName select (double?)s.ActualMix).Sum() ?? 0;
                prodVM.BatchTotalTime = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == PlantName select (double?)s.TotalProdMins).Sum() ?? 0;
                Math.Abs(prodVM.BatchTotalTime);
                prodVM.BatchTotalManning = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate < DateTo) && s.Plant.Name == PlantName select (double?)s.Manning).Sum() ?? 0;
                prodVM.BatchTotalDowntime = (from s in db.Downtime where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == PlantName select (double?)s.TotalDownMins).Sum() ?? 0;
                prodVM.PlantName = PlantName;
                prodVM.Downtime = (from s in db.Downtime select s);
                prodVM.StartTime = st;
                prodVM.EndTime = et;
                prodVM.StartDate = df;
                prodVM.EndTime = dt;
                
                // Batch 
                //prodVM.BatchTotalComp = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.Cmp_Waste).Sum() ?? 0;
                //prodVM.BatchTotalCrumb = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.CrumbWaste).Sum() ?? 0;
                //prodVM.BatchTotalGenPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
                //prodVM.BatchTotalPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.Pack_Waste).Sum() ?? 0;
                //prodVM.BatchTotalMixes = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.ActualMix).Sum() ?? 0;
                //prodVM.BatchTotalTime = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.TotalProdMins).Sum() ?? 0;
                //prodVM.BatchTotalManning = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate < DateTo) && s.Plant.Name == "Batch" select (double?)s.Manning).Sum() ?? 0;
                //prodVM.BatchTotalDowntime = (from s in db.Downtime where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.TotalDownMins).Sum() ?? 0;
                // //-------------------------------------------------------------------------------------------------------------------
                // // PAN 
                // prodVM.PanTotalComp = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pan" select (double?)s.Cmp_Waste).Sum() ?? 0;
                // prodVM.PanTotalCrumb = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pan" select (double?)s.CrumbWaste).Sum() ?? 0;
                // prodVM.PanTotalGenPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pan" select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
                // prodVM.PanTotalPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pan" select (double?)s.Pack_Waste).Sum() ?? 0;
                // prodVM.PanTotalMixes = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pan" select (double?)s.ActualMix).Sum() ?? 0;
                // prodVM.PanTotalTime = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pan" select (double?)s.TotalProdMins).Sum() ?? 0;
                // prodVM.PanTotalManning = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.Manning).Sum() ?? 0;
                // prodVM.PanTotalDowntime = (from s in db.Downtime where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pan" select (double?)s.TotalDownMins).Sum() ?? 0;
                // //-------------------------------------------------------------------------------------------------------------------
                // // ROLL 
                // prodVM.RollTotalComp = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Roll" select (double?)s.Cmp_Waste).Sum() ?? 0;
                // prodVM.RollTotalCrumb = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Roll" select (double?)s.CrumbWaste).Sum() ?? 0;
                // prodVM.RollTotalGenPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Roll" select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
                // prodVM.RollTotalPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Roll" select (double?)s.Pack_Waste).Sum() ?? 0;
                // prodVM.RollTotalMixes = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.ActualMix).Sum() ?? 0;
                // prodVM.RollTotalTime = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Roll" select (double?)s.TotalProdMins).Sum() ?? 0;
                // prodVM.RollTotalManning = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Roll" select (double?)s.Manning).Sum() ?? 0;
                // prodVM.RollTotalDowntime = (from s in db.Downtime where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Roll" select (double?)s.TotalDownMins).Sum() ?? 0;
                // //-------------------------------------------------------------------------------------------------------------------
                // // WHEATEN 
                // prodVM.WheatenTotalComp = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Wheaten" select (double?)s.Cmp_Waste).Sum() ?? 0;
                // prodVM.WheatenTotalCrumb = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Wheaten" select (double?)s.CrumbWaste).Sum() ?? 0;
                // prodVM.WheatenTotalGenPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Wheaten" select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
                // prodVM.WheatenTotalPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Wheaten" select (double?)s.Pack_Waste).Sum() ?? 0;
                // prodVM.WheatenTotalMixes = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Wheaten" select (double?)s.ActualMix).Sum() ?? 0;
                // prodVM.WheatenTotalTime = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Wheaten" select (double?)s.TotalProdMins).Sum() ?? 0;
                // prodVM.WheatenTotalManning = (from s in db.Productions where(s.StartDate >= DateFrom && s.EndDate <= DateTo)  && s.Plant.Name == "Wheaten" select (double?)s.Manning).Sum() ?? 0;
                // prodVM.WheatenTotalDowntime = (from s in db.Downtime where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Wheaten" select (double?)s.TotalDownMins).Sum() ?? 0;
                // //-------------------------------------------------------------------------------------------------------------------
                // // SODA 
                // prodVM.SodaTotalComp = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Soda" select (double?)s.Cmp_Waste).Sum() ?? 0;
                // prodVM.SodaTotalCrumb = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Soda" select (double?)s.CrumbWaste).Sum() ?? 0;
                // prodVM.SodaTotalGenPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Soda" select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
                // prodVM.SodaTotalPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Soda" select (double?)s.Pack_Waste).Sum() ?? 0;
                // prodVM.SodaTotalMixes = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Soda" select (double?)s.ActualMix).Sum() ?? 0;
                // prodVM.SodaTotalTime = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Soda" select (double?)s.TotalProdMins).Sum() ?? 0;
                // prodVM.SodaTotalManning = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Soda" select (double?)s.TotalProdMins).Sum() ?? 0;
                // prodVM.SodaTotalDowntime = (from s in db.Downtime where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Soda" select (double?)s.TotalDownMins).Sum() ?? 0;
                // //-------------------------------------------------------------------------------------------------------------------
                // // PANCAKES 
                // prodVM.PancakesTotalComp = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.Cmp_Waste).Sum() ?? 0;
                // prodVM.PancakesTotalCrumb = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.CrumbWaste).Sum() ?? 0;
                // prodVM.PancakesTotalGenPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
                // prodVM.PancakesTotalPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.Pack_Waste).Sum() ?? 0;
                // prodVM.PancakesTotalMixes = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.ActualMix).Sum() ?? 0;
                // prodVM.PancakesTotalTime = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.TotalProdMins).Sum() ?? 0;
                // prodVM.PancakesTotalManning = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.Manning).Sum() ?? 0;
                // prodVM.PancakesTotalDowntime = (from s in db.Downtime where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.TotalDownMins).Sum() ?? 0;
                // //-------------------------------------------------------------------------------------------------------------------
                // //// PANCAKES 
                // //prodVM.PancakesTotalComp = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (int?)s.Cmp_Waste).Sum() ?? 0;
                // //prodVM.PancakesTotalCrumb = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (int?)s.CrumbWaste).Sum() ?? 0;
                // //prodVM.PancakesTotalGenPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
                // //prodVM.PancakesTotalPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (int?)s.Pack_Waste).Sum() ?? 0;
                // //prodVM.PancakesTotalMixes = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (int?)s.ActualMix).Sum() ?? 0;
                // //prodVM.PancakesTotalTime = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.TotalProdMins).Sum() ?? 0;
                // ////prodVM.PancakesTotalManning = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Manning);
                // //prodVM.PancakesTotalDowntime = (from s in db.Downtime where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Batch" select (double?)s.TotalDownMins).Sum() ?? 0;
                // //-------------------------------------------------------------------------------------------------------------------
                // // POTATO 
                // prodVM.PotatoTotalComp = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Potato" select (double?)s.Cmp_Waste).Sum() ?? 0;
                // prodVM.PotatoTotalCrumb = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Potato" select (double?)s.CrumbWaste).Sum() ?? 0;
                // prodVM.PotatoTotalGenPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Potato" select (double?)s.Gen_Pack_Waste).Sum() ?? 0;
                // prodVM.PotatoTotalPack = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Potato" select (double?)s.Pack_Waste).Sum() ?? 0;
                // prodVM.PotatoTotalMixes = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Potato" select (double?)s.ActualMix).Sum() ?? 0;
                // prodVM.PotatoTotalTime = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Potato" select (double?)s.TotalProdMins).Sum() ?? 0;
                // prodVM.PotatoTotalManning = (from s in db.Productions where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Potato" select (double?)s.Manning).Sum() ?? 0;
                // prodVM.PotatoTotalDowntime = (from s in db.Downtime where (s.StartDate >= DateFrom && s.EndDate <= DateTo) && s.Plant.Name == "Potato" select (double?)s.TotalDownMins).Sum() ?? 0;



            }

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
        public ActionResult Admin()
        {

            var userVM = new ProductionVM();

            var user = from u in db.Users
                       select u;

           

            return View(user.ToList());
        }
    }
}