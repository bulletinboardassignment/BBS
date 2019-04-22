using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using EBBS.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EBBS.Models;
using EBBS.Service.IService;
using EBBS.Service.Service;
using Microsoft.Ajax.Utilities;

namespace EBBS.Controllers
{
    [Authorize]

    
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISecurityQuestionService _securityQuestionService;
        private readonly IRoleService _roleService;
        private ILogService logService;

        public AccountController()
        {
            _userService = new UserService();
            _securityQuestionService = new SecurityQuestionService();
            _roleService = new RoleService();
            logService = new LogService();

        }


        //GET: /Account/Login
       [AllowAnonymous]
        public ActionResult Login()
        {
            HttpCookie authoCookies = Request.Cookies["User"];
            if (authoCookies != null)
            {
                ViewBag.username = authoCookies["Username"].ToString();
                ViewBag.EncryptedPW = authoCookies["Password"].ToString();
            }
            return View();

        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl = "")

        {
            HttpCookie cookie = new HttpCookie("User");

            if (ModelState.IsValid)
            {

                string encryptedPw = _userService.Encrypt(model.password.TrimEnd());

                User user = null;

                var isValidUser = _userService.ValidateUser(model.username.TrimEnd(), encryptedPw);

                if (isValidUser)
                {

                    user = _userService.UserList.FirstOrDefault(a => a.username.Equals(model.username.TrimEnd()));

                    if (user != null)
                    {
                        user.lastLogin = DateTime.Now;
                    }
                    _userService.Save(user);

                }

                if (user != null)

                {

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        user.username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        model.RememberMe,
                        "");
                    string encToken = FormsAuthentication.Encrypt(ticket);
                    HttpCookie authoCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                    cookie.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Add(authoCookies);

                    Logs logIn = new Logs();
                    logIn.userId = user.userId;
                    logIn.loggedTime = DateTime.Now;
                    logService.Add(logIn);

                    Session["lUser"] = user;
                    if (user.userType == 1) //Admin
                    {
                        TempData["Message"] = "<script>alert('Login Successfull !!')</script>";
                        return RedirectToAction("Index", "Category"); //Admin dash
                    }
                    else if (user.userType == 2) //User
                    {
                        TempData["Message"] = "<script>alert('Login Successfull !!')</script>";
                        return RedirectToAction("Index", "Category"); //SuperUser Dash
                    }
                    else {

                        TempData["Message"] = "<script>alert('Login Successfull !!')</script>";
                        return RedirectToAction("Index", "Category"); //User HomePage
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    ModelState.Remove("Password");

                    return View();

                }
            }
            return View(model);

        }


        //// GET: Account
        //public ActionResult Logout()
        //{
        //    if (Session["User"] != null)
        //    {
        //        Session.Abandon();
        //        return RedirectToAction("Login", "Account");
        //    }
        //    return View();

        //}




        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {

            var dQuery = (from d in _securityQuestionService.SqIeNum select d).ToList();
            ViewBag.QuestionList = new SelectList(_securityQuestionService.SqIeNum, "qId", "question");
            return View();
           
        }


        //POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel data)
        {
            //if fail, the dropdown list will not be set to null.
            var dQuery = (from d in _securityQuestionService.SqIeNum select d).ToList();
            ViewBag.QuestionList = new SelectList(_securityQuestionService.SqIeNum, "qId", "question");

            var result = false;
            User obj = new User();
            if (ModelState.IsValid)
            {
                obj.firstName = data.firstname;
                obj.lastName = data.lastname;
                obj.questionId = data.questionId;
                obj.answer = data.answer;

                bool uniqueUsername = _userService.UniqueEmail(data.username.TrimEnd());
                if (uniqueUsername == true)
                {
                    ModelState.AddModelError(string.Empty, "Username is Exist, Please Enter a new username .");
                    return View(data);
                }
                obj.username = data.username.TrimEnd();
                string encryptedPw = _userService.Encrypt(data.Password.TrimEnd());
                obj.password = encryptedPw;
                obj.createTime = DateTime.Now;
                obj.updateTime = DateTime.Now;
                obj.lastLogin = DateTime.Now;
                obj.userType = 3; //TO be normal user Role
                result = _userService.Save(obj);


                if (result == true)
                {

                    User user = _userService.UserList.FirstOrDefault(a => a.username.Equals(data.username.TrimEnd()));
                   
                    if (user != null)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string datajs = js.Serialize(user);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.username, DateTime.Now, DateTime.Now.AddMinutes(30), true, datajs);
                        string encToken = FormsAuthentication.Encrypt(ticket);
                        HttpCookie authoCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                        Response.Cookies.Add(authoCookies);
                        return Redirect(Url.Action("Login", "Account"));
                    }

                }

            }

           return View(data);
        }



        private User GetUserSession()
        {
            if (Session["lUser"] == null)
            {
                Session["lUser"] = new User();
            }
            return (User)Session["user"];
        }













        //
        // GET: /Account/ForgotPassword


        //
        // POST: /Account/ForgotPassword


        //
        // GET: /Account/ForgotPasswordConfirmation

        //
        // GET: /Account/ResetPassword


        //
        // POST: /Account/ResetPassword

        //
        // GET: /Account/ResetPasswordConfirmation


    }
}