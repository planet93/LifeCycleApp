using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeCycleApp.Handlers;

namespace LifeCycleApp.Modules
{
    public class HandlerSelectionModule: IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.PostResolveRequestCache += (src, args) =>
             {
                 if (string.Equals((string)app.Context.Request.RequestContext.RouteData.Values["controller"], "Home", StringComparison.OrdinalIgnoreCase) &&
                 string.Equals((string)app.Context.Request.RequestContext.RouteData.Values["action"], "Index", StringComparison.OrdinalIgnoreCase))
                 {
                     app.Context.RemapHandler(new UserInfoHandler());
                 }
             };

            app.EndRequest += (src, args) =>
            {
                app.Context.Response.Write("<p>Handler: " + app.Context.Handler.ToString() + "</p>");
            };
        }
        public void Dispose() { }
    }
}