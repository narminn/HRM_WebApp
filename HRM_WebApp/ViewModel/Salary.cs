using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRM_WebApp.Models;
namespace HRM_WebApp.ViewModel
{
    public class Salary
    {
        public List<Employee> _employee { get; set; }
        public List<Award> _award { get; set; }
        public List<Leave_App> _leave { get; set; }
    }
}