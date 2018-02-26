using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRM_WebApp.Models;

namespace HRM_WebApp.Controllers
{
    public class AttendencesController : Controller
    {
        private HRM_databaseEntities1 db = new HRM_databaseEntities1();

        // GET: Attendences
        public ActionResult Index()
        {
            var attendences = db.Attendences.Include(a => a.Employee).Include(a => a.Leave_type);
            return View(attendences.ToList());
        }

        // GET: Attendences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: Attendences/Create
        public ActionResult Create()
        {
            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname");
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name");
            return View();
        }

        // POST: Attendences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,atten_emp_id,atten_status,atten_leave_type_id,atten_date,atten_reason")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Attendences.Add(attendence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname", attendence.atten_emp_id);
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name", attendence.atten_leave_type_id);
            return View(attendence);
        }

        // GET: Attendences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname", attendence.atten_emp_id);
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name", attendence.atten_leave_type_id);
            return View(attendence);
        }

        // POST: Attendences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,atten_emp_id,atten_status,atten_leave_type_id,atten_date,atten_reason")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.atten_emp_id = new SelectList(db.Employees, "id", "emp_fullname", attendence.atten_emp_id);
            ViewBag.atten_leave_type_id = new SelectList(db.Leave_type, "id", "type_name", attendence.atten_leave_type_id);
            return View(attendence);
        }

        // GET: Attendences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: Attendences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendence attendence = db.Attendences.Find(id);
            db.Attendences.Remove(attendence);
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
