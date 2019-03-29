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
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }
    }
}