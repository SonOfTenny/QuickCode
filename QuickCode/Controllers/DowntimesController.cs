using System;
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
    public class DowntimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private User user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        // GET: Downtimes
        
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, DateTime? DateFrom, DateTime? DateTo)
        {
            //------------- sorting parameters ---------------
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";
            ViewBag.DownSortParm = String.IsNullOrEmpty(sortOrder) ? "Down_desc" : "";
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
            if (user.RoleName.Equals("ProcessOwner") || user.RoleName.Equals("Manager"))
            {
                //var downtime = db.Downtime.Include(d => d.DowntimeType).Include(d => d.Plant).Include(d => d.Shift).Include(d => d.User);

                var downtime = from s in db.Downtime 
                               orderby s.StartDate  descending                         
                               select s;
                // Search by date because you're a cool dude!
                if (DateFrom.HasValue)
                {
                    downtime = db.Downtime.Where(s => s.StartDate >= DateFrom && s.EndDate <= DateTo).OrderByDescending(s => s.StartDate).ThenByDescending(s => s.DowntimeID);
                }

                // ---SEARCH FOR WHATEVER YOU FEEL LIKE HERE -------
                if (!String.IsNullOrEmpty(searchString))
                {
                    downtime = downtime.Where(s => s.User.UserName.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Shift.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.DowntimeType.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Plant.Name.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(s => s.StartDate).ThenByDescending(s => s.DowntimeID);
                }
                    // SWITCH STATEMENT --- WHEN USER SELECTS TO SORT DATA
                    switch (sortOrder)
                {
                    case "user_desc":
                        downtime = downtime.OrderByDescending(s => s.User);
                        break;
                    case "shift_desc":
                        downtime = downtime.OrderByDescending(s => s.Shift);
                        break;
                    case "Plant_desc":
                        downtime = downtime.OrderByDescending(s => s.Plant);
                        break;
                    case "Down_desc":
                        downtime = downtime.OrderByDescending(s => s.DowntimeType);
                        break;
                    case "Date":
                        downtime = downtime.OrderBy(s => s.StartDate);
                        break;
                    case "date_desc":
                        downtime = downtime.OrderBy(s => s.EndDate);
                        break;
                    default:                 
                        downtime = downtime.OrderByDescending(s => s.StartDate);
                        break;
                }

                int pageSize = 15;
                int pageNumber = (page ?? 1);
                return View(downtime.OrderByDescending(s => s.StartDate).ToPagedList(pageNumber, pageSize));
                //return View(downtime.ToList());

            }
            else // not admin do the following
            {
                
                var downtime = from s in db.Downtime
                                   where s.UserID == user.Id.ToString()
                                   orderby s.StartDate descending
                                   select s;

        
                if (DateFrom.HasValue)
                {
                   downtime = db.Downtime.Where(s => s.StartDate >= DateFrom && s.EndDate <= DateTo).OrderByDescending(s => s.StartDate).ThenByDescending(s => s.DowntimeID);

                }
               

                // ---SEARCH FOR WHATEVER YOU FEEL LIKE HERE -------
                if (!String.IsNullOrEmpty(searchString))
                {
                    downtime = downtime.Where(s => s.User.UserName.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Shift.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.DowntimeType.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                                        s.Plant.Name.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(s => s.StartDate).ThenByDescending(s => s.DowntimeID);
                }

                // SWITCH STATEMENT --- WHEN USER SELECTS TO SORT DATA
                switch (sortOrder)
                {
                    case "user_desc":
                        downtime = downtime.OrderByDescending(s => s.User);
                        break;
                    case "shift_desc":
                        downtime = downtime.OrderByDescending(s => s.Shift);
                        break;
                    case "Plant_desc":
                        downtime = downtime.OrderByDescending(s => s.Plant);
                        break;
                    case "Down_desc":
                        downtime = downtime.OrderByDescending(s => s.DowntimeType);
                        break;
                    case "Date":
                        downtime = downtime.OrderBy(s => s.StartDate);
                        break;
                    case "date_desc":
                        downtime = downtime.OrderBy(s => s.EndDate);
                        break;
                    default:
                        downtime = downtime.OrderByDescending(s => s.StartDate);
                        break;
                }

                int pageSize = 20;
                int pageNumber = (page ?? 1);
                return View(downtime.OrderByDescending(s => s.StartDate).ToPagedList(pageNumber, pageSize));
                //return View(downtime.ToList());

            }                      
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
        public ActionResult Create(DateTime MyDate, DateTime EndDate, String reason, String action, [Bind(Include = "DowntimeID,UserID,ShiftID,DowntimeTypeID,PlantID,Date,TotalDownMins")] Downtime downtime)
        {
            // Date config:
            DateTime edt = EndDate;
            DateTime dt = MyDate;
            string a = dt.ToString("HH:mm");
            string b = edt.ToString("HH:mm");
            DateTime st = DateTime.Parse(a); /*Convert.ToDateTime(startTime);*/
            DateTime et = DateTime.Parse(b);
            //TimeSpan span = (downtime.EndTime - downtime.StartTime);
            TimeSpan span = MyDate - EndDate;
            double totalMins = span.TotalMinutes;
            totalMins = Math.Abs(totalMins);
            try
            {

                if (ModelState.IsValid)
                {
                    downtime.StartDate = MyDate;
                    downtime.EndDate = EndDate;
                    downtime.StartTime = st;
                    downtime.EndTime = et;
                    downtime.TotalDownMins = totalMins;
                    downtime.UserID = user.Id;
                    downtime.Reason = reason;
                    downtime.Action = action;
                    db.Downtime.Add(downtime);
                    db.SaveChanges();
                    return RedirectToAction("Create");
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
        public ActionResult Edit(DateTime MyDate, DateTime EndDate, DateTime startTime, DateTime endTime, [Bind(Include = "DowntimeID,UserID,ShiftID,DowntimeTypeID,PlantID,Reason,Action,Date,TotalDownMins")] Downtime downtime)
        {
            // Date config:
            DateTime edt = EndDate;
            DateTime dt = MyDate;
            string a = dt.ToString("HH:mm");
            string b = edt.ToString("HH:mm");
            DateTime st = DateTime.Parse(a); /*Convert.ToDateTime(startTime);*/
            DateTime et = DateTime.Parse(b);
            //TimeSpan span = (downtime.EndTime - downtime.StartTime);
            TimeSpan span = MyDate - EndDate;
            double totalMins = span.TotalMinutes;
            totalMins = Math.Abs(totalMins);
            try
            {
                if (ModelState.IsValid)
                {
                    downtime.StartDate = MyDate;
                    downtime.EndDate = EndDate;
                    downtime.StartTime = st;
                    downtime.EndTime = et;
                    downtime.TotalDownMins = totalMins;
                    downtime.UserID = user.Id;
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
