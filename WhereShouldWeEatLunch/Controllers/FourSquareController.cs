using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WhereShouldWeEatLunch.Models;

namespace WhereShouldWeEatLunch.Controllers
{
    public class FourSquareController : BaseController
    {
        //
        // GET: /FourSquare/

        public ActionResult Index()
        {
            var categories = APIs.FourSquare.API.GetCategories();
            categories.Add(new APIs.FourSquare.VenueCategory() { id = "0", name = "(anything)" });
            var viewModel = new FourSquareViewModel() { Categories = categories.OrderBy(x => x.name).Select(c => new SelectListItem() { Value = c.id, Text = c.name, Selected = c.id == "0" }) };
            return View(viewModel);
        }

        public String FourSquareListByCoords()
        {
            var lat = Convert.ToDouble(Request.Params["lat"]);
            var lon = Convert.ToDouble(Request.Params["long"]);
            var eateriesNearHere = APIs.FourSquare.API.FindEateriesNearLatLong(lat, lon, Server.UrlDecode(Request.Params["categoryId"])).OrderBy(x => x.Distance);
            var loggedIn = IsLoggedIn();
            var bananaTime = new BananaTime()
                                 {
                                     Eateries = eateriesNearHere,
                                     LoggedIn = loggedIn
                                 };
            var json = new JavaScriptSerializer().Serialize(bananaTime);
            return json;
        }

        class BananaTime
        {
            public IOrderedEnumerable<APIs.FourSquare.Venue> Eateries { get; set; }
            public bool LoggedIn { get; set; } 
        }

        //
        // GET: /FourSquare/Details/5

        public ActionResult Details(string id)
        {
            var venue = APIs.FourSquare.API.GetVenue(id);
            return View(venue);
        }

        
    }
}
