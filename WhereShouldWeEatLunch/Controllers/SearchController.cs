using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhereShouldWeEatLunch.Controllers
{
    public class SearchViewModel
    {
        public Boolean IsWalkingDistance { get; set; }
    }
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index(FormCollection values)
        {
            SearchViewModel viewModel;
            if (values.GetValues("IsWalkingDistance") != null)
            {
                viewModel = new SearchViewModel()
                                {
                                    IsWalkingDistance = values.GetValues("IsWalkingDistance").Contains("true")
                                };
            }
            else
                viewModel = new SearchViewModel();
            
            return View(viewModel);
        }
        
       
    }
}
