using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ElectroShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Dang-Ky",
               url: "register",
              defaults: new { Controller = "Account", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Dang-xuat",
               url: "logout",
              defaults: new { Controller = "Account", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Dang-nhap",
               url: "login",
              defaults: new { Controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AllProducts",
                url: "san-pham",
                defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "AllPosts",
               url: "tin-tuc",
               defaults: new { controller = "Module", action = "Posts", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Gio hang",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Thanh toán!",
               url: "thanh-toan",
               defaults: new { controller = "Cart", action = "Checkout", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Search",
               url: "search",
               defaults: new { controller = "Site", action = "Search", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Contact",
              url: "lien-he",
              defaults: new { controller = "Site", action = "Contact", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "SiteSlug",
                url: "{slug}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
