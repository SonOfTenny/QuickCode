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
    public class DowntimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private User user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        // GET: Downtimes
        public ActionResult Index()
        {
            var downtime = db.Downtime.Include(d => d.DowntimeType).Include(d => d.Plant).Include(d => d.Shift).Include(d => d.User);
            return View(downtime.ToList());
        }

        // GET: Downtimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Downtime downtime = db.Downtime.Find(id);
            if (downtime == null)
            {
                return HttpNotFound();
            }
            return View(downtime);
        }

        // GET: Downtimes/Create
        public ActionResult Create()
        {
            ViewBag.DowntimeTypeID = new SelectList(db.DowntimeTypes, "DowntimeTypeID", "Name");
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name");
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Downtimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DowntimeID,UserID,ShiftID,DowntimeTypeID,PlantID,StartTime,EndTime,Reason,Action,Date,TotalDownMins")] Downtime downtime)
        {
            TimeSpan span = (downtime.EndTime - downtime.StartTime);
            double totalMins = span.TotalMinutes;
            try
            {

                if (ModelState.IsValid)
                {
                    downtime.TotalDownMins = totalMins;
                    downtime.UserID = user.Id;
                    db.Downtime.Add(downtime);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.DowntimeTypeID = new SelectList(db.DowntimeTypes, "DowntimeTypeID", "Name", downtime.DowntimeTypeID);
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", downtime.PlantID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", downtime.ShiftID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", downtime.UserID);
            return View(downtime);
        }

        // GET: Downtimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Downtime downtime = db.Downtime.Find(id);
            if (downtime == null)
            {
                return HttpNotFound();
            }
            ViewBag.DowntimeTypeID = new SelectList(db.DowntimeTypes, "DowntimeTypeID", "Name", downtime.DowntimeTypeID);
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", downtime.PlantID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", downtime.ShiftID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", downtime.UserID);
            return View(downtime);
        }

        // POST: Downtimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DowntimeID,UserID,ShiftID,DowntimeTypeID,PlantID,StartTime,EndTime,Reason,Action,Date,TotalDownMins")] Downtime downtime)
        {
            TimeSpan span = (downtime.EndTime - downtime.StartTime);
            double totalMins = span.TotalMinutes;
            try
            {
                if (ModelState.IsValid)
                {
                    downtime.TotalDownMins = totalMins;
                    //downtime.UserID = user.Id;
                    db.Entry(downtime).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.DowntimeTypeID = new SelectList(db.DowntimeTypes, "DowntimeTypeID", "Name", downtime.DowntimeTypeID);
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "Name", downtime.PlantID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", downtime.ShiftID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", downtime.UserID);
            return View(downtime);
        }

        // GET: Downtimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Downtime downtime = db.Downtime.Find(id);
            if (downtime == null)
            {
                return HttpNotFound();
            }
            return View(downtime);
        }

        // POST: Downtimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                Downtime downtime = db.Downtime.Find(id);
                db.Downtime.Remove(downtime);
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
