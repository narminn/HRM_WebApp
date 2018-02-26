using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM_WebApp.Models;
using HRM_WebApp.ViewModel;
namespace HRM_WebApp.Controllers
{
    public class AdminPanelController : Controller
    {
        HRM_databaseEntities1 db = new HRM_databaseEntities1();
        Salary vm = new Salary();
        AttendenceModel atd = new AttendenceModel();
        // GET: AdminPanel
        public ActionResult Index()
        {
            if (Check.Check_Login())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string email = frm["email"];
            string password = frm["password"];
            int count = db.Admins.Where(a => a.admin_email == email && a.admin_password == password).Count();
            if (count>0)
            {
                var admin = db.Admins.First(d => d.admin_email == email && d.admin_password == password);
                Session["Email"] = email;
                Session["Password"] = password;
                return RedirectToAction("Index");
            }
            else
            { 
            return View();
            }
        }
        public ActionResult Logout()
        {
            if (Check.Check_Login())
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Salary()
        {
            vm._employee = db.Employees.ToList();
            vm._award = db.Awards.ToList();
            vm._leave = db.Leave_App.ToList();
            return View(vm);
        }
        public ActionResult Attendence_Employee()
        {
            atd.emp = db.Employees.ToList();
            atd.type = db.Leave_type.ToList();
            return View(atd);
        }
        //public ActionResult createarr(Array info)
        //{
        //    return Content(info.GetType().ToString());
        //}

        [HttpPost]
        public ActionResult Attendence_Employee(string ad)
        {
            atd.emp = db.Employees.ToList();
            atd.type = db.Leave_type.ToList();
            //Attendence at = new Attendence();
            //int a = 0;
            //foreach (var item in info)
            //{
            //    a = item;
            //}


            //for (int i = 0; i < frm.Count; i++)
            //{
            //    at.atten_date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //    at.atten_emp_id =19 /*Convert.ToInt32(frm["emp_id"][i])*/;
            //    at.atten_reason ="sada" /*frm["reason"][i].ToString()*/;
            //    at.atten_leave_type_id =1 /*Convert.ToInt32(frm["leave_id"][i])*/;
            //    if (frm["status"][i]==0)
            //    {
            //        at.atten_status = false;
            //    }
            //    else if (frm["status"][i] == 1)
            //    {
            //        at.atten_status = true;
            //    }
            //    db.Attendences.Add(at);
            //    db.SaveChanges();
            //}
            //string Reason = frm["reason"];
            //string empId = frm["emp_id"];
            //int leaveId = Convert.ToInt32(frm["leave_id"]);
            //bool Status = Convert.ToBoolean(frm["status"]);
            //atd.emp = db.Employees.ToList();
            //atd.type = db.Leave_type.ToList();

            return View(atd);
        }

        public ActionResult createajax(string[] number1,string[] leave1, string[] reason1, string[] arrid1)
        {
            //return Content(number+" "+leave);
            bool[] checkbool = new bool[reason1.Length];
            for (int i = 0; i < arrid1.Length; i++)
            {
                if (arrid1[i]=="1")
                {
                    checkbool[i] = true;
                }
                else
                {
                    checkbool[i] = false;
                }
            }
            Attendence at = new Attendence();
            for (int i = 0; i < number1.Length; i++)
            {
                at.atten_leave_type_id = Convert.ToInt32(leave1[i]);
                at.atten_emp_id = Convert.ToInt32(number1[i]);
                at.atten_status = checkbool[i];
                at.atten_reason = reason1[i];
                at.atten_date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Attendences.Add(at);
                db.SaveChanges();
            }
            return Content(number1.Length.ToString());

        }

    }
}