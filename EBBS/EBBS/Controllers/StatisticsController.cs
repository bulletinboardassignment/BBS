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
        //get current user's data
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
            //create a months list and fill it with the months in which
            //somebody has created a post in
            List<string> months = new List<string>();
            List<int> postsInMonths = new List<int>();
            foreach (var post in postService.GetAllPosts())
            {
                string month = post.createTime.Value.Month.ToString();
                string year = post.createTime.Value.Year.ToString();
                string toWrite = month + "-" + year;
                //check if the record is unique
                if (!months.Contains(toWrite))
                {
                    months.Add(toWrite);
                }
            }
            //pass it to view to be used in graphs
            ViewBag.labels = months;


            //grab the number of posts that have been created in each month
            for (int i = 0; i < months.Count; i++) {
                string[] param = months[i].Split('-').ToArray();
                int nPosts = postService.AllPostsInThisMonthAndYear(param[0], param[1]);
                postsInMonths.Add(nPosts);
            }
            //passs the number of posts to the view
            ViewBag.nOfPosts = postsInMonths;
            //months will be like ["01-2019", "02,2019"]
            //nOfPosts will be like [7, 11]
            //Since they are in order, we know that the first element in first array correspondes
            //to the first element in the second array and so on.
            return View();
        }

        public ActionResult UsersStats() {
            int allUsers = userService.NoAllUsers();
            int usersInLastMonth = userService.UsersInLastMonth();
            //Get all users and the users in last month from UserService
            ViewBag.allUsers = allUsers;
            ViewBag.usersThisMonth = usersInLastMonth;


            return View();
        }
    }
}