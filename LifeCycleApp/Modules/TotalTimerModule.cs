using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeCycleApp.Modules
{
    public class TotalTimerModule:IHttpModule
    {
        private static float totalTime = 0;
        private static int requestCount = 0;
        public void Init(HttpApplication app)
        {
            IHttpModule module = app.Modules["Timer"];
            if(module != null && module is TimerModules)
            {
                TimerModules timerModules = (TimerModules)module;
                timerModules.RequestTimer += HandleRequestTimer;
            }
            app.EndRequest += HandleEndRequest;
        }
        public void HandleRequestTimer(object src, RequestTimerEventArgs e)
        {
            totalTime += e.Duration;
            requestCount++;
        }
        private void HandleEndRequest(object src, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            string result = string.Format(@"<div style='color:red;'>Количество обращений: {0} </div>+
                <div style='color:red;'>Общее время обработки запросов: {1:F5} секунд </div>", requestCount, totalTime);
            context.Response.Write(result);
        }
        public void Dispose() { }
    }
}