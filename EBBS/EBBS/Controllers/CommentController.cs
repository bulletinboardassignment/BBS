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

        //Get all comments for one specific post
        public ActionResult Get(int postId) {
            Post post = postService.GetPost(postId);
            ViewBag.post = post;
            List<Comment> comments = commentService.GetAllCommentsForPost(postId);
            return View(comments);
        }

        [HttpPost]
        //A JSonResult controller to store a new comment
        //and pass a successful json response to the view asynchronously
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



        // GET: Comment/Edit/5
        //Get the comment and pass it to the view
        //Edit the comment of selected post in My Comment page
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
                //If somebody is coming from post edit page, redirect him back to 
                //MyComments page
                if (Session["pid"] == null)
                {
                    return RedirectToAction("MyComments", "Comment");
                }
                else
                {

                    int postId = int.Parse(Session["pid"].ToString());
                    //return RedirectToAction("Details/"+postId, "Post");
                    return RedirectToAction("MyComments/" + postId, "Comment");

                }

                
            }
            catch
            {
                return View();
            }
        }



        // GET: Comment/Delete/5
        //Delete the comment of selected post in My Comment page
        public ActionResult Delete(int id)
        {
            //Grab the comment and go to delete confirmation page
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
                    //if the pid session is set, go to the details of the post with id pid

                    int postId = int.Parse(Session["pid"].ToString());
                    return RedirectToAction("MyComments/" + postId, "Comment");
                }
            }
            catch
            {
                return View();
            }
        }



        // GET: Comment/EditMyComment/5
        //Edit the comment of selected post in MyPost page
        public ActionResult EditMyComment(int id)
        {
            return View(commentService.GetComment(id));
        }

        // POST: Comment/EditMyComment/5
        [HttpPost]
        public ActionResult EditMyComment(int id, Comment comment)
        {
            try
            {
                // TODO: Add update logic here

                commentService.EditComment(id, comment);

                if (Session["pid"] == null)

                {
                    return RedirectToAction("MyDetails", "Post");
                }
                else
                {

                    int postId = int.Parse(Session["pid"].ToString());
                    return RedirectToAction("MyDetails/" + postId, "Post");

                }
                
            }
            catch
            {
                return View();
            }
        }


        // GET: Comment/DeleteMyComment/5
        //Delete the comment of selected post in MyPost page
        public ActionResult DeleteMyComment(int id)
        {

            return View(commentService.GetComment(id));
        }

        // POST: Comment/DeleteMyComment/5
        [HttpPost]
        public ActionResult DeleteMyComment(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                commentService.DeleteComment(id);
                if (Session["pid"] == null)

                {
                    return RedirectToAction("MyDetails", "Post");
                }
                else
                {

                    int postId = int.Parse(Session["pid"].ToString());
                    //return RedirectToAction("Details/" + postId, "Post");
                    return RedirectToAction("MyDetails/" + postId, "Post");

                }
            }
            catch
            {
                return View();
            }
        }


        // GET: Comment/EditMyComment/5
        //Edit the comment of selected post in All Post page
        public ActionResult EditPostComment(int id)
        {
            return View(commentService.GetComment(id));
        }

        // POST: Comment/EditMyComment/5

        [HttpPost]
        public ActionResult EditPostComment(int id, Comment comment)
        {
            try
            {
                // TODO: Add update logic here

                commentService.EditComment(id, comment);

                if (Session["pid"] == null)

                {
                    return RedirectToAction("Details", "Post");
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


        // GET: Comment/DeleteMyComment/5
        //Delete the comment of selected post in All Post page
        public ActionResult DeletePostComment(int id)
        {

            return View(commentService.GetComment(id));
        }

        // POST: Comment/DeleteMyComment/5
        [HttpPost]
        public ActionResult DeletePostComment(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                commentService.DeleteComment(id);
                if (Session["pid"] == null)

                {
                    return RedirectToAction("Details", "Post");
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


        //get the user session
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
