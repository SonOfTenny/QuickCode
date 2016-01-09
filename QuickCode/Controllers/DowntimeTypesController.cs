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
    public class DowntimeTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DowntimeTypes
        public ActionResult Index()
        {
            return View(db.DowntimeTypes.ToList());
        }

        // GET: DowntimeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DowntimeType downtimeType = db.DowntimeTypes.Find(id);
            if (downtimeType == null)
            {
                return HttpNotFound();
            }
            return View(downtimeType);
        }

        // GET: DowntimeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DowntimeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DowntimeTypeID,Name")] DowntimeType downtimeType)
        {
            if (ModelState.IsValid)
            {
                db.DowntimeTypes.Add(downtimeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(downtimeType);
        }

        // GET: DowntimeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DowntimeType downtimeType = db.DowntimeTypes.Find(id);
            if (downtimeType == null)
            {
                return HttpNotFound();
            }
            return View(downtimeType);
        }

        // POST: DowntimeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DowntimeTypeID,Name")] DowntimeType downtimeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(downtimeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(downtimeType);
        }

        // GET: DowntimeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DowntimeType downtimeType = db.DowntimeTypes.Find(id);
            if (downtimeType == null)
            {
                return HttpNotFound();
            }
            return View(downtimeType);
        }

        // POST: DowntimeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DowntimeType downtimeType = db.DowntimeTypes.Find(id);
            db.DowntimeTypes.Remove(downtimeType);
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
