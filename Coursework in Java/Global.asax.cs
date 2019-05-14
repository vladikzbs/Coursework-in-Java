using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Coursework_in_Java.AppKernel.DatabaseConfigurations;
using Coursework_in_Java.Models;

namespace Coursework_in_Java
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CreateAndIninitializeDbIfNotExist();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        private void CreateAndIninitializeDbIfNotExist()
        {
            Database.SetInitializer(new DbInitilizer());
            var db = new ApplicationDbContext();
            db.SaveChanges();
            db.Dispose();
        }
    }
}
