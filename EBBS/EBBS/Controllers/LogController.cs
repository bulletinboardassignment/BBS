using EBBS.Data;
using EBBS.Service.IService;
using EBBS.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBS.Controllers
{
    public class LogController : Controller
    {
        private ILogService logService;
        private IUserService userService;

        public LogController() {
            logService = new LogService();
            userService = new UserService();
        }


        // GET: Log
        public ActionResult Index()
        {
            return View(logService.GetAllLogs());
        }

        public ActionResult Welcome() {
            return View(userService.GetUser(this.GetUserSession().userId));
        }

        private User GetUserSession()
        {
            if (Session["lUser"] != null)
            {
                User user = (User)Session["lUser"];
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}