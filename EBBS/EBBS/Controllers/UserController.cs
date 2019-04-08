using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using EBBS.Data;
using EBBS.Service.IService;
using EBBS.Service.Service;
using EBBS.Models;
using PagedList;
using AutoMapper;

namespace EBBS.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ISecurityQuestionService _securityQuestionService;

        public UserController()
        {
            _userService = new UserService();
            _roleService=new RoleService();
            _securityQuestionService=new SecurityQuestionService();

        }
        // GET: User
        public ActionResult Index(int page =1, int pageSize=5)
        {
            var userList = _userService.UserList.OrderBy(p => p.userId).OrderByDescending(p => p.createTime).ToPagedList(page,pageSize);
            return View(userList);
            //var userList = _userService.UserList.OrderBy(p => p.userId).OrderByDescending(p => p.createTime)
            //var userViewList = AutoMapper.Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(userList);
            //var userViewList = AutoMapper.Mapper.Map<IEnumerable<User>>(userList);
            //var model = new UserVm();
            //model.Users = userViewList.ToPagedList(page, pageSize);
            //return View(model);

        }
        
        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _userService.Details(id);


            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _userService.Details(id);
            model.IENUMRoleDetails = _roleService.RoleIeNum;
            model.IENUMQuestionDetails = _securityQuestionService.SqIeNum;
            //model.password = _userService.Decrypt(model.password);


            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: User/Edit/5
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(User data)
        {
            if (ModelState.IsValid)
            {
                User obj = GetUserSession();
                obj.userId = data.userId;
                obj.firstName = data.firstName;
                obj.lastName = data.lastName;
                obj.username = data.username;
                //obj.password = _userService.Encrypt(data.password);
                obj.updateTime = DateTime.Now;
                obj.userType = data.userType;
                obj.questionId = data.questionId;
                obj.answer = data.answer;
                _userService.Save(obj);
                int? Newid = obj.userId;
                if (obj != null)
                {
                    TempData["message"] = string.Format("{0} was Edited Successfully", obj.firstName + " " + obj.lastName);

                }
                return RedirectToAction("Details", new { id = Newid });
            }
            return View();
        }


        //// GET: User/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
        //    }
        //    User user = _userService.UserById(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        // POST: User/Delete/5
        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {

            User user = _userService.Delete(id);
            if (user != null)
            {
                TempData["message"] = string.Format("{0} was deleted", user.firstName + " " + user.lastName);
            }
            return RedirectToAction("Index", "User");
        }

        private User GetUserSession()
        {
            if (Session["user"] == null)
            {
                Session["user"] = new User();
            }
            return (User)Session["user"];
        }

    }
}
