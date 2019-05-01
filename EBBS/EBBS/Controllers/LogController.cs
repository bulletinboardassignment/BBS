using EBBS.Data;
using EBBS.Service.IService;
using EBBS.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace EBBS.Controllers
{//controller for storing user logs
    public class LogController : Controller
    {
        private ILogService logService;
        private IUserService userService;
        private EbbSEntities db;

        public LogController() {
            logService = new LogService();
            userService = new UserService();
            db = new EbbSEntities();
        }


        // GET: Log/Index
        public ActionResult Index(int? page)
        {
            //get all logs and display the record sorted descending by the loggedTime
            var result = logService.GetAllLogs().OrderByDescending(p => p.loggedTime);
            //show 20 logs per page 
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));

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