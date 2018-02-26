using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRM_WebApp.Models
{
    public class Check
    {
        public static bool Check_Login()
        {
            if (HttpContext.Current.Session["Email"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Check_User_Login()
        {
            if (HttpContext.Current.Session["UserEmail"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}