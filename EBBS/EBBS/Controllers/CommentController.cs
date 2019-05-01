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
{
    public class CommentController : Controller
    {
        private ICommentService commentService;
        private IPostService postService;



        public CommentController() {
            commentService = new CommentService();
            postService = new PostService();
        }

        public ActionResult MyComments(int? page)
        {
            int userId = GetUserSession().userId;
            var comments = commentService.GetAllCommentsByUser(userId).OrderByDescending(p => p.createTime); //the "Comments", sorted descending by the "createdTime" 
            int pageSize = 3; //3 Comments per page display
            int pageNumber = (page ?? 1); //Per page
            return View(comments.ToPagedList(pageNumber, pageSize));
        }
       

        public ActionResult Get(int postId) {
            Post post = postService.GetPost(postId);
            ViewBag.post = post;
            List<Comment> comments = commentService.GetAllCommentsForPost(postId);
            return View(comments);
        }

        [HttpPost]
        public JsonResult Comment(Models.CommentViewModel commentViewModel) {
            Comment comment = new Comment();
            comment.commentText = commentViewModel.commentText;
            comment.postId = commentViewModel.postId;
            comment.createTime = DateTime.Now;

            if (GetUserSession() != null) {
                comment.commentedBy = GetUserSession().userId;
            }

            commentService.Add(comment);
            string result = "You commented on the post!";
            return Json(result);
        }


        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

  

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
               return View(commentService.GetComment(id));
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Comment comment)
        {
            try
            {
                // TODO: Add update logic here

                commentService.EditComment(id, comment);

                if (Session["pid"] == null)
                {
                    return RedirectToAction("MyComments", "Comment");
                }
                else
                {

                    int postId = int.Parse(Session["pid"].ToString());
                    return RedirectToAction("Details/"+postId, "Post");

                }

                
            }
            catch
            {
                return View();
            }
        }

     

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {

            return View(commentService.GetComment(id));
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                commentService.DeleteComment(id);
                if (Session["pid"] == null)
                {
                    return RedirectToAction("MyComments", "Comment");
                }
                else
                {

                    int postId = int.Parse(Session["pid"].ToString());
                    return RedirectToAction("Details/" + postId, "Post");

                }
            }
            catch
            {
                return View();
            }
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
