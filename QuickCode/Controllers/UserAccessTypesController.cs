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
    public class UserAccessTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserAccessTypes
        public ActionResult Index()
        {
            var userAccessTypes = db.UserAccessTypes.Include(u => u.Access).Include(u => u.User);
            return View(userAccessTypes.ToList());
        }

        // GET: UserAccessTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccessTypes userAccessTypes = db.UserAccessTypes.Find(id);
            if (userAccessTypes == null)
            {
                return HttpNotFound();
            }
            return View(userAccessTypes);
        }

        // GET: UserAccessTypes/Create
        public ActionResult Create()
        {
            ViewBag.AccessID = new SelectList(db.AccessTypes, "AccessID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserAccessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserAccessID,UserID,AccessID")] UserAccessTypes userAccessTypes)
        {
            if (ModelState.IsValid)
            {
                db.UserAccessTypes.Add(userAccessTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccessID = new SelectList(db.AccessTypes, "AccessID", "Name", userAccessTypes.AccessID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", userAccessTypes.UserID);
            return View(userAccessTypes);
        }

        // GET: UserAccessTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccessTypes userAccessTypes = db.UserAccessTypes.Find(id);
            if (userAccessTypes == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccessID = new SelectList(db.AccessTypes, "AccessID", "Name", userAccessTypes.AccessID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", userAccessTypes.UserID);
            return View(userAccessTypes);
        }

        // POST: UserAccessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserAccessID,UserID,AccessID")] UserAccessTypes userAccessTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAccessTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccessID = new SelectList(db.AccessTypes, "AccessID", "Name", userAccessTypes.AccessID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", userAccessTypes.UserID);
            return View(userAccessTypes);
        }

        // GET: UserAccessTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccessTypes userAccessTypes = db.UserAccessTypes.Find(id);
            if (userAccessTypes == null)
            {
                return HttpNotFound();
            }
            return View(userAccessTypes);
        }

        // POST: UserAccessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccessTypes userAccessTypes = db.UserAccessTypes.Find(id);
            db.UserAccessTypes.Remove(userAccessTypes);
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
