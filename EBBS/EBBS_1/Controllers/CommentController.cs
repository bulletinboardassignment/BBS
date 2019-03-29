using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EBBS_1.Data;
using EBBS_1.Models;
using EBBS_1.Service.IService;
using EBBS_1.Service.Service;

namespace EBBS_1.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IPostService postService;

        public CommentController()
        {
            commentService=new CommentService();
            postService=new PostService();
        }

        // GET: Comment
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {
            IEnumerable<Comments> model = commentService.CommentList.OrderBy(p => p.CommentId)
                .OrderByDescending(p => p.Create_time);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SuperUserIndex(int? page)
        {

            IEnumerable<Comments> model = commentService.CommentList.OrderBy(p => p.CommentId)
            .OrderByDescending(p => p.Create_time);//5 is pagesize

            return View(model);

        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult AddNewComment()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public ActionResult AddNewComment(int? Id, Comments data)
        {

            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);

            Comments obj = GetCommentSession();

            obj.CommentId = data.CommentId;
            if (String.IsNullOrEmpty(data.Comment_Content))
            {
                TempData["message"] = string.Format(" Write Commnet Before Publish it .");
                return View();
            }
            obj.Comment_Content = data.Comment_Content;
            int PostId = Convert.ToInt32(Id);

            obj.Update_time = DateTime.Now;//Need solution for this field no need any value.

            obj.PostId = PostId;//PostId

            if (obj.PostId == 0)
            {

                obj.PostId = data.PostId;

            }
            if (obj.CommentId == 0)
            {
                obj.Publish = false; //New Commnet need aprove(Dash) to publish it
                obj.Create_time = DateTime.Now;
                obj.UserId = Convert.ToInt32(_CurrentUserId);//SaME uSER Wrote it
                //to increase frq for post , how many comment for post
                //postService.IncreaseFreqOne(obj.PostId);
            }

            else
            {
                obj.Create_time = data.Create_time;
                obj.UserId = data.UserId;
                obj.Publish = false; //Edite  Commnet need aprove(Dash) to publish it ,again
            }

            commentService.Save(obj);
            int? Newid = obj.CommentId;
            if (obj != null)
            {
                if (data.CommentId == 0)
                {
                    TempData["message"] = string.Format(" Added Successfully , it's on Waiting list to aprove it.");
                }
                else
                {
                    TempData["message"] = string.Format(" Edited Successfully, it's on Waiting list to aprove it.");
                }
            }
            return RedirectToAction("Details", "Post", new { Id = obj.PostId });

        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments model = commentService.Details(Id);

            if (model == null)
            {
                return HttpNotFound();
            }
            //Send you to NewComment page.chtml to save copy same page 
            return View("AddNewComment", model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Comments _Comment = commentService.Details(Id);

            if (_Comment == null)
            {
                return HttpNotFound();
            }
            return View(_Comment);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int? Id)
        {

            Comments _Comment = commentService.Delete(Id);
            //postService.DecreaseFreqOne(_Comment.PostId);
            if (_Comment != null)
            {
                TempData["message"] = string.Format("deleted");
            }
            return RedirectToAction("Details", "Post", new { Id = _Comment.PostId });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CommentNeedAprove(int? page)
        {
            IEnumerable<Comments> model = commentService.CommentList
                .Where(p => p.Publish == false)//just what need aprove
                .OrderBy(p => p.CommentId)
           .OrderByDescending(p => p.Create_time);//5 is pagesize

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult SuperUserCommentNeedAprove(int? page)
        {
            IEnumerable<Comments> model = commentService.CommentList
                .Where(p => p.Publish == false)//just what need aprove
                .OrderBy(p => p.CommentId)
                .OrderByDescending(p => p.Create_time);//5 is pagesize

            return View(model);

        }




        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PublishComment(int? Id)
        {
            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserRole = Convert.ToInt32(identity.User.RoleId);

            Comments _Comment = commentService.Details(Id);
            _Comment.Publish = true;//Aprove 
            commentService.Save(_Comment);

            TempData["message"] = string.Format(" Published Successfully");

            if (_CurrentUserRole == 1)
            {
                return RedirectToAction("CommentNeedAprove", "Comment");
            }
            else
            {//SuperUser Role Page
                return RedirectToAction("SuperUserCommentNeedAprove", "Comment");
            }
        }




        ///Sessions
        ///
        private Comments GetCommentSession()
        {
            if (Session["commnet"] == null)
            {
                Session["commnet"] = new Comments();
            }
            return (Comments)Session["commnet"];
        }
    }
}