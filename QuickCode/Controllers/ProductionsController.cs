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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            ViewBag.CurrentFilter = searchString;

            if (user.Roles.Equals("Administrator")) { var productions = db.Productions.Include(p => p.Plant).Include(p => p.Shift).Include(p => p.User);

                // ---SEARCH FOR WHATEVER YOU FEEL LIKE HERE -------
                if (!String.IsNullOrEmpty(searchString))
                {
                    productions = productions.Where(s => s.User.UserName.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Shift.Name.ToUpper().Contains(searchString.ToUpper()) ||                                        
                                                        s.Plant.Name.ToUpper().Contains(searchString.ToUpper()));

                }

                // SWITCH STATEMENT --- WHEN USER SELECTS TO SORT DATA
                switch (sortOrder)
                {
                    case "name_desc":
                        productions = productions.OrderByDescending(s => s.User);
                        break;
                    case "Date":
                        productions = productions.OrderBy(s => s.Date);
                        break;
                    case "date_desc":
                        productions = productions.OrderByDescending(s => s.Date);
                        break;
                    default:
                        productions = productions.OrderBy(s => s.Date);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(productions.ToPagedList(pageNumber, pageSize));
                //return View(productions.ToList());
            }
            else {
            
            var productions = from s in db.Productions
                              where s.UserID == user.Id.ToString()
                              select s;

                // ---SEARCH FOR WHATEVER YOU FEEL LIKE HERE -------
                if (!String.IsNullOrEmpty(searchString))
                {
                    productions = productions.Where(s => s.User.UserName.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Shift.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Plant.Name.ToUpper().Contains(searchString.ToUpper()));

                }

                // SWITCH STATEMENT --- WHEN USER SELECTS TO SORT DATA
                switch (sortOrder)
                {
                    case "name_desc":
                        productions = productions.OrderByDescending(s => s.User);
                        break;
                    case "Date":
                        productions = productions.OrderBy(s => s.Date);
                        break;
                    case "date_desc":
                        productions = productions.OrderByDescending(s => s.Date);
                        break;
                    default:
                        productions = productions.OrderBy(s => s.Date);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(productions.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create(String manning, String MyDate,String notes, String box1, String box2, String box3, [Bind(Include = "ProductionID,UserID,ShiftID,PlantID,StartTime,EndTime,ActualMix,CrumbWaste,Cmp_Waste,Pack_Waste,Gen_Pack_Waste,Date,TotalWaste,TotalProdMins")] Production production)
        {
            // box1 = std box2 = agency box3 = operator
            int std = Int32.Parse(box1);
            int agency = Int32.Parse(box2);
            int op = Int32.Parse(box3);
            DateTime dt = Convert.ToDateTime(MyDate);
            int sum = production.Cmp_Waste + production.CrumbWaste;
            TimeSpan span = (production.EndTime - production.StartTime);
            double totalMins = span.TotalMinutes;
            //int mann = Int32.Parse(manning);
            String mann = manning;
           
            try
            {

                if (ModelState.IsValid)
                {
                    production.Date = dt;
                    production.TotalWaste = sum;
                    production.TotalProdMins = totalMins;
                    production.UserID = user.Id;
                    production.Manning = mann;
                    production.StdManning = std;
                    production.AgencyManning = agency;
                    production.OpManning = op;
                    production.Notes = notes;
                    db.Productions.Add(production);
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
        public ActionResult Edit(String MyDate, [Bind(Include = "ProductionID,UserID,ShiftID,PlantID,StartTime,EndTime,ActualMix,CrumbWaste,Cmp_Waste,Pack_Waste,Gen_Pack_Waste,StdManning,OpManning,AgencyManning,Manning,Date,TotalWaste,TotalProdMins")] Production production)
        {
            DateTime dt = Convert.ToDateTime(MyDate);
            int sum = production.Cmp_Waste + production.CrumbWaste;
            TimeSpan span = (production.EndTime - production.StartTime);
            double totalMins = span.TotalMinutes;
            User user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            
            try
            {
                if (ModelState.IsValid)
                {
                    production.Date = dt;
                    production.UserID = user.Id;
                    production.TotalWaste = sum;
                    production.TotalProdMins = totalMins;
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
