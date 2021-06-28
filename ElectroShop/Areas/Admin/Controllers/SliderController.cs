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
    public class SliderController : BaseController
    {
        private ElectroShopDbContext db = new ElectroShopDbContext();

        // GET: Admin/Slider
        public ActionResult Index()
        {
            ViewBag.count_trash = db.Sliders.Where(m => m.Status == 0).Count();
            return View(db.Sliders.Where(m => m.Status != 0).ToList());
        }

        // GET: Admin/Slider/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.count_trash = db.Sliders.Where(m => m.Status == 0).Count();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MSlider modelSlider = db.Sliders.Find(id);
            if (modelSlider == null)
            {
                return HttpNotFound();
            }
            return View(modelSlider);
        }

        // GET: Admin/Slider/Create
        public ActionResult Create()
        {
            ViewBag.count_trash = db.Sliders.Where(m => m.Status == 0).Count();
            ViewBag.Orders = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Slider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MSlider modelSlider)
        {
            if (ModelState.IsValid)
            {
                String strSlug = MyString.ToAscii(modelSlider.Name);
                String slug = strSlug;
                modelSlider.Link = slug;
                modelSlider.Position = "slideshow";
                modelSlider.Created_at = DateTime.Now;
                modelSlider.Updated_at = DateTime.Now;
                modelSlider.Created_by = int.Parse(Session["Admin_ID"].ToString());
                modelSlider.Updated_by = int.Parse(Session["Admin_ID"].ToString());

                var f = Request.Files["Img"];

                if (f != null & f.ContentLength > 0)
                {
                    String fileName = strSlug + f.FileName.Substring(f.FileName.LastIndexOf("."));
                    modelSlider.Img = fileName;
                    String Strpath = Path.Combine(Server.MapPath("~/Public/library/slider"), fileName);
                    f.SaveAs(Strpath);
                }
                ViewBag.Orders = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
                db.Sliders.Add(modelSlider);
                db.SaveChanges();
                Notification.set_flash("Thêm thành công!", "success");
                return RedirectToAction("Index");
            }

            return View(modelSlider);
        }

        // GET: Admin/Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.count_trash = db.Sliders.Where(m => m.Status == 0).Count();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Orders = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            MSlider modelSlider = db.Sliders.Find(id);
            if (modelSlider == null)
            {
                return HttpNotFound();
            }
            return View(modelSlider);
        }

        // POST: Admin/Slider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MSlider modelSlider)
        {
            ViewBag.count_trash = db.Sliders.Where(m => m.Status == 0).Count();
            if (ModelState.IsValid)
            {
                String strSlug = MyString.ToAscii(modelSlider.Name);
                String slug = strSlug;
                modelSlider.Link = slug;
                modelSlider.Position = "slideshow";
                modelSlider.Updated_at = DateTime.Now;

                modelSlider.Updated_by = int.Parse(Session["Admin_ID"].ToString());
                modelSlider.Created_by = int.Parse(Session["Admin_ID"].ToString());

                ////Upload file
                var f = Request.Files["Img"];

                if (f != null & f.ContentLength > 0)
                {
                    String fileName = strSlug + f.FileName.Substring(f.FileName.LastIndexOf("."));
                    modelSlider.Img = fileName;
                    String Strpath = Path.Combine(Server.MapPath("~/Public/library/slider"), fileName);
                    f.SaveAs(Strpath);
                }
                db.Entry(modelSlider).State = EntityState.Modified;
                db.SaveChanges();
                Notification.set_flash("Cập nhập thông tin slider thành công!", "success");
                return RedirectToAction("Index");
            }
            ViewBag.Orders = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            return View(modelSlider);
        }
        public ActionResult Trash()
        {
            var list = db.Sliders.Where(m => m.Status == 0).ToList();

            return View(list);
        }
        public ActionResult delTrash(int? id)
        {
            MSlider modelSlider = db.Sliders.Find(id);
            if (modelSlider == null)
            {
                return RedirectToAction("Index");
            }
            modelSlider.Status = 0;

            modelSlider.Updated_at = DateTime.Now;

            modelSlider.Updated_at = DateTime.Now;
            modelSlider.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(modelSlider).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Ném thành công vào thùng rác!" + " ID = " + id, "success");
            return RedirectToAction("Index");

        }
        public ActionResult Undo(int? id)
        {
            MSlider modelSlider = db.Sliders.Find(id);
            if (modelSlider == null)
            {
                return RedirectToAction("Trash");
            }
            modelSlider.Status = 2;

            modelSlider.Updated_at = DateTime.Now;
            modelSlider.Updated_at = DateTime.Now;
            modelSlider.Updated_by = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(modelSlider).State = EntityState.Modified;
            db.SaveChanges();
            Notification.set_flash("Khôi phục thành công!" + " ID = " + id, "success");
            return RedirectToAction("Trash");

        }

        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            MSlider mSlider = db.Sliders.Find(id);
            mSlider.Status = (mSlider.Status == 1) ? 2 : 1;

            mSlider.Updated_at = DateTime.Now;
            mSlider.Updated_by = 1;
            db.Entry(mSlider).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { Status = mSlider.Status });
        }
        // GET: Admin/Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MSlider modelSlider = db.Sliders.Find(id);
            if (modelSlider == null)
            {
                return HttpNotFound();
            }
            return View(modelSlider);
        }

        // POST: Admin/Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MSlider modelSlider = db.Sliders.Find(id);
            db.Sliders.Remove(modelSlider);
            db.SaveChanges();
            Notification.set_flash("Đã xóa vĩnh viễn slider!", "success");
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
