using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LifeCycleApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            BeginRequest += (src, args) => AddEvent("BeginRequesttt");
            AuthenticateRequest += (src, args) => AddEvent("Authen Request");
            PreRequestHandlerExecute += (src, args) => AddEvent("HandlerExecute");
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddEvent(string name)
        {
            List<string> eventList = Application["events"] as List<string>;
            if(eventList == null)
            {
                Application["events"] = eventList = new List<string>();
            }
            eventList.Add(name);
        }
        protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Write("Application_PostRequestHandlerExecute111");
        }
    }
}
