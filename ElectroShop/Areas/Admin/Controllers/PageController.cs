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
    public class PageController : BaseController
    {
        private ElectroShopDbContext db = new ElectroShopDbContext();

        public ActionResult Index()
        {
            ViewBag.countTrash = db.Posts.Where(m => m.Status == 0 && m.Type == "page").Count();
            var list = db.Posts.Where(m => m.Status != 0 && m.Type == "page").ToList();
            foreach (var row in list)
            {
                var temp_link = db.Links.Where(m => m.Type == "page" && m.TableId == row.Id);
                if (temp_link.Count() > 0)
                {
                    var row_link = temp_link.First();
                    row_link.Name = row.Title;
                    row_link.Slug = row.Slug;
                    db.Entry(row_link).State = EntityState.Modified;
                }
                else
                {
                    var row_link = new MLink();
                    row_link.Name = row.Title;
                    row_link.Slug = row.Slug;
                    row_link.Type = "page";
                    row_link.TableId = row.Id;
                    db.Links.Add(row_link);
                }
            }
            db.SaveChanges();
            return View(list);
        }
        public ActionResult Trash()
        {
            ViewBag.countTrash = db.Posts.Where(m => m.Status == 0 && m.Type == "page").Count();
            return View(db.Posts.Where(m => m.Status == 0 && m.Type == "page").ToList());
        }

        public ActionResult Details(int? id)
        {
            ViewBag.countTrash = db.Posts.Where(m => m.Status == 0 && m.Type == "page").Count();
            if (id == null)
            {
                Notification.set_flash("Không tồn tại trang đơn!", "warning");
                return RedirectToAction("Index", "Page");
            }
            MPost mPost = db.Posts.Find(id);
            if (mPost == null)
            {
                Notification.set_flash("Không tồn tại trang đơn!", "warning");
                return RedirectToAction("Index", "Page");
            }
            return View(mPost);
        }

        public ActionResult Create()
        {
            ViewBag.countTrash = db.Posts.Where(m => m.Status == 0 && m.Type == "page").Count();
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MPost mPost)
        {
            if (ModelState.IsValid)
            {
                String strSlug = MyString.ToAscii(mPost.Title);
                mPost.Slug = strSlug;
                mPost.Type = "page";
                mPost.Created_At = DateTime.Now;
                mPost.Created_By = int.Parse(Session["Admin_ID"].ToString());
                mPost.Updated_At = DateTime.Now;
                mPost.Updated_By = int.Parse(Session["Admin_ID"].ToString());
                var file = Request.Files["Img"];
                if (file != null && file.ContentLength > 0)
                {
                    String filename = strSlug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    mPost.Img = filename;
                    String Strpath = Path.Combine(Server.MapPath("~/Public/Library/page/"), filename);
                    file.SaveAs(Strpath);
                }
                db.Posts.Add(mPost);
                db.SaveChanges();
                Notification.set_flash("Đã thêm trang đơn mới!", "success");
                return RedirectToAction("Index");
            }

            return View(mPost);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.countTrash = db.Posts.Where(m => m.Status == 0 && m.Type == "page").Count();
            if (id == null)
            {
                Notification.set_flash("Không tồn tại trang đơn!", "warning");
                return RedirectToAction("Index", "Page");
            }
            MPost mPost = db.Posts.Find(id);
            if (mPost == null)
            {
                Notification.set_flash("Không tồn tại trang đơn!", "warning");
                return RedirectToAction("Index", "Page");
            }
            return View(mPost);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MPost mPost)
        {
            if (ModelState.IsValid)
            {
                String strSlug = MyString.ToAscii(mPost.Title);
                mPost.Slug = strSlug;
                mPost.Type = "page";
                mPost.Updated_At = DateTime.Now;
                mPost.Updated_By = int.Parse(Session["Admin_ID"].ToString());
                var file = Request.Files["Img"];
                if (file != null && file.ContentLength > 0)
                {
                    String filename = strSlug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    mPost.Img = filename;
                    String Strpath = Path.Combine(Server.MapPath("~/Public/Library/page/"), filename);
                    file.SaveAs(Strpath);
                }

                db.Entry(mPost).State = EntityState.Modified;
                db.SaveChanges();
                Notification.set_flash("Đã cập nhật lại nội dung trang đơn!", "success");
                return RedirectToAction("Index");
            }
            return View(mPost);
        }
        public ActionResult DelTrash(int? id)
        {
            MPost mPost = db.Posts.Find(id);
            mPost.Status = 0;

            mPost.Updated_At = DateTime.Now;
            mPost.Updated_By = 1;
            db.Entry(mPost).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Đã chuyển vào thùng rác!" + " ID = " + id, "success");
            return RedirectToAction("Index");
        }
        public ActionResult Undo(int? id)
        {
            MPost mPost = db.Posts.Find(id);
            mPost.Status = 2;

            mPost.Updated_At = DateTime.Now;
            mPost.Updated_By = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(mPost).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Khôi phục thành công!" + " ID = " + id, "success");
            return RedirectToAction("Trash");
        }
        public ActionResult Delete(int? id)
        {
            ViewBag.countTrash = db.Posts.Where(m => m.Status == 0 && m.Type == "page").Count();
            if (id == null)
            {
                Notification.set_flash("Không tồn tại trang đơn!", "warning");
                return RedirectToAction("Index", "Page");
            }
            MPost mPost = db.Posts.Find(id);
            if (mPost == null)
            {
                Notification.set_flash("Không tồn tại trang đơn!", "warning");
                return RedirectToAction("Index", "Page");
            }
            return View(mPost);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MPost mPost = db.Posts.Find(id);
            db.Posts.Remove(mPost);
            db.SaveChanges();
            Notification.set_flash("Đã xóa vĩnh viễn", "danger");
            return RedirectToAction("Trash");
        }

    }
}
