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
        public ActionResult Index(string searchString,int page = 1, int size = 10)
        {
            var dao = new UserDAO();
            var model = dao.ListAllPaging(searchString, page, size);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var user = new UserDAO().Detail(Convert.ToInt32(id));
            return View(user);
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

        [HttpPost]
        public ActionResult Edit(User user)
        { 
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                bool result = dao.Update(user);
                if (result)
                    return RedirectToAction("Index", "User");
                else
                    ModelState.AddModelError("", "Cập nhật thành công");
            }
            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            new UserDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}