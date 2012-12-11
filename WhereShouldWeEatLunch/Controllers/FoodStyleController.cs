using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhereShouldWeEatLunch.Models;

namespace WhereShouldWeEatLunch.Controllers
{ 
    public class FoodStyleController : Controller
    {
        private WhereShouldWeEatLunchContext db = new WhereShouldWeEatLunchContext();

        //
        // GET: /Default1/

        public ViewResult Index()
        {
            return View(db.FoodStyleModels.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ViewResult Details(int id)
        {
            FoodStyleModel foodstylemodel = db.FoodStyleModels.Find(id);
            return View(foodstylemodel);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(FoodStyleModel foodstylemodel)
        {
            if (ModelState.IsValid)
            {
                db.FoodStyleModels.Add(foodstylemodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(foodstylemodel);
        }
        
        //
        // GET: /Default1/Edit/5
 
        public ActionResult Edit(int id)
        {
            FoodStyleModel foodstylemodel = db.FoodStyleModels.Find(id);
            return View(foodstylemodel);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(FoodStyleModel foodstylemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodstylemodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodstylemodel);
        }

        //
        // GET: /Default1/Delete/5
 
        public ActionResult Delete(int id)
        {
            FoodStyleModel foodstylemodel = db.FoodStyleModels.Find(id);
            return View(foodstylemodel);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            FoodStyleModel foodstylemodel = db.FoodStyleModels.Find(id);
            db.FoodStyleModels.Remove(foodstylemodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}