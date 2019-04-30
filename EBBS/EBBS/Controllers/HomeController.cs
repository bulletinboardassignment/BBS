using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EBBS.Controllers
{
    public class HomeController : Controller
    {
       //Controller for About page
        public ActionResult About()
        {
                      
            return View();
        }
        //Controller for Contact Page
        public ActionResult Contact()
        {
               return View();
        }

    }
}