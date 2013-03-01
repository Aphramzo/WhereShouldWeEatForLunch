using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
       
    }
}
