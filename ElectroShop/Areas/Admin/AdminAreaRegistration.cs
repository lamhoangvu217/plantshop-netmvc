using System.Web.Mvc;

namespace ElectroShop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "Login",
               "Admin/Login",
               new { Controller = "Auth", action = "Login", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "Logout",
               "Admin/Logout",
               new { Controller = "Auth", action = "Logout", id = UrlParameter.Optional }
           );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}