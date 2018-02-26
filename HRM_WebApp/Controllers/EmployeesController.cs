using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRM_WebApp.Models;

namespace HRM_WebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private HRM_databaseEntities1 db = new HRM_databaseEntities1();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Departament).Include(e => e.Designation).Include(e => e.Gender);
            
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name");
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name");
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,emp_fullname,emp_fathername,emp_dateof_birth,emp_gender_id,emp_phone,emp_address,emp_photo,emp_email,emp_password,emp_dep_id,emp_desig_id,emp_date_of_joining,emp_exit_date,emp_work_status,emp_salary,emp_resume,emp_offer_letter,emp_joining_letter,emp_contact_and_argue,emp_ID_proof")] Employee employee, HttpPostedFileBase emp_photo, HttpPostedFileBase emp_resume, HttpPostedFileBase emp_offer_letter, HttpPostedFileBase emp_joining_letter, HttpPostedFileBase emp_contact_and_argue, HttpPostedFileBase emp_ID_proof)
        {
            if (ModelState.IsValid)
            { 
                var g = Guid.NewGuid(); 
                if (emp_photo != null)
                {
                    if (emp_photo.ContentLength / 1024 > 5 && !emp_photo.ContentType.Contains("image"))
                    {
                        return View();

                    }
                    else
                    {
                        var file_name = g + Path.GetFileName(emp_photo.FileName);
                        var file_src = Path.Combine(Server.MapPath("/Uploads"), file_name);
                        emp_photo.SaveAs(file_src);
                        employee.emp_photo = file_name;
                    }
                }
                if (emp_resume != null)
                {
                    if (emp_resume.ContentLength / 1024 > 5 && !emp_resume.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var pdf_file_name = g + Path.GetFileName(emp_resume.FileName);
                        var pdf_file_src = Path.Combine(Server.MapPath("/Files"), pdf_file_name);
                        emp_resume.SaveAs(pdf_file_src);
                        employee.emp_resume = pdf_file_name;
                    }
                }
                if (emp_offer_letter != null)
                {
                    if (emp_offer_letter.ContentLength / 1024 > 5 && !emp_offer_letter.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var letter_file_name = g + Path.GetFileName(emp_offer_letter.FileName);
                        var letter_file_src = Path.Combine(Server.MapPath("/Files"), letter_file_name);
                        emp_offer_letter.SaveAs(letter_file_src);
                        employee.emp_offer_letter = letter_file_name;
                    }
                }
                if (emp_joining_letter != null)
                {
                    if (emp_joining_letter.ContentLength / 1024 > 5 && !emp_joining_letter.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var join_file_name = g + Path.GetFileName(emp_joining_letter.FileName);
                        var join_file_src = Path.Combine(Server.MapPath("/Files"), join_file_name);
                        emp_joining_letter.SaveAs(join_file_src);
                        employee.emp_joining_letter = join_file_name;
                    }
                }
                if (emp_contact_and_argue != null)
                {
                    if (emp_contact_and_argue.ContentLength / 1024 > 5 && !emp_contact_and_argue.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var contact_file_name = g + Path.GetFileName(emp_contact_and_argue.FileName);
                        var contact_file_src = Path.Combine(Server.MapPath("/Files"), contact_file_name);
                        emp_contact_and_argue.SaveAs(contact_file_src);
                        employee.emp_contact_and_argue = contact_file_name;
                    }
                }
                if (emp_ID_proof != null)
                {
                    if (emp_ID_proof.ContentLength / 1024 > 5 && !emp_ID_proof.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var proof_file_name = g + Path.GetFileName(emp_ID_proof.FileName);
                        var proof_file_src = Path.Combine(Server.MapPath("/Files"), proof_file_name);
                        emp_ID_proof.SaveAs(proof_file_src);
                        employee.emp_ID_proof = proof_file_name;
                    }
                }

                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");

            }

            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name", employee.emp_dep_id);
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name", employee.emp_desig_id);
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name", employee.emp_gender_id);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name", employee.emp_dep_id);
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name", employee.emp_desig_id);
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name", employee.emp_gender_id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,emp_fullname,emp_fathername,emp_dateof_birth,emp_gender_id,emp_phone,emp_address,emp_photo,emp_email,emp_password,emp_dep_id,emp_desig_id,emp_date_of_joining,emp_exit_date,emp_work_status,emp_salary,emp_resume,emp_offer_letter,emp_joining_letter,emp_contact_and_argue,emp_ID_proof")] Employee employee, HttpPostedFileBase emp_photo, HttpPostedFileBase emp_resume, HttpPostedFileBase emp_offer_letter, HttpPostedFileBase emp_joining_letter, HttpPostedFileBase emp_contact_and_argue, HttpPostedFileBase emp_ID_proof)
        {
            if (ModelState.IsValid)
            {
                var empl = db.Employees.Find(employee.id);
                //string oldimage = empl.emp_photo;
                var g = Guid.NewGuid();
                if (emp_photo != null)
                {
                    if (emp_photo.ContentLength / 1024 > 5 && !emp_photo.ContentType.Contains("image"))
                    {
                        return View();

                    }
                    else
                    {
                        var file_name = g + Path.GetFileName(emp_photo.FileName);
                        var file_src = Path.Combine(Server.MapPath("/Uploads"), file_name);
                        emp_photo.SaveAs(file_src);
                        empl.emp_photo = file_name;
                    }
                }
                if (emp_resume != null)
                {
                    if (emp_resume.ContentLength / 1024 > 5 && !emp_resume.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var pdf_file_name = g + Path.GetFileName(emp_resume.FileName);
                        var pdf_file_src = Path.Combine(Server.MapPath("/Files"), pdf_file_name);
                        emp_resume.SaveAs(pdf_file_src);
                        empl.emp_resume = pdf_file_name;
                    }
                }
                if (emp_offer_letter != null)
                {
                    if (emp_offer_letter.ContentLength / 1024 > 5 && !emp_offer_letter.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var letter_file_name = g + Path.GetFileName(emp_offer_letter.FileName);
                        var letter_file_src = Path.Combine(Server.MapPath("/Files"), letter_file_name);
                        emp_offer_letter.SaveAs(letter_file_src);
                        empl.emp_offer_letter = letter_file_name;
                    }
                }
                if (emp_joining_letter != null)
                {
                    if (emp_joining_letter.ContentLength / 1024 > 5 && !emp_joining_letter.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var join_file_name = g + Path.GetFileName(emp_joining_letter.FileName);
                        var join_file_src = Path.Combine(Server.MapPath("/Files"), join_file_name);
                        emp_joining_letter.SaveAs(join_file_src);
                        empl.emp_joining_letter = join_file_name;
                    }
                }
                if (emp_contact_and_argue != null)
                {
                    if (emp_contact_and_argue.ContentLength / 1024 > 5 && !emp_contact_and_argue.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var contact_file_name = g + Path.GetFileName(emp_contact_and_argue.FileName);
                        var contact_file_src = Path.Combine(Server.MapPath("/Files"), contact_file_name);
                        emp_contact_and_argue.SaveAs(contact_file_src);
                        empl.emp_contact_and_argue = contact_file_name;
                    }
                }
                if (emp_ID_proof != null)
                {
                    if (emp_ID_proof.ContentLength / 1024 > 5 && !emp_ID_proof.ContentType.Contains("application"))
                    {
                        return View();

                    }
                    else
                    {
                        var proof_file_name = g + Path.GetFileName(emp_ID_proof.FileName);
                        var proof_file_src = Path.Combine(Server.MapPath("/Files"), proof_file_name);
                        emp_ID_proof.SaveAs(proof_file_src);
                        empl.emp_ID_proof = proof_file_name;
                    }
                }

                //db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_dep_id = new SelectList(db.Departaments, "id", "depart_name", employee.emp_dep_id);
            ViewBag.emp_desig_id = new SelectList(db.Designations, "id", "desig_name", employee.emp_desig_id);
            ViewBag.emp_gender_id = new SelectList(db.Genders, "id", "gender_name", employee.emp_gender_id);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
