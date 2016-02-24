using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuickCode.Models;

namespace QuickCode.Controllers
{
    public class FiscalWeeksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FiscalWeeks
        public ActionResult Index()
        {
            return View(db.FiscalWeeks.ToList());
        }

        // GET: FiscalWeeks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiscalWeek fiscalWeek = db.FiscalWeeks.Find(id);
            if (fiscalWeek == null)
            {
                return HttpNotFound();
            }
            return View(fiscalWeek);
        }

        // GET: FiscalWeeks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FiscalWeeks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FiscalWeekID,FiscalYear,WeekName,WeekQuarter,WeekMonth,WeekNumber,WeekStart")] FiscalWeek fiscalWeek)
        {
            if (ModelState.IsValid)
            {
                db.FiscalWeeks.Add(fiscalWeek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fiscalWeek);
        }

        // GET: FiscalWeeks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiscalWeek fiscalWeek = db.FiscalWeeks.Find(id);
            if (fiscalWeek == null)
            {
                return HttpNotFound();
            }
            return View(fiscalWeek);
        }

        // POST: FiscalWeeks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FiscalWeekID,FiscalYear,WeekName,WeekQuarter,WeekMonth,WeekNumber,WeekStart")] FiscalWeek fiscalWeek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiscalWeek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fiscalWeek);
        }

        // GET: FiscalWeeks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiscalWeek fiscalWeek = db.FiscalWeeks.Find(id);
            if (fiscalWeek == null)
            {
                return HttpNotFound();
            }
            return View(fiscalWeek);
        }

        // POST: FiscalWeeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FiscalWeek fiscalWeek = db.FiscalWeeks.Find(id);
            db.FiscalWeeks.Remove(fiscalWeek);
            db.SaveChanges();
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
