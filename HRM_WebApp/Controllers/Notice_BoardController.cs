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
    public class Notice_BoardController : Controller
    {
        private HRM_databaseEntities1 db = new HRM_databaseEntities1();

        // GET: Notice_Board
        public ActionResult Index()
        {
            var notice_Board = db.Notice_Board.Include(n => n.Departament);
            return View(notice_Board.ToList());
        }

        // GET: Notice_Board/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice_Board notice_Board = db.Notice_Board.Find(id);
            if (notice_Board == null)
            {
                return HttpNotFound();
            }
            return View(notice_Board);
        }

        // GET: Notice_Board/Create
        public ActionResult Create()
        {
            ViewBag.dep_id = db.Departaments.ToList();
            return View();
        }

        // POST: Notice_Board/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,notice_title,notice_content,notice_status,dep_id")] Notice_Board notice_Board)
        {
            
            if (ModelState.IsValid)
            {
                if (notice_Board.dep_id == 0)
                {
                    notice_Board.dep_id = null;
                }
                db.Notice_Board.Add(notice_Board);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dep_id = new SelectList(db.Departaments, "id", "depart_name", notice_Board.dep_id);
            return View(notice_Board);
        }

        // GET: Notice_Board/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice_Board notice_Board = db.Notice_Board.Find(id);
            if (notice_Board == null)
            {
                return HttpNotFound();
            }
            ViewBag.dep_id = new SelectList(db.Departaments, "id", "depart_name", notice_Board.dep_id);
            return View(notice_Board);
        }

        // POST: Notice_Board/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,notice_title,notice_content,notice_status,dep_id")] Notice_Board notice_Board)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notice_Board).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dep_id = new SelectList(db.Departaments, "id", "depart_name", notice_Board.dep_id);
            return View(notice_Board);
        }

        // GET: Notice_Board/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice_Board notice_Board = db.Notice_Board.Find(id);
            if (notice_Board == null)
            {
                return HttpNotFound();
            }
            return View(notice_Board);
        }

        // POST: Notice_Board/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notice_Board notice_Board = db.Notice_Board.Find(id);
            db.Notice_Board.Remove(notice_Board);
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
