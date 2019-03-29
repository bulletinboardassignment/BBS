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
    public class UserController : Controller
    {
        // GET: User
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IDEncryptionService dEncryptionService;
        private readonly ICommentService commentService;

        public UserController()
        {
            userService=new UserService();
            roleService = new RoleService();
            dEncryptionService = new DEncryptionService();
            commentService = new CommentService();

        }
        [Authorize(Roles = "Admin")]

        public ActionResult Index()
        {
            var model = userService.UserList.OrderBy(p => p.UserId)
            .OrderByDescending(p => p.Create_time); ;
            return View(model);
        }
        
        [Authorize(Roles = "User,Admin")]
        public ActionResult Details(int? Id)
        {

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = userService.Details(Id);


            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = userService.Details(Id);
            model.IENUMRoleDetails = roleService.RoleIEnum;
            model.Password = dEncryptionService.Decrypt(model.Password);


            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Users data)
        {
            if (ModelState.IsValid)
            {
                Users obj = GetUserSession();
                obj.UserId = data.UserId;
                obj.FName = data.FName;
                obj.LName = data.LName;
                obj.Email = data.Email;

                obj.Password = dEncryptionService.Encrypt(data.Password);
                obj.Create_time = data.Create_time;
                obj.Update_Time = DateTime.Now;//Need solution for this field no need any value
                obj.Last_Login = data.Last_Login;
                obj.RoleId = data.RoleId;
                userService.Save(obj);
                int? Newid = obj.UserId;
                if (obj != null)
                {
                    TempData["message"] = string.Format("{0} was Edited Successfully", obj.FName + " " + obj.LName);

                }
                return RedirectToAction("Details", new { Id = Newid });
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Users user = userService.Details(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int? Id)
        {

            Users user = userService.Delete(Id);
            if (user != null)
            {
                TempData["message"] = string.Format("{0} was deleted", user.FName + " " + user.LName);
            }
            return RedirectToAction("Index", "User");
        }
        [Authorize(Roles = "User,SuperUser,Admin")]
        //UserControl For User Details
        public ActionResult UserFormDetails(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashbordVM UserAndCommment = new DashbordVM();
            UserAndCommment.User = userService.Details(Id);
            UserAndCommment.NumberOfCommentNeedApprove = commentService.CommentList.Where(p => p.Publish == false).Count();

            if (UserAndCommment == null)
            {
                return HttpNotFound();
            }
            return PartialView("_UserFormDetails", UserAndCommment);
        }

        private Users GetUserSession()
        {
            if (Session["user"] == null)
            {
                Session["user"] = new Users();
            }
            return (Users)Session["user"];
        }
    }
}