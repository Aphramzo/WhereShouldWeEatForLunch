using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhereShouldWeEatLunch.Models;

namespace WhereShouldWeEatLunch.Controllers
{
    public class SearchViewModel
    {
        public Boolean IsWalkingDistance { get; set; }
        public List<EateryModel> Eateries { get; set; }
        public EateryModel SuggestedEatery { get; set; }
    }
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        private WhereShouldWeEatLunchContext db = new WhereShouldWeEatLunchContext();
        public ActionResult Index(FormCollection values)
        {
            var viewModel = new SearchViewModel();
            var eateries = new List<EateryModel>();
            viewModel.Eateries = eateries;
            if (values.GetValues("IsWalkingDistance") != null)
            {
                var isWalkingDistance = values.GetValues("IsWalkingDistance").Contains("true");
                viewModel.IsWalkingDistance = isWalkingDistance;
                viewModel.Eateries =
                    db.EateryModels.Where(x => x.IsWalkingDistance == isWalkingDistance).OrderBy(c => c.Name).ToList();
                viewModel.SuggestedEatery = GetSuggestionFromList(viewModel.Eateries);
            }
            
            return View(viewModel);
        }

        private EateryModel GetSuggestionFromList(List<EateryModel> eateries)
        {
            var rand = new Random();
            return eateries.ElementAt(rand.Next(0, eateries.Count));
        }
    }
}
