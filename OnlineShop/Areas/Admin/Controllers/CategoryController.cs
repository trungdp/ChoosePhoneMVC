using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int size = 10)
        {
            var dao = new CategoryDAO();
            var model = dao.ListAllPaging(searchString, page, size);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                long id = dao.Insert(category);
                if (id > 0)
                    return RedirectToAction("Index", "Category");
                else
                    ModelState.AddModelError("", "Thêm thành công");
            }
            return View("Index");
        }
    }
}