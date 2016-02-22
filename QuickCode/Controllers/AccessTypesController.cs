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
    public class AccessTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccessTypes
        public ActionResult Index()
        {
            return View(db.AccessTypes.ToList());
        }

        // GET: AccessTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessTypes accessTypes = db.AccessTypes.Find(id);
            if (accessTypes == null)
            {
                return HttpNotFound();
            }
            return View(accessTypes);
        }

        // GET: AccessTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccessID,Name,Description")] AccessTypes accessTypes)
        {
            if (ModelState.IsValid)
            {
                db.AccessTypes.Add(accessTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accessTypes);
        }

        // GET: AccessTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessTypes accessTypes = db.AccessTypes.Find(id);
            if (accessTypes == null)
            {
                return HttpNotFound();
            }
            return View(accessTypes);
        }

        // POST: AccessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccessID,Name,Description")] AccessTypes accessTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accessTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accessTypes);
        }

        // GET: AccessTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessTypes accessTypes = db.AccessTypes.Find(id);
            if (accessTypes == null)
            {
                return HttpNotFound();
            }
            return View(accessTypes);
        }

        // POST: AccessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessTypes accessTypes = db.AccessTypes.Find(id);
            db.AccessTypes.Remove(accessTypes);
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
