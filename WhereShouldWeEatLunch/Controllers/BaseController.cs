using System.Web.Mvc;
using System.Web.Routing;

namespace WhereShouldWeEatLunch.Controllers
{
    public class BaseController : Controller
    {
        
        public bool IsLoggedIn()
        {
            return Session["User"] != null;
        }
    }
}