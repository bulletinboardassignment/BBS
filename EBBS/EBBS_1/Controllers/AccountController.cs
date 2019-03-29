
using EBBS_1.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using EBBS_1.Data;
using EBBS_1.Service.IService;
using EBBS_1.Service.Service;
using System.Security.Policy;
using Microsoft.Owin.Security.Provider;

namespace EBBS_1.Controllers
{

    public class AccountController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly ICommentService commentService;
        private readonly ICategoryService categoryService;
        private readonly IDEncryptionService dEncryptionService;
        private readonly ISettingService settingService;
        private readonly ISecurityQuestionService securityQuestionService;

       
        public AccountController()
        {
            authenticationService = new AuthenticationService();
            userService = new UserService();
            postService = new PostService();
            commentService = new CommentService();
            categoryService = new CategoryService();
            dEncryptionService = new DEncryptionService();
            settingService = new SettingService();
            securityQuestionService = new SecurityQuestionService();

        }

        //[Authorize(Roles = "Admin")]
        //public ActionResult Index()
        //{

        //    DateTime Last24Hours = DateTime.Now.Date.AddHours(-24);

        //    DashbordVM model = new DashbordVM();
        //    model.NumberOfNewUser = userService.UserList.Where(p => p.Create_time >= Last24Hours).Count();// >= 24H to get all users added last 24h
        //    model.NumberOfNewPost = postService.PostList.Where(p => p.Create_time >= Last24Hours).Count();
        //    model.NumberOfNewCategory = categoryService.CategoryIList.Where(p => p.Create_time >= Last24Hours).Count();
        //    model.NumberOfNewComment = commentService.CommentList.Where(p => p.Create_time >= Last24Hours).Count();
        //    model.NumberOfCommentNeedApprove = commentService.CommentList.Where(p => p.Publish == false).Count();
        //    return View(model);

        //}


        // GET: Account
        public ActionResult Logout()
        {
            if (Session["User"] != null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }

            return View();

        }



        [AllowAnonymous]
        public ActionResult Login()
        {
            HttpCookie authoCookies = Request.Cookies["User"];
            if (authoCookies != null)
            {
                ViewBag.Email = authoCookies["Email"].ToString();
                ViewBag.EncryptedPW = authoCookies["Password"].ToString();
            }
            return View();

        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl = "")

        {
           HttpCookie cookie = new HttpCookie("Users");

            if (ModelState.IsValid)
            {
                
                string EncryptedPW = dEncryptionService.Encrypt(model.Password.TrimEnd());

                Users _User = null;

                var IsValidUser = userService.ValidateUser(model.Email.TrimEnd(), EncryptedPW);

                if (IsValidUser)
                {

                    _User = userService.UserIEmum.Where(a => a.Email.Equals(model.Email.TrimEnd())).FirstOrDefault();

                    _User.Last_Login = DateTime.Now;

                    userService.Save(_User);

                }

                if (_User != null)

                {

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        _User.Email,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        model.RememberMe,
                        "");
                    string encToken = FormsAuthentication.Encrypt(ticket);
                    HttpCookie authoCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                    cookie.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Add(authoCookies);
                    
                    

                    if (_User.RoleId == 1) //Admin
                    {
                        Session["User"] = _User.Email;
                        TempData["Message"] = "<script>alert('Login Successfull !!')</script>";
                        return RedirectToAction("Index", "Home"); //Admin dash
                    }
                    else if (_User.RoleId == 2) //User
                    {
                        Session["User"] = _User.Email;
                        TempData["Message"] = "<script>alert('Login Successfull !!')</script>";
                        return RedirectToAction("Register", "Account"); //SuperUser Dash
                    }
                    Session["User"] = _User.Email;
                    TempData["Message"] = "<script>alert('Login Successfull !!')</script>";
                    return RedirectToAction("Login", "Account"); //User HomePage
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

            


        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        //POST: /Account/Register
       [HttpPost]
       [AllowAnonymous]
       [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel data)
        {
            var result = false;
            Users obj = GetUserSession();
            if (ModelState.IsValid)
            {

                obj.FName = data.FName;
                obj.LName = data.LName;
                

                bool EmailUniqe = userService.UniqueEmail(data.Email.TrimEnd());
                if (EmailUniqe == true)
                {
                    ModelState.AddModelError(string.Empty, "Email is Exist, Please Enter New One .");
                    return View(data);
                }
                obj.Email = data.Email.TrimEnd();
                //   string encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(data.Password, "SHA1");
                string EncryptedPW = dEncryptionService.Encrypt(data.Password.TrimEnd());
                obj.Password = EncryptedPW;

                obj.Create_time = DateTime.Now;
                obj.Update_Time = DateTime.Now;
                obj.Last_Login = DateTime.Now;
                obj.RoleId = 2;//TO be normal user Role
                

                result = userService.Save(obj);

                ViewBag.UserID = obj.UserId;
                if (result == true)
                {

                    Users _User = userService.UserIEmum.Where(a => a.Email.Equals(data.Email.TrimEnd())).FirstOrDefault();




                    if (_User != null)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string datajs = js.Serialize(_User);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, _User.Email, DateTime.Now, DateTime.Now.AddMinutes(30), true, datajs);
                        string encToken = FormsAuthentication.Encrypt(ticket);
                        HttpCookie authoCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                        Response.Cookies.Add(authoCookies);
                        return Redirect(Url.Action("Index", "Home"));//User HomePage
                    }

                }

            }

            // If we got this far, something failed, redisplay form
            return View(data);
        }


        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //        //
        //        // POST: /Account/ForgotPassword
        //        [HttpPost]
        //        [AllowAnonymous]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult ForgotPassword(ForgotViewModel model)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                Users user = repositoryUser.UserIEmum.Where(p => p.Email.Equals(model.Email)).FirstOrDefault();
        //                if (user != null)
        //                {
        //                    //Send Email with password
        //                    EMailPasswordSender(user.Email, user.Password);
        //                    return View("ForgotPasswordConfirmation");
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("", "Your Email is not exists");
        //                    return View();
        //                }


