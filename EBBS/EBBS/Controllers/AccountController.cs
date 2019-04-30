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
    //[Authorize]


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
            // Get cookie from the current request.
            HttpCookie authoCookies = Request.Cookies["User"];
            if (authoCookies != null)
            {
                ViewBag.username = authoCookies["Username"].ToString();
                ViewBag.EncryptedPW = authoCookies["Password"].ToString();
            }

            // If something failed, re-display the form
            return View();

        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl = "")

        {

            HttpCookie cookie = new HttpCookie("User");

            //ViewBag.show = "false";

            //if there are no any model errors exists, allows user to login to the system
            if (ModelState.IsValid)
            {
                //Encripted password assign to a string variable
                string encryptedPw = _userService.Encrypt(model.password.TrimEnd());

                User user = null;

                //Validated user assign to a variable
                var isValidUser = _userService.ValidateUser(model.username.TrimEnd(), encryptedPw);

                //If user is exists, the user lastlogin will update in the database
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
                    //If user is exists, The ticket is passed as the value of the forms authentication cookie with 
                    // each request and is used by forms authentication on the server to identify an authenticated user. 
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        user.username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        model.RememberMe,
                        "");
                    string encToken = FormsAuthentication.Encrypt(ticket);
                    HttpCookie authoCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);

                    //Set the coockie expire time
                    cookie.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Add(authoCookies);

                    Logs logIn = new Logs();
                    logIn.userId = user.userId;
                    logIn.loggedTime = DateTime.Now;
                    logService.Add(logIn);

                    //Session Management
                    Session["lUser"] = user;

                    //if the logged in user type is admin, the user will be redirected to the all post lists
                    if (user.userType == 1) //Admin
                    {
                        TempData["Message"] = "<script>alert('You have successfully loggedin!!!')</script>";
                        return RedirectToAction("Index", "Post"); //Admin dash
                    }
                    //if the logged in user type is noremal user, the user will be redirected to the all post lists
                    else if (user.userType == 2) //User
                    {
                        TempData["Message"] = "<script>alert('You have successfully loggedin!!!')</script>";
                        return RedirectToAction("Index", "Post"); //User Dash
                    }
                    //if the logged in user type is other user types, the user will be redirected to the all post lists
                    else
                    {
                        TempData["Message"] = "<script>alert('You have successfully loggedin!!!')</script>";
                        return RedirectToAction("Index", "Post"); //Other User types HomePage
                    }
                }

                //if the username and password correct, validation message will be showed 
                else
                {
                    ModelState.AddModelError("", "Sorry, You have enterd the incorrect username or password");
                    ModelState.Remove("Password");

                    return View();

                }
            }
            return View(model);

        }



        //// GET: Account
        public ActionResult LogMeOut()
        {
            // signInUserSession set to null.
            Session["lUser"] = null;
            // removes the forms-authentication ticket information from the cookie or the URL 
            FormsAuthentication.SignOut();
            //User Redirects To the Login Page and allow a different user to log in.
            return RedirectToAction("Login", "Account");
        }




        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            //transfer temporary security questions lists from the controller to the view
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
            //transfer temporary security questions lists from the controller to the view
            ViewBag.QuestionList = new SelectList(_securityQuestionService.SqIeNum, "qId", "question");

            var result = false;
            User obj = new User();

            //if the registration successfull, the user details saved in the database
            if (ModelState.IsValid)
            {
                obj.firstName = data.firstname;
                obj.lastName = data.lastname;
                obj.questionId = data.questionId;
                obj.answer = data.answer;
                obj.userImage = "img_avatar.jpg"; //set the default image intially


                bool uniqueUsername = _userService.UniqueEmail(data.username.TrimEnd());

                // Verify the entered username is already exists in the database or not. If username exists , the validation message will display otherwise user information will store successfully.
                if (uniqueUsername == true)
                {
                    ModelState.AddModelError(string.Empty, "Sorry, Username is Exist, Please Enter a new username.");
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

                TempData["registrationConfirmation"] = "<script>alert('You have successfully Registered!. Login with new user credentials')</script>";

                if (result == true)
                {
                    User user = _userService.UserList.FirstOrDefault(a => a.username.Equals(data.username.TrimEnd()));

                    if (user != null)
                    { //Store the user cookies in form authentication ticket to detect the user session
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
            //if something goes wrong, re-dispaly the registration view
            return View(data);
        }


        //store the user session
        private User GetUserSession()
        {
            if (Session["lUser"] == null)
            {
                Session["lUser"] = new User();
            }
            return (User)Session["user"];
        }

    }
}