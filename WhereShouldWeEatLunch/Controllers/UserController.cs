using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using WhereShouldWeEatLunch.Models;

namespace WhereShouldWeEatLunch.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private WhereShouldWeEatLunchContext db = new WhereShouldWeEatLunchContext();
        public ActionResult Index()
        {
            return View(db.UserModels.ToList());
        }

        public ActionResult SignUp()
        {

            return View("Create");
        }

        [HttpPost]
        public ActionResult SignUp(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                db.UserModels.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create",userModel);
        }
       

        //

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {
            var user = db.UserModels.FirstOrDefault(c => c.EmailAddress == userModel.EmailAddress);
            if(user != null)
            {
                Session["User"] = user;
                Session["UserName"] = user.FirstName;
                return RedirectToAction("Index", "FourSquare");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            Session["UserName"] = null;
            return RedirectToAction("Index", "FourSquare");
        }
    }
}
