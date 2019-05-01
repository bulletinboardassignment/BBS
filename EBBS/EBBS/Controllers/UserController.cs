using EBBS.Data;
using EBBS.Service.IService;
using EBBS.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using AutoMapper;
using System.Net;

namespace EBBS.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;
        private readonly IRoleService roleService;
        private readonly ISecurityQuestionService securityQuestionService;
        private EbbSEntities context;

        public UserController() {
            userService = new UserService();
            roleService = new RoleService();
            securityQuestionService = new SecurityQuestionService();
            context = new EbbSEntities();
        }

        [HttpPost]
        //Promote the user from one role to a higher
        //The same method is used to demote the user as well
        public JsonResult Promote(int userId, int userType) {
            string result = "";

            try
            {
                userService.PromoteUser(userId, userType);
                result = "You successfully promoted this user!";
            }
            catch (Exception e) {
                result = e.ToString();
            }



            return Json(result);
        }


        // GET: User
        public ActionResult Index(int? page)
        { 
            //"Users"sorted descending by the "createdTime" and 5 User records display per page
            var userList = userService.GetAllUsersExceptMe(
                this.GetUserSession().userId).OrderBy(p => p.userId).OrderByDescending(p => p.createTime).ToPagedList(page ?? 1, 5);
           
            List<Role> roles = roleService.GetAllRoles().ToList(); // get all the roles to show in the dropdown
            ViewBag.roles = roles;
            return View(userList);

        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            //return View(userService.GetUser(id));
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = userService.Details(id);


            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = userService.Details(id);
            var x = (from d in roleService.RoleIeNum select d).ToList();
            //Roles for dropdown list in the view
            ViewBag.RoleList = new SelectList(roleService.RoleIeNum, "rId", "rolename");


            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User data)
        {
            var x = (from d in roleService.RoleIeNum select d).ToList();
            //Roles for dropdown list in the view
            ViewBag.RoleList = new SelectList(roleService.RoleIeNum, "rId", "rolename");

            if (ModelState.IsValid)
            {
                User obj = GetUserSession();
                obj.userId = data.userId;
                obj.updateTime = DateTime.Now;
                obj.userType = data.userType;
                userService.Save(obj);
                int? Newid = obj.userId;
                if (obj != null)
                {
                    TempData["message"] = string.Format("Role has been changed Successfully");

                }
                return RedirectToAction("Details", new { id = Newid });
            }
            return View();
        }


        // GET: /Movies/Delete/5
        public ActionResult Delete(int id)
        {
            User x = userService.UserById(id);
                      return View(x);
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User x = userService.UserById(id);
            userService.DeleteUser(x);
            if (x != null)
            {
                TempData["message"] = string.Format("{0} has been deleted successfully", x.firstName + " " + x.lastName);
            }
            return RedirectToAction("Index");
        }

        //Grab the current user session
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
