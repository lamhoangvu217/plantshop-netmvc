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
    public class CategoryController : BaseController
    {
        private ElectroShopDbContext db = new ElectroShopDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            ViewBag.count_trash = db.Categorys.Where(m => m.Status == 0).Count();
            var list = db.Categorys.Where(m => m.Status != 0).ToList();
            ViewBag.GetAllCategory = list;
            foreach (var row in list)
            {
                var temp_link = db.Links.Where(m => m.Type == "category" && m.TableId == row.Id);
                if (temp_link.Count() > 0)
                {
                    var row_link = temp_link.First();
                    row_link.Name = row.Name;
                    row_link.Slug = row.Slug;
                    db.Entry(row_link).State = EntityState.Modified;
                }
                else
                {
                    var row_link = new MLink();
                    row_link.Name = row.Name;
                    row_link.Slug = row.Slug;
                    row_link.Type = "category";
                    row_link.TableId = row.Id;
                    db.Links.Add(row_link);
                }
            }
            db.SaveChanges();
            return View(list);
        }


        public ActionResult Trash()
        {
            ViewBag.Title = "Danh sách các loại sản phẩm";
            ///////Select * from
            var model = db.Categorys
                .Where(m => m.Status == 0)
                .OrderByDescending(m => m.Created_at)
                .ToList();

            return View("Trash", model);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                Notification.set_flash("Không tồn tại!", "warning");
                return RedirectToAction("Index");
            }
            MCategory modelCategory = db.Categorys.Find(id);
            if (modelCategory == null)
            {
                Notification.set_flash("Không tồn tại!", "warning");
                return RedirectToAction("Index");
            }
            return View(modelCategory);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCast = new SelectList(db.Categorys, "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(db.Categorys, "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MCategory modelCategory)
        {
            ViewBag.ListCast = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Orders", "Name", 0);
            if (ModelState.IsValid)
            {
                if (modelCategory.ParentId == null)
                {
                    modelCategory.ParentId = 0;
                }
                string slug = MyString.ToAscii(modelCategory.Name);
                modelCategory.Slug = slug;
                CheckSlug check = new CheckSlug();

                if (!check.KiemTraSlug("Category", slug, null))
                {
                    Notification.set_flash("Tên danh mục đã tồn tại, vui lòng thử lại!", "warning");
                    return RedirectToAction("Create", "Category");
                }
                modelCategory.Orders += 1;
                modelCategory.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                modelCategory.Created_by = int.Parse(Session["Admin_ID"].ToString());
                modelCategory.Created_at = DateTime.Now;
                modelCategory.Updated_at = DateTime.Now;
                db.Categorys.Add(modelCategory);
                db.SaveChanges();
                Notification.set_flash("Danh mục đã được thêm!", "success");
                return RedirectToAction("Index");
            }
            Notification.set_flash("Có lỗi xảy ra khi thêm danh mục!", "warning");
            return View(modelCategory);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCategory modelCategory = db.Categorys.Find(id);
            if (modelCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCast = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Orders", "Name", 0);
            return View(modelCategory);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MCategory modelCategory)
        {
            if (ModelState.IsValid)
            {
                if (modelCategory.ParentId == null)
                {
                    modelCategory.ParentId = 0;
                }
                String slug = MyString.ToAscii(modelCategory.Name);
                modelCategory.Slug = slug;
                int ID = modelCategory.Id;
                if (db.Categorys.Where(m => m.Slug == slug && m.Id != ID).Count() > 0)
                {
                    Notification.set_flash("Tên danh mục đã tồn tại, vui lòng thử lại!", "warning");
                    return RedirectToAction("Edit", "Category");
                }
                if (db.Topics.Where(m => m.Slug == slug && m.Id != ID).Count() > 0)
                {
                    Notification.set_flash("Tên danh mục đã tồn tại trong TOPIC, vui lòng thử lại!", "warning");
                    return RedirectToAction("Edit", "Category");
                }
                if (db.Posts.Where(m => m.Slug == slug && m.Id != ID).Count() > 0)
                {
                    Notification.set_flash("Tên danh mục đã tồn tại trong POST, vui lòng thử lại!", "warning");
                    return RedirectToAction("Edit", "Category");
                }
                if (db.Products.Where(m => m.Slug == slug && m.ID != ID).Count() > 0)
                {
                    Notification.set_flash("Tên danh mục đã tồn tại trong PRODUCT, vui lòng thử lại!", "warning");
                    return RedirectToAction("Edit", "Category");
                }
                modelCategory.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                modelCategory.Created_by = int.Parse(Session["Admin_ID"].ToString());
                modelCategory.Created_at = DateTime.Now;
                modelCategory.Updated_at = DateTime.Now;
                db.Categorys.Add(modelCategory);
                // db.SaveChanges();

                db.Entry(modelCategory).State = EntityState.Modified;
                db.SaveChanges();
                Notification.set_flash("Cập nhật thành công!", "success");
                return RedirectToAction("Index");
            }
            ViewBag.ListCast = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Orders", "Name", 0);
            return View(modelCategory);
        }


        public ActionResult DelTrash(int id)
        {
            MCategory MCategory = db.Categorys.Find(id);
            if (MCategory == null)
            {
                Notification.set_flash("Không tồn tại danh mục cần xóa vĩnh viễn!", "warning");
                return RedirectToAction("Index");
            }
            int count_child = db.Categorys.Where(m => m.ParentId == id).Count();
            if (count_child != 0)
            {
                Notification.set_flash("Không thể xóa, danh mục có chứa danh mục con!", "warning");
                return RedirectToAction("Index");
            }
            MCategory.Status = 0;

            MCategory.Created_at = DateTime.Now;
            MCategory.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            MCategory.Updated_at = DateTime.Now;
            MCategory.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(MCategory).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Ném vào thùng rác!" + " ID = " + id, "success");
            return RedirectToAction("Index");
        }
        public ActionResult Undo(int? id)
        {
            MCategory modelCategory = db.Categorys.Find(id);
            if (modelCategory == null)
            {
                Notification.set_flash("Không tồn tại danh mục!", "warning");
                return RedirectToAction("Trash");
            }
            modelCategory.Status = 2;

            modelCategory.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            modelCategory.Created_by = int.Parse(Session["Admin_ID"].ToString());
            modelCategory.Created_at = DateTime.Now;
            modelCategory.Updated_at = DateTime.Now;

            db.Entry(modelCategory).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Khôi phục thành công!" + " ID = " + id, "success");
            return RedirectToAction("Trash","Category");

        }
        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            MCategory MCategory = db.Categorys.Find(id);
            MCategory.Status = (MCategory.Status == 1) ? 2 : 1;

            MCategory.Updated_at = DateTime.Now;
            MCategory.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(MCategory).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                Status = MCategory.Status
            });
        }
        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                Notification.set_flash("Không tồn tại danh mục cần xóa!", "warning");
                return RedirectToAction("Trash", "Category");
            }
            MCategory modelCategory = db.Categorys.Find(id);
            if (modelCategory == null)
            {
                Notification.set_flash("Không tồn tại danh mục cần xóa!", "warning");
                return RedirectToAction("Trash", "Category");
            }
            return View(modelCategory);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MCategory modelCategory = db.Categorys.Find(id);
            db.Categorys.Remove(modelCategory);
            db.SaveChanges();
            Notification.set_flash("Đã xóa hoàn toàn danh mục!", "success");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
