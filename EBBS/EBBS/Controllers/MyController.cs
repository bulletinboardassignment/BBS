﻿using EBBS.Data;
using EBBS.Models;
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
    public class MyController : Controller
    {
        private IUserService userService;
        private ISecurityQuestionService securityQuestionService;

        public MyController() {
            userService = new UserService();
            securityQuestionService = new SecurityQuestionService();
        }

        // GET: My/MyProfile
        public ActionResult MyProfile() {
            
            return View(userService.GetUser(this.GetUserSession().userId));
        }
        // GET: My/MyEdit
        public ActionResult MyEdit(int id) {

            List<SecurityQuestion> securityQuestions = securityQuestionService.GetMySQs();
            ViewBag.securityQuestions = securityQuestions;
            ViewBag.user = userService.GetUser(id);

            return View(new Models.UserUpdateModel());
        }
        // POST: My/MyEdit
        [HttpPost]
        public JsonResult MyEdit(Models.UserUpdateModel userUpdateModel) {
            string result = "";
            User user = new User();
                user.firstName = userUpdateModel.firstname;
                user.lastName = userUpdateModel.lastname;
                user.questionId = userUpdateModel.qId;
                user.answer = userUpdateModel.answer;
                var image = userUpdateModel.userImage;
                if (image != null)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(image.FileName);

                    image.SaveAs(Server.MapPath("/images/") + image.FileName);
                    user.userImage = fileName.ToString();
                    var contentType = image.ContentType.ToString();
                }

            userService.EditUser(this.GetUserSession().userId, user);
            result = "You have successfully modified the profile!";          
            return Json(result);
        }


        // GET: My/Index

        public ActionResult Index()
        {
            return View(userService.GetUser(this.GetUserSession().userId));
        }

        // POST: My/ChangeMyPassword

        [HttpPost]
        public JsonResult ChangeMyPassword(string password, string newPassword) {
            string result = "";
            string currentPassword = userService.GetUserPassword(this.GetUserSession().userId);
            password = userService.Encrypt(password);
            newPassword = userService.Encrypt(newPassword);
            if (password.Equals(userService.GetUserPassword(this.GetUserSession().userId))) {
                userService.ChangeUserPassword(this.GetUserSession().userId, newPassword);
                result = "You have successfully changed the password!";
            }
            else {
                result = "Sorry, Password could not be changed! Try again later!";
            }
             return Json(result);
        }




        // GET: My/ForgotPassword
        public ActionResult ForgotPassword()
        {

            List<SecurityQuestion> securityQuestions = securityQuestionService.GetMySQs();
            ViewBag.securityQuestions = securityQuestions;

            return View();
        }


        // POST: My/ForgotPassword
        [HttpPost]
        public JsonResult ForgotPassword(string username, int sqId, string answer)
        {
        
                string[] results = new string[3];

                int genuine = userService.AreResetCredentialsTrue(username, sqId, answer);
                if (genuine > 0)
                {
                    results[0] = "success";
                    results[1] = "Credentials are true. You are being navigated to password reset page !";
                    results[2] = "" + genuine;
                    Session["ruId"] = genuine;
                }
                else
                {
                    results[0] = "failure";
                    results[1] = "Sorry, Your Credentials are wrong !";
                }

                return Json(results);
           
        }

        // GET: My/ResetPassword
        public ActionResult ResetPassword(string userId) {
            
            return View();
        }

        // POST: My/ResetPassword
        [HttpPost]
        public JsonResult SetNewPassword(string password) {
            string result = "";
            int id = int.Parse(Session["ruId"].ToString());
            try
            {
               userService.ChangeUserPassword(id, userService.Encrypt(password));
               result = "Your password has reset successfully !";
            }
            catch (Exception e) {
                result = "Sorry, unable to reset your password !"+e.ToString();
            }

            return Json(result);
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