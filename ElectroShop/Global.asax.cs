using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ElectroShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            Session["Notification"] = "";
            Session["Message"] = "";
            
            // Administrators
            Session["Admin_Name"] = null;
            Session["Admin_ID"] = null;
            Session["Admin_Images"] = null;
            Session["Admin_Address"] = null;
            Session["Admin_Email"] = null;
            Session["Admin_Created_at"] = null;
            // Customer
            Session["User_Name"] = null;
            Session["User_ID"] = null;
            Session["User_Images"] = null;


            Session["Cart"] = null;
            Session["keywords"] = null;
            Session["Status"] = null;
        }
    }
}
