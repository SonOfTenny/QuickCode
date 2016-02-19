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
            // Batch ------------------------------------------------------------------------------------------------------------
            prodVM.BatchTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select (int?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.BatchTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select (int?)s.CrumbWaste).Sum() ?? 0;
            prodVM.BatchTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.BatchTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select (int?)s.Pack_Waste).Sum() ?? 0;
            prodVM.BatchTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select (int?)s.ActualMix).Sum() ?? 0;
            prodVM.BatchTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Batch" select (double?)s.TotalProdMins).Sum() ?? 0;
            //prodVM.BatchTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select s).Sum(s => s.Manning);
            prodVM.BatchTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Batch" select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // PAN 
            prodVM.PanTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select (int?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.PanTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select (int?)s.CrumbWaste).Sum() ?? 0;
            prodVM.PanTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.PanTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select (int?)s.Pack_Waste).Sum() ?? 0;
            prodVM.PanTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select (int?)s.ActualMix).Sum() ?? 0;
            prodVM.PanTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pan" select (double?)s.TotalProdMins).Sum() ?? 0;
            //prodVM.PanTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select s).Sum(s => s.Manning);
            prodVM.PanTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Pan" select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // ROLL 
            prodVM.RollTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select (int?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.RollTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select (int?)s.CrumbWaste).Sum() ?? 0;
            prodVM.RollTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.RollTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select (int?)s.Pack_Waste).Sum() ?? 0;
            prodVM.RollTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select (int?)s.ActualMix).Sum() ?? 0;
            prodVM.RollTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Roll" select (double?)s.TotalProdMins).Sum() ?? 0;
            //prodVM.RollTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo)  && s.Plant.Name == "Roll" select s).Sum(s => s.Manning);
            prodVM.RollTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Roll" select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // WHEATEN 
            prodVM.WheatenTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select (int?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.WheatenTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select (int?)s.CrumbWaste).Sum() ?? 0;
            prodVM.WheatenTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.WheatenTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select (int?)s.Pack_Waste).Sum() ?? 0;
            prodVM.WheatenTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select (int?)s.ActualMix).Sum() ?? 0;
            prodVM.WheatenTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Wheaten" select (double?)s.TotalProdMins).Sum() ?? 0;
            //prodVM.WheatenTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo)  && s.Plant.Name == "Wheaten" select s).Sum(s => s.Manning);
            prodVM.WheatenTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Wheaten" select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // SODA 
            prodVM.SodaTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select (int?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.SodaTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select (int?)s.CrumbWaste).Sum() ?? 0;
            prodVM.SodaTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.SodaTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select (int?)s.Pack_Waste).Sum() ?? 0;
            prodVM.SodaTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select (int?)s.ActualMix).Sum() ?? 0;
            prodVM.SodaTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Soda" select (double?)s.TotalProdMins).Sum() ?? 0;
            //prodVM.SodaTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Soda" select s).Sum(s => s.Manning);
            prodVM.SodaTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Soda" select (double?)s.TotalDownMins).Sum() ?? 0;
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
            prodVM.PancakesTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.PancakesTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.CrumbWaste).Sum() ?? 0;
            prodVM.PancakesTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.PancakesTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.Pack_Waste).Sum() ?? 0;
            prodVM.PancakesTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (int?)s.ActualMix).Sum() ?? 0;
            prodVM.PancakesTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Pancakes" select (double?)s.TotalProdMins).Sum() ?? 0;
            //prodVM.PancakesTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Manning);
            prodVM.PancakesTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Pancakes" select (double?)s.TotalDownMins).Sum() ?? 0;
            //-------------------------------------------------------------------------------------------------------------------
            // POTATO 
            prodVM.PotatoTotalComp = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select (int?)s.Cmp_Waste).Sum() ?? 0;
            prodVM.PotatoTotalCrumb = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select (int?)s.CrumbWaste).Sum() ?? 0;
            prodVM.PotatoTotalGenPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
            prodVM.PotatoTotalPack = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select (int?)s.Pack_Waste).Sum() ?? 0;
            prodVM.PotatoTotalMixes = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select (int?)s.ActualMix).Sum() ?? 0;
            prodVM.PotatoTotalTime = (from s in db.Productions where (s.Date > dt) && s.Plant.Name == "Potato" select (double?)s.TotalProdMins).Sum() ?? 0;
            //prodVM.PotatoTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Potato" select s).Sum(s => s.Manning);
            prodVM.PotatoTotalDowntime = (from s in db.Downtime where (s.Date > dt) && s.Plant.Name == "Potato" select (double?)s.TotalDownMins).Sum() ?? 0;


            return View(prodVM);
        }

        public ActionResult Dashboard(DateTime? DateFrom, DateTime? DateTo)
        {
            //--- Custom date range dashboard alternative ------
            var dash = new DashboardData();

            var prodVM = new ProductionVM();

            if (DateFrom.HasValue)
            {

                // Batch 
                prodVM.BatchTotalComp = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Batch" select (int?)s.Cmp_Waste).Sum() ?? 0;
               prodVM.BatchTotalCrumb = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Batch" select (int?)s.CrumbWaste).Sum() ?? 0;
                prodVM.BatchTotalGenPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Batch" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
                prodVM.BatchTotalPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Batch" select (int?)s.Pack_Waste).Sum() ?? 0;
                prodVM.BatchTotalMixes = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Batch" select (int?)s.ActualMix).Sum() ?? 0;
                prodVM.BatchTotalTime = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Batch" select (double?)s.TotalProdMins).Sum() ?? 0;
                //prodVM.BatchTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select s).Sum(s => s.Manning);
                prodVM.BatchTotalDowntime = (from s in db.Downtime where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Batch" select (double?)s.TotalDownMins).Sum() ?? 0;
                //-------------------------------------------------------------------------------------------------------------------
                // PAN 
                prodVM.PanTotalComp = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pan" select (int?)s.Cmp_Waste).Sum() ?? 0;
                prodVM.PanTotalCrumb = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pan" select (int?)s.CrumbWaste).Sum() ?? 0;
                prodVM.PanTotalGenPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pan" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
                prodVM.PanTotalPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pan" select (int?)s.Pack_Waste).Sum() ?? 0;
                prodVM.PanTotalMixes = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pan" select (int?)s.ActualMix).Sum() ?? 0;
                prodVM.PanTotalTime = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pan" select (double?)s.TotalProdMins).Sum() ?? 0;
                //prodVM.PanTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select s).Sum(s => s.Manning);
                prodVM.PanTotalDowntime = (from s in db.Downtime where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pan" select (double?)s.TotalDownMins).Sum() ?? 0;
                //-------------------------------------------------------------------------------------------------------------------
                // ROLL 
                prodVM.RollTotalComp = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Roll" select (int?)s.Cmp_Waste).Sum() ?? 0;
                prodVM.RollTotalCrumb = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Roll" select (int?)s.CrumbWaste).Sum() ?? 0;
                prodVM.RollTotalGenPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Roll" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
                prodVM.RollTotalPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Roll" select (int?)s.Pack_Waste).Sum() ?? 0;
                prodVM.RollTotalMixes = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Batch" select (int?)s.ActualMix).Sum() ?? 0;
                prodVM.RollTotalTime = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Roll" select (double?)s.TotalProdMins).Sum() ?? 0;
                //prodVM.RollTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo)  && s.Plant.Name == "Roll" select s).Sum(s => s.Manning);
                prodVM.RollTotalDowntime = (from s in db.Downtime where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Roll" select (double?)s.TotalDownMins).Sum() ?? 0;
                //-------------------------------------------------------------------------------------------------------------------
                // WHEATEN 
                prodVM.WheatenTotalComp = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Wheaten" select (int?)s.Cmp_Waste).Sum() ?? 0;
                prodVM.WheatenTotalCrumb = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Wheaten" select (int?)s.CrumbWaste).Sum() ?? 0;
                prodVM.WheatenTotalGenPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Wheaten" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
                prodVM.WheatenTotalPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Wheaten" select (int?)s.Pack_Waste).Sum() ?? 0;
                prodVM.WheatenTotalMixes = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Wheaten" select (int?)s.ActualMix).Sum() ?? 0;
                prodVM.WheatenTotalTime = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Wheaten" select (double?)s.TotalProdMins).Sum() ?? 0;
                //prodVM.WheatenTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo)  && s.Plant.Name == "Wheaten" select s).Sum(s => s.Manning);
                prodVM.WheatenTotalDowntime = (from s in db.Downtime where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Wheaten" select (double?)s.TotalDownMins).Sum() ?? 0;
                //-------------------------------------------------------------------------------------------------------------------
                // SODA 
                prodVM.SodaTotalComp = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Soda" select (int?)s.Cmp_Waste).Sum() ?? 0;
                prodVM.SodaTotalCrumb = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Soda" select (int?)s.CrumbWaste).Sum() ?? 0;
                prodVM.SodaTotalGenPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Soda" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
                prodVM.SodaTotalPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Soda" select (int?)s.Pack_Waste).Sum() ?? 0;
                prodVM.SodaTotalMixes = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Soda" select (int?)s.ActualMix).Sum() ?? 0;
                prodVM.SodaTotalTime = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Soda" select (double?)s.TotalProdMins).Sum() ?? 0;
                //prodVM.SodaTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Soda" select s).Sum(s => s.Manning);
                prodVM.SodaTotalDowntime = (from s in db.Downtime where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Soda" select (double?)s.TotalDownMins).Sum() ?? 0;
                //-------------------------------------------------------------------------------------------------------------------
                // PANCAKES 
                prodVM.PancakesTotalComp = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pancakes" select (int?)s.Cmp_Waste).Sum() ?? 0;
                prodVM.PancakesTotalCrumb = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pancakes" select (int?)s.CrumbWaste).Sum() ?? 0;
                prodVM.PancakesTotalGenPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pancakes" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
                prodVM.PancakesTotalPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pancakes" select (int?)s.Pack_Waste).Sum() ?? 0;
                prodVM.PancakesTotalMixes = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pancakes" select (int?)s.ActualMix).Sum() ?? 0;
                prodVM.PancakesTotalTime = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.TotalProdMins).Sum() ?? 0;
                //prodVM.PancakesTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Manning);
                prodVM.PancakesTotalDowntime = (from s in db.Downtime where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Pancakes" select (double?)s.TotalDownMins).Sum() ?? 0;
                //-------------------------------------------------------------------------------------------------------------------
                //// PANCAKES 
                //prodVM.PancakesTotalComp = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select (int?)s.Cmp_Waste).Sum() ?? 0;
                //prodVM.PancakesTotalCrumb = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select (int?)s.CrumbWaste).Sum() ?? 0;
                //prodVM.PancakesTotalGenPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
                //prodVM.PancakesTotalPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select (int?)s.Pack_Waste).Sum() ?? 0;
                //prodVM.PancakesTotalMixes = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select (int?)s.ActualMix).Sum() ?? 0;
                //prodVM.PancakesTotalTime = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select (double?)s.TotalProdMins).Sum() ?? 0;
                ////prodVM.PancakesTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Pancakes" select s).Sum(s => s.Manning);
                //prodVM.PancakesTotalDowntime = (from s in db.Downtime where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Batch" select (double?)s.TotalDownMins).Sum() ?? 0;
                //-------------------------------------------------------------------------------------------------------------------
                // POTATO 
                prodVM.PotatoTotalComp = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Potato" select (int?)s.Cmp_Waste).Sum() ?? 0;
                prodVM.PotatoTotalCrumb = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Potato" select (int?)s.CrumbWaste).Sum() ?? 0;
                prodVM.PotatoTotalGenPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Potato" select (int?)s.Gen_Pack_Waste).Sum() ?? 0;
                prodVM.PotatoTotalPack = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Potato" select (int?)s.Pack_Waste).Sum() ?? 0;
                prodVM.PotatoTotalMixes = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Potato" select (int?)s.ActualMix).Sum() ?? 0;
                prodVM.PotatoTotalTime = (from s in db.Productions where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Potato" select (double?)s.TotalProdMins).Sum() ?? 0;
                //prodVM.PotatoTotalManning = (from s in db.Productions where (s.Date >= DateFrom && s.Date < DateTo) && s.Plant.Name == "Potato" select s).Sum(s => s.Manning);
                prodVM.PotatoTotalDowntime = (from s in db.Downtime where (s.Date >= DateFrom && s.Date <= DateTo) && s.Plant.Name == "Potato" select (double?)s.TotalDownMins).Sum() ?? 0;



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