        //            }


        //            // If we got this far, something failed, redisplay form
        //            return View(model);
        //        }

        //        //
        //        // GET: /Account/ForgotPasswordConfirmation
        //        [AllowAnonymous]
        //        public ActionResult ForgotPasswordConfirmation()
        //        {
        //            return View();
        //        }

        //        // GET: /Account/ResetPassword
        //        [Authorize(Roles = "User,Admin,SuperUser")]
        //        public ActionResult ResetPassword(int? Id)
        //        {
        //            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
        //            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);
        //            if (Id == null)
        //            {
        //                return View("Error");
        //            }
        //            if (Id == _CurrentUserId)
        //            {
        //                Users _User = repositoryUser.UserIEmum.Where(p => p.UserId.Equals(Id)).FirstOrDefault();
        //                ResetPasswordViewModel _Email = new ResetPasswordViewModel();
        //                _Email.Email = _User.Email;
        //                return View(_Email);
        //            }
        //            else
        //            {
        //                return View("Error");
        //            }

        //            //  return View();
        //        }

        //        [HttpPost]
        //        [Authorize(Roles = "User,Admin,SuperUser")]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult ResetPassword(ResetPasswordViewModel model)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return View(model);
        //            }
        //            string EncryptedOldPW = repositoryDEncryption.Encrypt(model.OldPassword);
        //            Users _User = repositoryUser.UserIEmum.Where(p => p.Email.Equals(model.Email) && p.Password.Equals(EncryptedOldPW)).FirstOrDefault();
        //            if (_User != null)
        //            {
        //                string EncryptedNewPW = repositoryDEncryption.Encrypt(model.Password);
        //                _User.Password = EncryptedNewPW;
        //                repositoryUser.Save(_User);
        //                //Singout
        //                FormsAuthentication.SignOut();
        //                return RedirectToAction("ResetPasswordConfirmation", "Account");
        //            }
        //            return View();
        //        }

        //        [AllowAnonymous]
        //        public ActionResult ResetPasswordConfirmation()
        //        {
        //            return View();
        //        }


        private Users GetUserSession()
        {
            if (Session["user"] == null)
            {
                Session["user"] = new Users();
            }
            return (Users)Session["user"];
        }



        //        //Email Sender 
        //        public void EMailPasswordSender(string receiver, string Password)
        //        {
        //            EmailSetting _emailsetting = repositoryEmailSetting.GetEmailSetting;

        //            MailMessage mail = new MailMessage();
        //            SmtpClient SmtpServer = new SmtpClient(_emailsetting.SMTP_Server);

        //            mail.From = new MailAddress(_emailsetting.Sender);
        //            mail.To.Add(receiver);
        //            mail.Subject = "Your Password";
        //            string HashUserPassword = repositoryDEncryption.Decrypt(Password);
        //            mail.Body = HashUserPassword;
        //            SmtpServer.UseDefaultCredentials = false;
        //            SmtpServer.Port = _emailsetting.SMTPServer_Port;
        //            string HashEmailPassword = repositoryDEncryption.Decrypt(_emailsetting.Password);
        //            SmtpServer.Credentials = new NetworkCredential(_emailsetting.UserName, HashEmailPassword);
        //            NetworkCredential Credentials = new NetworkCredential(_emailsetting.Sender, HashEmailPassword);
        //            SmtpServer.Credentials = Credentials;

        //            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

        //            SmtpServer.EnableSsl = _emailsetting.EnableSSL;

        //            SmtpServer.Send(mail);
        //        }


        //            // To get current user data
        //            Users _User = repositoryUser.UserIEmum.Where(a => a.Email.Equals(obj.Email)).FirstOrDefault();


        //            //   string encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(data.Password, "SHA1");
        //            if (_Users != null)
        //            {
        //                JavaScriptSerializer js = new JavaScriptSerializer();
        //                string data = js.Serialize(_User);
        //                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, _User.Email, DateTime.Now, DateTime.Now.AddMinutes(30), false, data);
        //                string encToken = FormsAuthentication.Encrypt(ticket);
        //                HttpCookie authoCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
        //                Response.Cookies.Add(authoCookies);


        //            }
        //            ////



    


        //}



    }


}
