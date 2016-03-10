using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuickCode.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PagedList;

namespace QuickCode.Controllers
{
    public class ProductionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private User user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        protected UserManager<User> UserManager { get; set; }
        /// <returns></returns>
        // GET: Productions
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, DateTime? DateFrom, DateTime? DateTo)
        {
            //------------- sorting parameters ---------------
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";       
            ViewBag.ShiftSortParm = String.IsNullOrEmpty(sortOrder) ? "shift_desc" : "";
            ViewBag.PlantSortParm = String.IsNullOrEmpty(sortOrder) ? "Plant_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            // sets the current filter in the table to searchString:
            ViewBag.CurrentFilter = searchString;

            /*--- Allow access for the process owner or plant managers ----*/
            if (user.RoleName.Equals("ProcessOwner") || user.RoleName.Equals("Manager")) { /*var productions = db.Productions.Include(p => p.Plant).Include(p => p.Shift).Include(p => p.User);*/
                var productions = from s in db.Productions
                                  orderby s.StartDate descending                                
                                  select s;

                //---Search for record by Date -------
                if (DateFrom.HasValue)
                {
                    productions = db.Productions.Where(s => s.StartDate >= DateFrom && s.EndDate <= DateTo).OrderByDescending(s => s.StartDate).ThenByDescending(s => s.ProductionID);

                }


                //---SEARCH FOR WHATEVER YOU FEEL LIKE HERE -------
                if (!String.IsNullOrEmpty(searchString))
                {
                    productions = productions.Where(s => s.User.UserName.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Shift.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Plant.Name.ToUpper().Contains(searchString.ToUpper()) 
                                                      
                                                      ).OrderByDescending(s => s.StartDate).ThenByDescending(s => s.ProductionID);

                }

               
               

                // SWITCH STATEMENT --- WHEN USER SELECTS TO SORT DATA
                switch (sortOrder)
                {
                    case "user_desc":
                        productions = productions.OrderByDescending(s => s.User);
                        break;
                    case "shift_desc":
                        productions = productions.OrderByDescending(s => s.Shift);
                        break;
                    case "Plant_desc":
                        productions = productions.OrderByDescending(s => s.Plant);
                        break;
                    case "Date":
                        productions = productions.OrderBy(s => s.StartDate);
                        break;
                    case "date_desc":
                        productions = productions.OrderBy(s => s.EndDate);
                        break;
                    default:
                        productions = productions.OrderByDescending(s => s.StartDate);
                        break;
                }

                int pageSize = 20;
                int pageNumber = (page ?? 1);
                return View(productions.OrderByDescending(s => s.StartDate).ToPagedList(pageNumber, pageSize));
                //return View(productions.ToList());
            }
           
            else {
            
            var productions = from s in db.Productions
                              where s.UserID == user.Id.ToString()
                              orderby s.StartDate descending
                              select s;

                if (DateFrom.HasValue)
                {
                    productions = db.Productions.Where(s => s.StartDate >= DateFrom && s.EndDate <= DateTo).OrderByDescending(s => s.StartDate).ThenByDescending(s => s.ProductionID);

                }

                // ---SEARCH FOR WHATEVER YOU FEEL LIKE HERE -------
                if (!String.IsNullOrEmpty(searchString))
                {
                    productions = productions.Where(s => s.User.UserName.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Shift.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Plant.Name.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(s => s.StartDate).ThenByDescending(s => s.ProductionID);

                }
             
                switch (sortOrder)
                {
                    case "user_desc":
                        productions = productions.OrderByDescending(s => s.User);
                        break;
                    case "shift_desc":
                        productions = productions.OrderByDescending(s => s.Shift);
                        break;
                    case "Plant_desc":
                        productions = productions.OrderByDescending(s => s.Plant);
                        break;
                    case "Date":
                        productions = productions.OrderBy(s => s.StartDate);
                        break;
                    case "date_desc":
                        productions = productions.OrderBy(s => s.EndDate);
                        break;
                    default:
                        productions = productions.OrderBy(s => s.StartDate);
                        break;
                }

                int pageSize = 20;
                int pageNumber = (page ?? 1);
                return View(productions.OrderByDescending(s => s.StartDate).ToPagedList(pageNumber, pageSize));
                //return View(productions.ToList());
            }
           
        }

        // GET: Productions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Production production = db.Productions.Find(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }

        // GET: Productions/Create
        public ActionResult Create()
        {
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name");
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Productions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(double manning, DateTime MyDate, DateTime EndDate, String notes, String qualityIssues, double box1, double box2, double box3, [Bind(Include = "ProductionID,UserID,ShiftID,PlantID,ActualMix,CrumbWaste,Cmp_Waste,Pack_Waste,Gen_Pack_Waste,Date,TotalWaste,TotalProdMins")] Production production)
        {
            // box1 = std box2 = agency box3 = operator
            //int std = Int32.Parse(box1);
            //int agency = Int32.Parse(box2);
            //int op = Int32.Parse(box3);
            double std = box1;
            double agency = box2;
            double op = box3;

            // Date config:
            DateTime edt = EndDate;
            DateTime dt = MyDate;
            string a = dt.ToString("HH:mm");
            string b = edt.ToString("HH:mm");
            DateTime st = DateTime.Parse(a); /*Convert.ToDateTime(startTime);*/
            DateTime et = DateTime.Parse(b);
            DateTime nt = dt.Date;

            double sum = production.Cmp_Waste + production.CrumbWaste + production.Pack_Waste + production.Gen_Pack_Waste;
            //TimeSpan span = (production.EndTime - production.StartTime);
            TimeSpan span = MyDate - EndDate;
            double totalMins = span.TotalMinutes;
            totalMins = Math.Abs(totalMins);
            //int mann = Int32.Parse(manning);
            //String mann = manning;
            double mann = box1 + box2 + box3;
           
            try
            {

                if (ModelState.IsValid)
                {
                    production.sDate = nt;
                    production.StartDate = MyDate;
                    production.EndDate = EndDate;
                    production.StartTime = st;
                    production.EndTime = et;
                    production.TotalWaste = sum;
                    production.TotalProdMins = totalMins;
                    production.UserID = user.Id;
                    production.Manning = mann;
                    production.StdManning = std;
                    production.AgencyManning = agency;
                    production.OpManning = op;
                    production.Notes = notes;
                    production.QualityIssues = qualityIssues;
                    db.Productions.Add(production);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //User user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.UserID = user.Id.ToString();
            ViewBag.currentUser = user.UserName;
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", production.PlantID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", production.ShiftID);
            //ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", production.UserID);
            return View(production);
        }

        // GET: Productions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Production production = db.Productions.Find(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", production.PlantID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", production.ShiftID);
            //ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", production.UserID);
            return View(production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DateTime MyDate, DateTime EndDate, [Bind(Include = "ProductionID,UserID,ShiftID,PlantID,ActualMix,CrumbWaste,Cmp_Waste,Pack_Waste,Gen_Pack_Waste,StdManning,OpManning,AgencyManning,QualityIssues,Date,TotalWaste,StartTime,EndTime")] Production production)
        {

            // Date config:
            DateTime edt = EndDate;
            DateTime dt = MyDate;
            string a = dt.ToString("HH:mm");
            string b = edt.ToString("HH:mm");
            DateTime st = DateTime.Parse(a); /*Convert.ToDateTime(startTime);*/
            DateTime et = DateTime.Parse(b);
            double sum = production.Cmp_Waste + production.CrumbWaste + production.Pack_Waste + production.Gen_Pack_Waste;
            //TimeSpan span = (production.EndTime - production.StartTime);
            TimeSpan span = MyDate - EndDate;
            double totalMins = span.TotalMinutes;
            totalMins = Math.Abs(totalMins);
            double manning = production.OpManning + production.StdManning + production.AgencyManning;
            //int mann = Int32.Parse(manning);
            User user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            
            try
            {
                if (ModelState.IsValid)
                {
                    production.StartDate = MyDate;
                    production.EndDate = EndDate;
                    production.StartTime = st;
                    production.EndTime = et;
                    production.UserID = user.Id;
                    production.TotalWaste = sum;
                    production.TotalProdMins = totalMins;
                    production.Manning = manning;
                    db.Entry(production).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //User user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.UserID = user.Id;
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", production.PlantID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", production.ShiftID);
            //ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", production.UserID);
            return View(production);
        }

        // GET: Productions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Production production = db.Productions.Find(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Production production = db.Productions.Find(id);
                db.Productions.Remove(production);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                // uncomment dex and log error. 
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
