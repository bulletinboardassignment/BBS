using EBBS.Data;
using EBBS.Service.IService;
using EBBS.Service.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace EBBS.Controllers
{ 
    public class PostController : Controller
    {
        private IPostService postService;
        private ICategoryService categoryService;
        private IReportService reportService;
        private ICommentService commentService;


        public PostController() {
            postService = new PostService();
            categoryService = new CategoryService();
            reportService = new ReportService();
            commentService = new CommentService();
        }
        //get the user session data
        private User GetUserSession()
        {
            if (Session["lUser"] != null)
            {
                User user = (User)Session["lUser"];
                return user;
            }
            else {
                return null;
            }
        }

        //show all posts
        public ActionResult Index(int? page)
            {
            //get the posts in descending time order
            var posts = postService.GetAllPosts().OrderByDescending(p => p.createTime);

            foreach (var post in posts)
            {
                post.nLikes = postService.GetNumberOfLikes(post.pId);
                post.nDislikes = postService.GetNumberOfDislikes(post.pId);
                post.nComments = postService.GetNumberOfComments(post.pId);
            }
            //pass all posts to the view and paginate them
            ViewBag.userType = this.GetUserSession().userId;

            //set the post to show 3 post per page
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        //get details of a post
        // GET: Post/Details/5
        public ActionResult Details(int id)
        { //data for categories dropdown
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var item in categoryService.GetAllCategories())
            {
                categories.Add(new SelectListItem()
                {
                    Text = item.categoryName,
                    Value = item.cId.ToString()
                });
            }
            ViewBag.categories = categories;
            ViewBag.currentUser = this.GetUserSession().userId;
            //set the session to be used in case the user adds or edits a comment to the page

            Session["pid"] = id;

            List<Comment> commentsOfPost = commentService.GetAllCommentsForPost(id);
            ViewBag.comments = commentsOfPost;

            return View(postService.GetPost(id));
        }

        // GET: Post/Create
        //create a new post
        public ActionResult Create()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var item in categoryService.GetAllCategories()) {
                categories.Add(new SelectListItem() {
                    Text = item.categoryName,
                    Value = item.cId.ToString()
                });
            }
            ViewBag.categories = categories;
            return View();
        }

        //Asynchronous Json controller in case the user reports a post
        // POST: Post/Report
        [HttpPost]
        public JsonResult Report(Models.ReportModelView reportModel)
        {
            postService.ReportPost(reportModel.postId);
            //add a report to the reports table and also set the
            //isreported attribute of that post to true
            Reports report = new Reports();
                report.postId = reportModel.postId;
                report.reason = reportModel.reason;
                report.reportedBy = GetUserSession().userId;
                reportService.Add(report);
                string result = "You have been successfully reported the post";
                return Json(result);
        }


        //Asynchronous Json controller in case the user likes a post
        // POST: Post/Like
        [HttpPost]
        public JsonResult Like(Models.LikeViewModel likeModel) {
            Like like = new Like();
            like.likedBy = this.GetUserSession().userId;
            like.likedOn = DateTime.Now;
            like.vote = likeModel.vote;
            like.postId = likeModel.postId;
            //add a Like vote to the Like table
            postService.LikePost(like);

            string result = "You liked it";
            return Json(result);
        }
        //Asynchronous Json controller in case the user dislikes a post
        // POST: Post/Dislike
        [HttpPost]
        public JsonResult Dislike(Models.LikeViewModel likeModel)
        {
            Like like = new Like();
            like.likedBy = this.GetUserSession().userId;
            like.likedOn = DateTime.Now;
            like.vote = likeModel.vote;
            like.postId = likeModel.postId;
            //add a dislike vote for this post in like table
            postService.DislikePost(like);

            string result = "You disliked it";
            return Json(result);
        }

               
        // POST: Post/Create
        [HttpPost]
        public JsonResult Create(Models.PostModel post)
        {//check if the model coming from our view contains any image too
            var image = post.postImage;
            Post savePost = new Post();
            if (image != null)
            {//if post contains some image, grab the following data form the image

                var fileName = Path.GetFileName(image.FileName);
                var extension = Path.GetExtension(image.FileName);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(image.FileName);

                image.SaveAs(Server.MapPath("/images/") + image.FileName);
                savePost.mediaPath = fileName.ToString();
                var contentType = image.ContentType.ToString();
                savePost.mediaType = contentType;
            }
            //if not, just leave it blank
            else
            {
                savePost.mediaPath = "nothing";
            }
            //get the user id as well because we want to store userId for this post

            User user = GetUserSession();
            if (user != null) {
                savePost.creatorId = user.userId;
            }
            savePost.categoryId = post.categoryId;
            savePost.createTime = DateTime.Now;
            savePost.postTitle = post.postTitle;
            savePost.postContent = post.postContent;
            string result = "";
            try
            {
                postService.Add(savePost);
                result = "Post Created successfully!";
            }
            catch (Exception e) {
                result = e.ToString();
            }
            //return the json result to the view
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Post/Edit/5
        //edit post page
        public ActionResult Edit(int id)
        { //categories list for dropdown
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var item in categoryService.GetAllCategories())
            {
                categories.Add(new SelectListItem()
                {
                    Text = item.categoryName,
                    Value = item.cId.ToString()
                });
            }
            ViewBag.categories = categories;
            return View(postService.GetPost(id));
        }

        // POST: Post/Edit/5

        //Method to handle an edited post asynchronously
        [HttpPost]
        public JsonResult Edit(int id, Models.PostModel post)
        {
                // TODO: Add update logic here
                Post newPost = new Post();
                var image = post.postImage;
            //if the new post contains any image, grab its data
            if (image != null)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(image.FileName);
                    var contentType = image.ContentType.ToString();
                    newPost.mediaType = contentType;
                    newPost.mediaPath = fileName.ToString();
                    image.SaveAs(Server.MapPath("/images/") + image.FileName);
                    
                    
                    
                }
                User user = GetUserSession();
                if (user != null)
                {
                    newPost.creatorId = user.userId;
                }

                newPost.categoryId = post.categoryId;
                newPost.updateTime = DateTime.Now;
                newPost.postTitle = post.postTitle;
                newPost.postContent = post.postContent;
            //edit the post and send a success message
            postService.Edit(id, newPost);
            string result = "ok success";
            return Json(result, JsonRequestBehavior.AllowGet);

            }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View(postService.GetPost(id));
        }

        // POST: Post/Delete/5
        //delete a post asynchronously
        [HttpPost]
        public JsonResult DeletePost(int id)
        {
            string result = "";
             
                // TODO: Add delete logic here
                postService.DeleteCommentsForPost(id);
                postService.Delete(id);
                result = "You successfully deleted a post.";
            

            return Json(result);
        }
        //Get all the posts with userId the same as current user
        public ActionResult MyPosts(int? page)
        {

            int userId = GetUserSession().userId;
            var postsByUser = postService.GetAllPostsByUser(userId).OrderByDescending(p => p.createTime); ;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(postsByUser.ToPagedList(pageNumber, pageSize));
        }


        //grab the posts in each category
        public ActionResult CategoryPosts(int id) {

            List<Post> postsInCategory = postService.GetAllPostsInCategory(id);

            return View(postsInCategory);
        }



        // GET: Post/EditMyPost/5
        //Edit the post display in My post page
        public ActionResult EditMyPost(int id)
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var item in categoryService.GetAllCategories())
            {
                categories.Add(new SelectListItem()
                {
                    Text = item.categoryName,
                    Value = item.cId.ToString()
                });
            }
            ViewBag.categories = categories;
            return View(postService.GetPost(id));
        }

        // POST: Post/EditMyPost/5
        [HttpPost]
        public JsonResult EditMyPost(int id, Models.PostModel post)
        {
            // TODO: Add update logic here
            Post newPost = new Post();
            var image = post.postImage;
            if (image != null)
            {
                var fileName = Path.GetFileName(image.FileName);
                var extension = Path.GetExtension(image.FileName);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(image.FileName);
                var contentType = image.ContentType.ToString();
                newPost.mediaType = contentType;
                newPost.mediaPath = fileName.ToString();
                image.SaveAs(Server.MapPath("/images/") + image.FileName);



            }
            User user = GetUserSession();
            if (user != null)
            {
                newPost.creatorId = user.userId;
            }

            newPost.categoryId = post.categoryId;
            newPost.updateTime = DateTime.Now;
            newPost.postTitle = post.postTitle;
            newPost.postContent = post.postContent;

            postService.Edit(id, newPost);
            string result = "ok success";
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        //// GET: Post/DeleteMyPost/5
      //Delete the post display in My post page
        public ActionResult DeleteMyPost(int id)
        {
            return View(postService.GetPost(id));
        }

        // POST: Post/Delete/5
        [HttpPost]
        public JsonResult MyPostDelete(int id)
        {
            string result = "";

            // TODO: Add delete logic here
            postService.DeleteCommentsForPost(id);
            postService.Delete(id);
            result = "You successfully deleted a post.";


            return Json(result);
        }

        // GET: Post/Details/5

        // GET: Post/MyDetails/5
        //The post lists display in My post page
        public ActionResult MyDetails(int id)
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var item in categoryService.GetAllCategories())
            {
                categories.Add(new SelectListItem()
                {
                    Text = item.categoryName,
                    Value = item.cId.ToString()
                });
            }
            ViewBag.categories = categories;
            ViewBag.currentUser = this.GetUserSession().userId;

            Session["pid"] = id;

            List<Comment> commentsOfPost = commentService.GetAllCommentsForPost(id);
            ViewBag.comments = commentsOfPost;

            return View(postService.GetPost(id));
        }

    }
}
