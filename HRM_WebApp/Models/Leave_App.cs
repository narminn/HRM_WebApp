//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRM_WebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Leave_App
    {
        public int id { get; set; }
        public Nullable<int> leave_emp_id { get; set; }
        public Nullable<System.DateTime> leave_date { get; set; }
        public string leave_reason { get; set; }
        public Nullable<int> leave_status_id { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Leave_status Leave_status { get; set; }
    }
}
