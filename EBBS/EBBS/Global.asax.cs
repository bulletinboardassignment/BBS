using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EBBS.Models;

namespace EBBS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //configuration set for showing the 404 error page
        protected void Application_Error (object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();//get last server error and assign to the exception variable
            Server.ClearError();//clears the last exception that was thrown.
            Response.Redirect("/Error/ErrorMessage");//If exception occured, 404 error page shows
        }
    }
}
