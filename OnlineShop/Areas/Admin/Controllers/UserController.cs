using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;
using OnlineShop.Common;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int size = 1)
        {
            var dao = new UserDAO();
            var model = dao.ListAllPaging(page, size);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var encryptedPass = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedPass;
                long id = dao.Insert(user);
                if (id > 0)
                    return RedirectToAction("Index", "User");
                else
                    ModelState.AddModelError("", "Thêm thành công");
            }
            return View("Index");
        }
    }
}