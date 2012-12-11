﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhereShouldWeEatLunch.Models;

namespace WhereShouldWeEatLunch.Controllers
{ 
    public class EateryController : Controller
    {
        private WhereShouldWeEatLunchContext db = new WhereShouldWeEatLunchContext();


        public class EateryViewModel
        {
            public EateryModel Eatery { get; set; }
            public IEnumerable<SelectListItem> Styles;
        }

        public ViewResult Index()
        {
            return View(db.EateryModels.ToList());
        }

        //
        // GET: /Eatery/Details/5

        public ViewResult Details(int id)
        {
            EateryModel eaterymodel = db.EateryModels.Find(id);
            return View(eaterymodel);
        }

        //
        // GET: /Eatery/Create

        public ActionResult Create()
        {
            var viewModel = CreateViewModelFromEntityModel(null, null);
            return View(viewModel);

        } 

        //
        // POST: /Eatery/Create

        [HttpPost]
        public ActionResult Create(EateryViewModel eaterymodel)
        {

            var eatery = eaterymodel.Eatery;
            eatery.FoodStyleModel = db.FoodStyleModels.FirstOrDefault(x => x.Id == eatery.FoodStyleModel.Id);
            db.EateryModels.Add(eatery);
            db.SaveChanges();
            return RedirectToAction("Index");  
        }

        private EateryViewModel CreateViewModelFromEntityModel(EateryModel eaterymodel, FoodStyleModel selected)
        {
            var viewModel = new EateryViewModel()
                                {
                                    Eatery = eaterymodel,
                                    Styles = new SelectList(db.FoodStyleModels.OrderBy(x => x.Name), "Id", "Name", selected)
                                };
            return viewModel;
        }

        //
        // GET: /Eatery/Edit/5
 
        public ActionResult Edit(int id)
        {
            EateryModel eaterymodel = db.EateryModels.Find(id);
            var viewModel = CreateViewModelFromEntityModel(eaterymodel, eaterymodel.FoodStyleModel);
            return View(viewModel);
        }

        //
        // POST: /Eatery/Edit/5

        [HttpPost]
        public ActionResult Edit(EateryModel eaterymodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eaterymodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewModel = CreateViewModelFromEntityModel(eaterymodel, eaterymodel.FoodStyleModel);
            return View(viewModel);
        }

        //
        // GET: /Eatery/Delete/5
 
        public ActionResult Delete(int id)
        {
            EateryModel eaterymodel = db.EateryModels.Find(id);
            return View(eaterymodel);
        }

        //
        // POST: /Eatery/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            EateryModel eaterymodel = db.EateryModels.Find(id);
            db.EateryModels.Remove(eaterymodel);
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