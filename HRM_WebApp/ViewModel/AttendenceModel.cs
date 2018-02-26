using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRM_WebApp.Models;
namespace HRM_WebApp.ViewModel
{
    public class AttendenceModel
    {
        public List<Employee> emp { get; set; }
        public List<Leave_type> type { get; set; }
    }
}