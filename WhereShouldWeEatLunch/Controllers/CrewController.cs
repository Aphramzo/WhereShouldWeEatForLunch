using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhereShouldWeEatLunch.Models;

namespace WhereShouldWeEatLunch.Controllers
{
    public class CrewController : Controller
    {
        //
        // GET: /Crew/
        private WhereShouldWeEatLunchContext db = new WhereShouldWeEatLunchContext();
        public ViewResult Details(int id)
        {
            CrewModel crewModel = db.CrewModels.Find(id);
            return View(crewModel);
        }

        
    }
}
