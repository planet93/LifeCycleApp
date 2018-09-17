using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LifeCycleApp.Models;

namespace LifeCycleApp.Handlers
{
    public class UserInfoAsyncHandler:HttpTaskAsyncHandler
    {
        //асинхронный поиск записи в бд по Id
        public override async Task ProcessRequestAsync(HttpContext context)
        {
            string idString = context.Request.Url.Segments[2];
            int id;
            Int32.TryParse(idString, out id);
            User user;
            string result = "";
            //UserContext db = new UserContext();
            using (UserContext db = new UserContext())
            {
                user = await db.Users.FindAsync(id);
            }
            //List<User> Ulist = db.Users.ToList();
            //foreach(var u in Ulist)
            //{
            //    result += "<p>Id=" + u.Id.ToString() + " Name: " + u.Name + "</p>";
            //}
            if (user != null)
            {
                result += "<p>Id=" + user.Id.ToString() + " Name: " + user.Name + "</p>";
            }
            context.Response.Write(result);
        }
    }
}