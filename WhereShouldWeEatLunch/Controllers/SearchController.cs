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
        public IEnumerable<SelectListItem> Styles { get; set; } 
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
            if (values.GetValues("IsWalkingDistance") != null)
            {
                setFormValues(values, viewModel);
            }
            else
                viewModel.Styles = GetStyleListOptions();
            
            return View(viewModel);
        }

        private void setFormValues(FormCollection values, SearchViewModel viewModel)
        {
            var isWalkingDistance = GetIsWalkingDistanceFromForm(values);
            var style = GetStyleFromForm(values);
            viewModel.IsWalkingDistance = isWalkingDistance;
            viewModel.Eateries =
                GetPossibleEateryList(isWalkingDistance, style);
            viewModel.SuggestedEatery = GetSuggestionFromList(viewModel.Eateries);
            viewModel.Styles = GetStyleListOptions(style);
        }

        private List<EateryModel> GetPossibleEateryList(bool isWalkingDistance, FoodStyleModel style)
        {
            return db.EateryModels.Where(x => x.IsWalkingDistance == isWalkingDistance && x.FoodStyleModel.Id == style.Id).OrderBy(c => c.Name).ToList();
        }

        private FoodStyleModel GetStyleFromForm(FormCollection values)
        {
            var styleId = Convert.ToInt32(values.GetValue("foodStyle").AttemptedValue);
            return db.FoodStyleModels.Where(x => x.Id == styleId).FirstOrDefault();
        }

        private static bool GetIsWalkingDistanceFromForm(FormCollection values)
        {
            return values.GetValues("IsWalkingDistance").Contains("true");
        }

        private IEnumerable<SelectListItem> GetStyleListOptions(FoodStyleModel style)
        {
            var styles = db.FoodStyleModels.OrderBy(x => x.Name).ToList();

            return styles.Select(c=> new SelectListItem(){Value = c.Id.ToString(), Text = c.Name, Selected = style.Id == c.Id});
        }

        private IEnumerable<SelectListItem> GetStyleListOptions()
        {
            return GetStyleListOptions(new FoodStyleModel());
        } 

        private EateryModel GetSuggestionFromList(List<EateryModel> eateries)
        {
            if(eateries.Count == 0)
                return new EateryModel(){Name = "Well shit, Mr. Picky Pants over here. There are no places that match your criteria."};
            var rand = new Random();
            return eateries.ElementAt(rand.Next(0, eateries.Count));
        }
    }
}
