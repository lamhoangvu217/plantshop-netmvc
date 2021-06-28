using ElectroShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectroShop.Controllers
{
    public class ModuleController : Controller
    {
        private ElectroShopDbContext db = new ElectroShopDbContext();
        // GET: Module
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header()
        {
            return View("_Header");
        }
        
        public ActionResult CategorySearch()
        {
            var list = db.Categorys
               .Where(m => m.Status == 1 && m.ParentId == 0);
            return View("_CategorySearch",list);
        }
        public ActionResult Cart()
        {
            return View("_Cart");
        }
        public ActionResult Navbar()
        {
            return View("_Navbar");
        }
        public ActionResult ProductView()
        {
            return View("_ProductView");
        }
        public ActionResult LatestNew()
        {
            var list = db.Posts
               .Where(m => m.Status == 1 && m.Type == "post")
               .Take(3)
               .OrderByDescending(m => m.Created_At )
               .ToList();
            return View("_LatestNew", list);
        }

        public ActionResult LatestNewListing()
        {
            var list = db.Posts
               .Where(m => m.Status == 1 && m.Type == "post")
               .Take(8)
               .OrderByDescending(m => m.Created_At)
               .ToList();
            return View("_LatestNewListing", list);
        }

        public ActionResult Brand()
        {
            return View("_Brand");
        }
        public ActionResult CompanyFacality()
        {
            return View("_CompanyFacality");
        }
        public ActionResult Footer()
        {
            return View("_Footer");
        }
        public ActionResult Copyright()
        {
            return View("_Copyright");
        }
        public ActionResult LinkHeader()
        {
            return View("_LinkHeader");
        }
        public ActionResult LinkFooter()
        {
            return View("_LinkFooter");
        }
        public ActionResult Category()
        {
            var list = db.Categorys
                .Where(m => m.Status == 1 && m.ParentId == 0);
            return View("_Category", list);
        }

        public ActionResult CategoryChild(int id)
        {
            var list = db.Categorys
                .Where(m => m.Status == 1 && m.ParentId == id);
            return View("_CategoryChild", list);
        }

        public ActionResult CategoryFooter()
        {
            var list = db.Categorys
                .Where(m => m.Status == 1 && m.ParentId == 0);
            return View("_CategoryFooter", list);
        }
        public ActionResult ProductNew()
        {
            var list = db.Products
               .Where(m => m.Status == 1 && m.Discount != 0)
               .Take(9)
               .ToList();
            return View("_ProductNew", list);
        }

        public ActionResult Sale()
        {
            var list = db.Products
               .Where(m => m.Status == 1)
               .Take(6)
               .OrderByDescending(m=>m.Created_at)
               .ToList();
            return View("_Sale", list);
        }
        public ActionResult SlideShow()
        {
            return View("_SlideShow",db.Sliders.Where(m=>m.Status!=0).ToList());
        }
        public ActionResult ListPage()
        {
            var list = db.Posts
              .Where(m => m.Status == 1 && m.Type=="page")
              .OrderByDescending(m => m.Created_At)
              .ToList();
            return View("_ListPage",list);
        }
        public ActionResult MainMenu()
        {
            var list = db.Menus
              .Where(m => m.Status == 1 && m.Position=="mainmenu")
              .ToList();
            return View("_MainMenu", list);
        }

        public ActionResult MainMenuWithCategory()
        {
            var listCat = db.Categorys
              .Where(m => m.Status == 1 && m.ParentId == 0);
          
            return View("_MainMenuWithCategory", listCat);
        }
        public ActionResult MainMenuMobile()
        {
            var list = db.Menus
              .Where(m => m.Status == 1 && m.Type == "custom")
              .ToList();
            return View("_MainMenuMobile", list);
        }
        public ActionResult ListCate()
        {
            return View("_ListCate", db.Categorys.Where(m => m.Status != 0 && m.ParentId==0).ToList());
        }
        public ActionResult Posts()
        {
            var list = db.Topics 
                .Where(m => m.Status == 1 && m.ParentId == 0);
            return View("Posts", list);
        }
        public ActionResult PostHome(int topid)
        {
            List<int> listtopid = new List<int>();
            listtopid.Add(topid);

            var list2 = db.Topics
                .Where(m => m.ParentId == topid).Select(m => m.Id)
                .ToList();
            foreach (var id2 in list2)
            {
                listtopid.Add(id2);
                var list3 = db.Topics
                    .Where(m => m.ParentId == id2)
                    .Select(m => m.Id).ToList();
                foreach (var id3 in list3)
                {
                    listtopid.Add(id3);
                }
            }

            var list = db.Posts
                .Where(m => m.Status == 1 && listtopid
                .Contains(m.Topid))
                .Take(12)
                .OrderByDescending(m => m.Created_At);

            return View("PostHome", list);
        }
        public ActionResult ListTopic()
        {
            return View("ListTopic", db.Topics.Where(m => m.Status != 0 && m.ParentId == 0).ToList());
        }
        public ActionResult PListPage()
        {
            var list = db.Posts
                .Where(m => m.Status == 1 && m.Type == "page");
            return View("PListPage", list);
        }
    }
}