using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LifeCycleApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserContext: DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}