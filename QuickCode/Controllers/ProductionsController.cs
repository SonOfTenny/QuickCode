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

namespace QuickCode.Controllers
{
    public class ProductionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private User user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        protected UserManager<User> UserManager { get; set; }
        /// <returns></returns>
        // GET: Productions
        public ActionResult Index()
        {
            var productions = db.Productions.Include(p => p.Plant).Include(p => p.Shift).Include(p => p.User);
            return View(productions.ToList());
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
        public ActionResult Create([Bind(Include = "ProductionID,UserID,ShiftID,PlantID,StartTime,EndTime,ActualMix,CrumbWaste,Cmp_Waste,Manning,Date,TotalWaste,TotalProdMins")] Production production)
        {
            int sum = production.Cmp_Waste + production.CrumbWaste;
            TimeSpan span = (production.EndTime - production.StartTime);
            double totalMins = span.TotalMinutes;
         
            try
            {

                if (ModelState.IsValid)
                {
                    production.TotalWaste = sum;
                    production.TotalProdMins = totalMins;
                    production.UserID = user.Id;
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
        public ActionResult Edit([Bind(Include = "ProductionID,UserID,ShiftID,PlantID,StartTime,EndTime,ActualMix,CrumbWaste,Cmp_Waste,Manning,Date,TotalWaste,TotalProdMins")] Production production)
        {
            int sum = production.Cmp_Waste + production.CrumbWaste;
            TimeSpan span = (production.EndTime - production.StartTime);
            double totalMins = span.TotalMinutes;
            User user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            try
            {
                if (ModelState.IsValid)
                {
                    // production.UserID = user.Id;
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
