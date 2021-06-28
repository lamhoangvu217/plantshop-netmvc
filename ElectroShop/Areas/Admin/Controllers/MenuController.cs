using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElectroShop.Models;

namespace ElectroShop.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        private ElectroShopDbContext db = new ElectroShopDbContext();

        public ActionResult Index()
        {
            ViewBag.demrac = db.Menus.Where(m => m.Status == 0).Count();
            ViewBag.countTrash = db.Menus.Where(m => m.Status == 0).Count();
            ViewBag.ListCat = db.Categorys.Where(m => m.Status == 1).ToList();
            ViewBag.ListTopic = db.Topics.Where(m => m.Status == 1).ToList();
            ViewBag.ListPage = db.Posts.Where(m => m.Status == 1 && m.Type == "page").ToList();
            return View(db.Menus.Where(m => m.Status != 0).ToList());
        }
        [HttpPost]
        public ActionResult Index(FormCollection data)
        {
            ViewBag.ListCat = db.Categorys.Where(m => m.Status == 1).ToList();
            ViewBag.ListTopic = db.Topics.Where(m => m.Status == 1).ToList();
            ViewBag.ListPage = db.Posts.Where(m => m.Status == 1 && m.Type == "page").ToList();

            if (!String.IsNullOrEmpty(data["AddCat"]))
            {
                if (!String.IsNullOrEmpty(data["itemCat"]))
                {
                    var itemcat = data["itemCat"];
                    var arr = itemcat.Split(',');
                    int count = 0;
                    foreach (var i in arr)
                    {
                        int id = int.Parse(i);
                        MCategory mCategory = db.Categorys.Find(id);
                        MMenu mMenu = new MMenu();
                        mMenu.Name = mCategory.Name;
                        mMenu.Link = mCategory.Slug;
                        mMenu.Type = "category";
                        mMenu.TableId = id;
                        mMenu.Orders = 1;
                        mMenu.Position = data["Position"];
                        mMenu.ParentId = 0;
                        mMenu.Status = 2;
                        mMenu.Created_at = DateTime.Now;
                        mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                        mMenu.Updated_at = DateTime.Now;
                        mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                        db.Menus.Add(mMenu);
                        db.SaveChanges();
                        count++;
                    }
                    Notification.set_flash("Đã thêm " + count + " menu mới!", "success");
                }
                else
                {
                    Notification.set_flash("Chưa chọn menu cần thêm!", "warning");
                }
            }
            if (!String.IsNullOrEmpty(data["AddTopic"]))
            {
                if (!String.IsNullOrEmpty(data["itemTopic"]))
                {
                    var itemtopic = data["itemTopic"];
                    var arr = itemtopic.Split(',');
                    int count = 0;
                    foreach (var i in arr)
                    {
                        int id = int.Parse(i);
                        MTopic mTopic = db.Topics.Find(id);
                        MMenu mMenu = new MMenu();
                        mMenu.Name = mTopic.Name;
                        mMenu.Link = mTopic.Slug;
                        mMenu.Type = "topic";
                        mMenu.TableId = id;
                        mMenu.Orders = 1;
                        mMenu.Position = data["Position"];
                        mMenu.ParentId = 0;
                        mMenu.Status = 2;
                        mMenu.Created_at = DateTime.Now;
                        mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                        mMenu.Updated_at = DateTime.Now;
                        mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                        db.Menus.Add(mMenu);
                        db.SaveChanges();
                        count++;
                    }
                    Notification.set_flash("Đã thêm " + count + " menu mới!", "success");
                }
                else
                {
                    Notification.set_flash("Chưa chọn menu cần thêm!", "warning");
                }
            }
            if (!String.IsNullOrEmpty(data["AddPage"]))
            {

                if (!String.IsNullOrEmpty(data["itemPage"]))
                {
                    var itempage = data["itemPage"];
                    var arr = itempage.Split(',');
                    int count = 0;
                    foreach (var i in arr)
                    {
                        int id = int.Parse(i);
                        MPost mPost = db.Posts.Find(id);
                        MMenu mMenu = new MMenu();
                        mMenu.Name = mPost.Title;
                        mMenu.Link = mPost.Slug;
                        mMenu.Type = "page";
                        mMenu.TableId = id;
                        mMenu.Orders = 1;
                        mMenu.Position = data["Position"];
                        mMenu.ParentId = 0;
                        mMenu.Status = 2;
                        mMenu.Created_at = DateTime.Now;
                        mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                        mMenu.Updated_at = DateTime.Now;
                        mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                        db.Menus.Add(mMenu);
                        db.SaveChanges();
                        count++;
                    }
                    Notification.set_flash("Đã thêm " + count + " menu mới!", "success");
                }
                else
                {
                    Notification.set_flash("Chưa chọn menu cần thêm!", "warning");
                }
            }
            if (!String.IsNullOrEmpty(data["AddCustom"]))
            {
                if (!String.IsNullOrEmpty(data["name"]) && !String.IsNullOrEmpty(data["link"]))
                {
                    MMenu mMenu = new MMenu();
                    mMenu.Name = data["name"];
                    mMenu.Link = data["link"];
                    mMenu.Type = "custom";
                    mMenu.Orders = 1;
                    mMenu.Position  = data["Position"];
                    mMenu.ParentId = 0;
                    mMenu.Status = 2;
                    mMenu.Created_at = DateTime.Now;
                    mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                    mMenu.Updated_at = DateTime.Now;
                    mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                    db.Menus.Add(mMenu);
                    db.SaveChanges();
                    Notification.set_flash("Đã thêm 1 menu mới!", "success");
                }
                else
                {
                    Notification.set_flash("Vui lòng nhập tên menu và link!", "warning");
                }
            }

            return View(db.Menus.Where(m => m.Status != 0).ToList());
        }
        public ActionResult Trash()
        {

            return View(db.Menus.Where(m => m.Status == 0).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                Notification.set_flash("Không tồn tại!", "warning");
                return RedirectToAction("Index");
            }
            MMenu mMenu = db.Menus.Find(id);
            if (mMenu == null)
            {
                Notification.set_flash("Không tồn tại!", "warning");
                return RedirectToAction("Index");
            }
            return View(mMenu);
        }

        public ActionResult DelTrash(int id)
        {
            MMenu mMenu = db.Menus.Find(id);
            if (mMenu == null)
            {
                Notification.set_flash("Không tồn tại danh mục cần xóa vĩnh viễn!", "warning");
                return RedirectToAction("Index");
            }

            mMenu.Status = 0;
            mMenu.Updated_at = DateTime.Now;
            mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(mMenu).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Ném thành công vào thùng rác!" + " ID = " + id, "success");
            return RedirectToAction("Index");
        }

        public ActionResult ReTrash(int? id)
        {
            MMenu mMenu = db.Menus.Find(id);
            if (mMenu == null)
            {
                Notification.set_flash("Không tồn tại danh mục!", "warning");
                return RedirectToAction("Trash", "Category");
            }
            mMenu.Status = 2;

            mMenu.Updated_at = DateTime.Now;
            mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(mMenu).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Khôi phục thành công!" + " ID = " + id, "success");
            return RedirectToAction("Trash", "Menu");
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.demrac = db.Products.Where(m => m.Status == 0).Count();
            if (id == null)
            {
                Notification.set_flash("Không tồn tại!", "warning");
                return RedirectToAction("Index");
            }
            MMenu mMenu = db.Menus.Find(id);
            if (mMenu == null)
            {
                Notification.set_flash("Không tồn tại!", "warning");
                return RedirectToAction("Index");
            }
            ViewBag.parentMenu = new SelectList(db.Menus.Where(m => m.Status == 1 && m.ParentId == 0), "ID", "Name", 0);
            return View(mMenu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MMenu mMenu)
        {
            ViewBag.parentMenu = new SelectList(db.Menus.Where(m => m.Status == 1 && m.ParentId == 0), "ID", "Name", 0);
            if (ModelState.IsValid)
            {
                if (mMenu.ParentId == null)
                {
                    mMenu.ParentId = 0;
                }
                mMenu.Type = "page";
                mMenu.Orders = 1;
                mMenu.Updated_at = DateTime.Now;
                mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                db.Entry(mMenu).State = EntityState.Modified;
                db.SaveChanges();
                Notification.set_flash("Cập nhật thành công!", "success");
                return RedirectToAction("Index");
            }
            return View(mMenu);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                Notification.set_flash("Không tồn tại menu!", "warning");
                return RedirectToAction("Trash", "Menu");
            }
            MMenu mMenu = db.Menus.Find(id);
            if (mMenu == null)
            {
                Notification.set_flash("Không tồn tại menu!", "warning");
                return RedirectToAction("Trash", "Menu");
            }


            return View(mMenu);
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MMenu mMenu = db.Menus.Find(id);
            db.Menus.Remove(mMenu);
            db.SaveChanges();
            Notification.set_flash("Đã xóa vĩnh viễn menu!", "success");
            return RedirectToAction("Trash");
        }
        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            MMenu mMenu = db.Menus.Find(id);
            mMenu.Status = (mMenu.Status == 1) ? 2 : 1;

            mMenu.Updated_at = DateTime.Now;
            mMenu.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(mMenu).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                Status = mMenu.Status
            });
        }

    }
}
