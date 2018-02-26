using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRM_WebApp.Models;
namespace HRM_WebApp.ViewModel
{
    public class UserModel
    {
        public List<Employee> _employee { get; set; }
        public Employee Employee;
        public List<Award> Award{ get; set; }
        public List<Holiday> Holiday { get; set; }
        public List<Notice_Board> Notice { get; set; }
        public List<Leave_App> Leave { get; set; }
        public List<Leave_status> LeaveStatus { get; set; }
    }
}