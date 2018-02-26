using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM_WebApp.Models;
using HRM_WebApp.ViewModel;
namespace HRM_WebApp.Controllers
{
    public class UserPanelController : Controller
    {
        HRM_databaseEntities1 db = new HRM_databaseEntities1();
        UserModel um = new UserModel();
        Leave l = new Leave();
        string email;
        string password;
        // GET: UserPanel
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            email = frm["userEmail"];
            password = frm["userPassword"];
            var count = db.Employees.Where(e => e.emp_email == email && e.emp_password == password).FirstOrDefault();
            if (count !=null)
            {
                var employee = db.Employees.First(p => p.emp_email == email && p.emp_password == password);
                Session["UserEmail"] = count.emp_email;
                Session["id"] = count.id;
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Index()
        {

            if (Check.Check_User_Login())
            {
                Employee employee = db.Employees.Find(Session["id"]);
                var id = Convert.ToInt32(Session["id"]);
                var depId = Convert.ToInt32(employee.emp_dep_id);
                um.Employee = db.Employees.Where(e => e.id ==id ).FirstOrDefault();
                um.Award = db.Awards.Where(a=>a.award_emp_id==id).ToList();
                um.Holiday = db.Holidays.ToList();
                um.Notice = db.Notice_Board.Where(n => n.dep_id == depId || n.dep_id==null).ToList();
                um.Leave = db.Leave_App.Where(l => l.leave_emp_id == id).ToList();
                um.LeaveStatus = db.Leave_status.ToList();
                return View(um);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            if (Check.Check_User_Login())
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Leave()
        {
            if (Check.Check_User_Login())
            {
                var id = Convert.ToInt32(Session["id"]);
                l._leave = db.Leave_App.Where(l => l.leave_emp_id == id).ToList();
                return View(l);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Submit_Leave(FormCollection frm)
        {
            
            var id = Convert.ToInt32(Session["id"]);
            var leaveDate = frm["l_date"];
            var leaveReason = frm["l_reason"];
            Leave_App l_app = new Leave_App();
            l_app.leave_date = Convert.ToDateTime(leaveDate);
            l_app.leave_reason = leaveReason;
            l_app.leave_status_id = 1;
            l_app.leave_emp_id = id;
            db.Leave_App.Add(l_app);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
        [HttpPost]
        public ActionResult ChangePassword(FormCollection frm)
        {
            Employee employee = db.Employees.Find(Session["id"]);

            string pass = Convert.ToString(employee.emp_password);
            string oldPass = frm["old_password"];
            string newPass = frm["new_password"];
            if (oldPass == pass)
            {
                employee.emp_password = newPass;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        
    }
}