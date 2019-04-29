using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBBS.Service;
using EBBS.Data;
using EBBS.Service.IService;
using EBBS.Service.Service;
using PagedList;


namespace EBBS.Controllers
{ 
    public class StatisticsController : Controller
    {
        private IUserService userService;
        private IPostService postService;
        private ICommentService commentService;

        public StatisticsController() {
            userService = new UserService();
            postService = new PostService();
            commentService = new CommentService();
             
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

       


        // GET: Statistics
        public ActionResult Index()
        {
            if (this.GetUserSession() == null) {
                RedirectToAction("/Account/Login");
            }

            List<string> months = new List<string>();
            List<int> postsInMonths = new List<int>();
            foreach (var post in postService.GetAllPosts())
            {
                string month = post.createTime.Value.Month.ToString();
                string year = post.createTime.Value.Year.ToString();
                string toWrite = month + "-" + year;
                if (!months.Contains(toWrite))
                {
                    months.Add(toWrite);
                }
            } 
            ViewBag.labels = months;

            

            for (int i = 0; i < months.Count; i++) {
                string[] param = months[i].Split('-').ToArray();
                int nPosts = postService.AllPostsInThisMonthAndYear(param[0], param[1]);
                postsInMonths.Add(nPosts);
            }

            ViewBag.nOfPosts = postsInMonths;

            return View();
        }

        public ActionResult UsersStats() {
            int allUsers = userService.NoAllUsers();
            int usersInLastMonth = userService.UsersInLastMonth();

            ViewBag.allUsers = allUsers;
            ViewBag.usersThisMonth = usersInLastMonth;


            return View();
        }
    }
}