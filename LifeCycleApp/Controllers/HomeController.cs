using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeCycleApp.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //Покажем обработанные события
        public string Index()
        {
            List<string> events = HttpContext.Application["events"] as List<string>;
            string result = "<ul>";
            foreach(string e in events)
            {
                result += "<li>" + e + "</li>";
            }
            result += "</ul>";

            result += "<p><a href='Home/Modules'>Как сделать такое же фото?</a></p>";
            return result;
        }

        //Покажет подлюченные модули в нашем приложении
        public ActionResult Modules()
        {
            var modules = HttpContext.ApplicationInstance.Modules;
            string[] modArray = modules.AllKeys;
            return View(modArray);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}