using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBS.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error

            //Controller for retrive the 404 error page
        public ActionResult ErrorMessage()
        {
            return View();
        }
    }
}