using ElectroShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectroShop.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private ElectroShopDbContext db = new ElectroShopDbContext();
        public ActionResult Login()
        {
            if (Session["Admin_Name"] != null)
            {
              Response.Redirect("~/Admin");
               
            }
            return View();
        }

        [HttpPost]
        public JsonResult Login(String User, String Pass)
        {
            int count_username = db.Users.Where(m => m.Status == 1 && ((m.Phone).ToString() == User || m.Email == User || m.Name == User) && m.Access == 0).Count();

            if (count_username == 0)
                return Json(new { s = 1 });
            else
            {
                String Password = MyString.ToMD5(Pass);
                /*access = 0 => admin, access = 1 => user*/
                /*String Password = Pass;*/
                var user_acount = db.Users
                .Where(m => m.Status == 1 && (m.Name == User && m.Password == Password) && m.Access == 0 );
                if (user_acount.Count() == 0)
                    return Json(new { s = 2 });
                else
                {
                    var user = user_acount.First();
                    Session["Admin_Name"] = user.FullName;
                    Session["Admin_ID"] = user.ID;
                    Session["Admin_Images"] = user.Image;
                    Session["Admin_Email"] = user.Email;
                    Session["Admin_Created_at"] = user.Created_at;
                    return Json(new { s = 0 });
                }
            }
        }

        public ActionResult Logout()
        {
            if (Session["Admin_Name"] != null)
            {
                Session["Admin_Name"] = null;
                Session["Admin_ID"] = null;
                Session["Admin_Images"] = null;
                Session["Admin_Address"] = null;
                Session["Admin_Email"] = null;
                Session["Admin_Created_at"] = null;
            }
            return Redirect("~/Admin/Login");
        }
        public ActionResult Profiles()
        {
            if (Session["Admin_Name"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            if (Session["Admin_Name"] == null)
            {
                return HttpNotFound();
            }


            return View("Profiles");

        }
        public ActionResult Check()
        {
            if (Session["Admin_Name"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            if (Session["Admin_Name"] == null)
            {
                return HttpNotFound();
            }
            var list = db.Users.Where(m => m.Status == 1).ToList();
            return View("_Test", list);

        }
    }
}