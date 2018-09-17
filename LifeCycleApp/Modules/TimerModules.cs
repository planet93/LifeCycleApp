using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;

namespace LifeCycleApp.Modules
{
    public class RequestTimerEventArgs : EventArgs
    {
        public float Duration { get; set; }
    }
    public class TimerModules:IHttpModule
    {
        private Stopwatch timer;
        long interval = 30000;
        static Timer timer1; 
        static object synclock = new object();
        static bool sent = false;
        public event EventHandler<RequestTimerEventArgs> RequestTimer;

        public void Init(HttpApplication app)
        {
            app.BeginRequest += HandleBeginRequest;
            app.EndRequest += HandleEndRequest;
            timer1 = new Timer(new TimerCallback(SendEmail), null, 0, interval);
        }
        private void HandleBeginRequest(object src, EventArgs args)
        {
            timer = Stopwatch.StartNew();
        }

        private void SendEmail(object obj)
        {
            lock (synclock)
            {
                DateTime dd = DateTime.Now;
                if(sent == false)
                {
                    SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.yandex.ru", 25);
                    
                    smtp.Credentials = new System.Net.NetworkCredential("sergey.ermolaev@base4web.ru", "vatt135790");
                    smtp.EnableSsl = true;

                    MailAddress from = new MailAddress("sergey.ermolaev@base4web.ru", "Test");
                    MailAddress to = new MailAddress("sergey.ermolaev@base4web.ru");

                    MailMessage m = new MailMessage(from, to);

                    m.Subject = "Test mail";

                    m.Body = "Рассылка писем с сайта";
                    smtp.Send(m);
                    sent = true;
                }
                else if (dd.Hour != 1 && dd.Minute != 30)
                {
                    sent = false;
                }
            }
        }

        private void HandleEndRequest(object src,EventArgs args)
        {
            HttpContext context = HttpContext.Current;
            float duration = (float)timer.ElapsedTicks / Stopwatch.Frequency;
            context.Response.Write(string.Format("<div style='color:red;'>Время обработки запроса: {0:F5} секунд</div>", duration));
            if(RequestTimer != null)
            {
                RequestTimer(this, new RequestTimerEventArgs { Duration = duration });
            }
        }
        public void Dispose() { }

    }
}