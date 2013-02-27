using System.Web.Mvc;
using MobileViewEngines.MVC3;
 
[assembly: WebActivator.PreApplicationStartMethod(typeof(WhereShouldWeEatLunch.MobileViewEngines), "Start")]
namespace WhereShouldWeEatLunch {
    public static class MobileViewEngines{
        public static void Start() 
        {
            ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine());
        }
    }
}