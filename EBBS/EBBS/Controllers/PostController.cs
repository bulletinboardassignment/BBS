using EBBS.Data;
using EBBS.Service.IService;
using EBBS.Service.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
       
        // GET: Post
        public ActionResult Index()
        {
            //return View(postService.GetAllPosts());
            IList<Post> posts = postService.GetAllPosts();
            foreach (var post in posts)
            {
                post.nLikes = postService.GetNumberOfLikes(post.pId);
                post.nDislikes = postService.GetNumberOfDislikes(post.pId);
                post.nComments = postService.GetNumberOfComments(post.pId);
            }

            ViewBag.userType = this.GetUserSession().userId;

            return View(posts);
            
                      
        }
    

        // GET: Post/Details/5
        public ActionResult Details(int id)
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
         
        // GET: Post/Create
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


        // POST: Post/Report
        [HttpPost]
        public JsonResult Report(Models.ReportModelView reportModel)
        {
            postService.ReportPost(reportModel.postId);

                Reports report = new Reports();
                report.postId = reportModel.postId;
                report.reason = reportModel.reason;
                report.reportedBy = GetUserSession().userId;
                reportService.Add(report);
                string result = "You have been successfully reported the post";
                return Json(result);
        }



        // POST: Post/Like
        [HttpPost]
        public JsonResult Like(Models.LikeViewModel likeModel) {
            Like like = new Like();
            like.likedBy = this.GetUserSession().userId;
            like.likedOn = DateTime.Now;
            like.vote = likeModel.vote;
            like.postId = likeModel.postId;

            postService.LikePost(like);

            string result = "You liked it";
            return Json(result);
        }

        // POST: Post/Dislike
        [HttpPost]
        public JsonResult Dislike(Models.LikeViewModel likeModel)
        {
            Like like = new Like();
            like.likedBy = this.GetUserSession().userId;
            like.likedOn = DateTime.Now;
            like.vote = likeModel.vote;
            like.postId = likeModel.postId;

            postService.DislikePost(like);

            string result = "You disliked it";
            return Json(result);
        }




        // POST: Post/Create
        [HttpPost]
        public JsonResult Create(Models.PostModel post)
        {
            var image = post.postImage;
            Post savePost = new Post();
            if (image != null)
            {
                var fileName = Path.GetFileName(image.FileName);
                var extension = Path.GetExtension(image.FileName);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(image.FileName);

                image.SaveAs(Server.MapPath("/images/") + image.FileName);
                savePost.mediaPath = fileName.ToString();
                var contentType = image.ContentType.ToString();
                savePost.mediaType = contentType;
            }
            else {
                savePost.mediaPath = "nothing";
            }

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
             
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
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

        // POST: Post/Edit/5
        [HttpPost]
        public JsonResult Edit(int id, Models.PostModel post)
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

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View(postService.GetPost(id));
        }

        // POST: Post/Delete/5
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


        public ActionResult MyPosts() {

            int userId = GetUserSession().userId;
            List<Post> postsByUser = postService.GetAllPostsByUser(userId);


            return View(postsByUser);
        }
         
        public ActionResult CategoryPosts(int id) {

            List<Post> postsInCategory = postService.GetAllPostsInCategory(id);

            return View(postsInCategory);
        }



    }
}
