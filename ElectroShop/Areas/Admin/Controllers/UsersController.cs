using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElectroShop.Models;

namespace ElectroShop.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private ElectroShopDbContext db = new ElectroShopDbContext();
        public ActionResult Index()
        {
            ViewBag.demrac = db.Users.Where(m => m.Status == 0 && m.Access != 0).Count();
            return View(db.Users.Where(m => m.Status != 0 && m.Access != 0).ToList());
        }
        public ActionResult Trash()
        {
            return View(db.Users.Where(m => m.Status == 0 && m.Access != 0).ToList());
        }

        public ActionResult DelTrash(int id)
        {
            MUser mUser = db.Users.Find(id);
            if (mUser == null)
            {
                Notification.set_flash("Không tồn tại User trên!", "warning");
                return RedirectToAction("Index");
            }

            mUser.Status = 0;

            mUser.Created_at = DateTime.Now;
            mUser.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            mUser.Updated_at = DateTime.Now;
            mUser.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(mUser).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Đã xóa vào thùng rác!" + " ID = " + id, "success");
            return RedirectToAction("Index");
        }

        // Delete to trash
        public ActionResult ReTrash(int? id)
        {
            MUser mUser = db.Users.Find(id);
            if (mUser == null)
            {
                Notification.set_flash("Không tồn tại User!", "warning");
                return RedirectToAction("Trash", "User");
            }
            mUser.Status = 2;

            mUser.Updated_at = DateTime.Now;
            mUser.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(mUser).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Khôi phục thành công!" + " ID = " + id, "success");
            return RedirectToAction("Trash", "User");
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUser mUser = db.Users.Find(id);
            if (mUser == null)
            {
                return HttpNotFound();
            }
            return View(mUser);
        }


        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MUser mUser = db.Users.Find(id);
        //    if (mUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mUser);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(MUser mUser)
        //{
        //    ViewBag.demrac = db.Users.Where(m => m.Status == 0).Count();
        //    if (ModelState.IsValid)
        //    {
        //        String avatar = MyString.str_slug(mUser.FullName);
        //        mUser.Password = MyString.ToMD5(mUser.Password);
        //        mUser.Created_at = DateTime.Now;
        //        mUser.Created_by = int.Parse(Session["Admin_ID"].ToString());
        //        mUser.Updated_at = DateTime.Now;
        //        mUser.Updated_by = int.Parse(Session["Admin_ID"].ToString());

        //        var file = Request.Files["Image"];
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            String filename = avatar + file.FileName.Substring(file.FileName.LastIndexOf("."));
        //            mUser.Image = filename;
        //            String Strpath = Path.Combine(Server.MapPath("~/Public/library/user"), filename);
        //            file.SaveAs(Strpath);
        //        }
        //        db.Entry(mUser).State = EntityState.Modified;
        //        Notification.set_flash("Sửa thành công!", "success");
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(mUser);
        //}

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUser mUser = db.Users.Find(id);
            if (mUser == null)
            {
                return HttpNotFound();
            }
            return View(mUser);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MUser mUser = db.Users.Find(id);
            db.Users.Remove(mUser);
            db.SaveChanges();
            Notification.set_flash("Đã xóa hoàn User!", "success");
            return RedirectToAction("Trash");
        }

        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            MUser mUser = db.Users.Find(id);
            mUser.Status = (mUser.Status == 1) ? 2 : 1;

            mUser.Updated_at = DateTime.Now;
            mUser.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(mUser).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                Status = mUser.Status
            });
        }
    }
}
