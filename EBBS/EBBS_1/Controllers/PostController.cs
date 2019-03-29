using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBBS_1.Data;
using EBBS_1.Service.IService;
using EBBS_1.Service.Service;

namespace EBBS_1.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }
    }
}