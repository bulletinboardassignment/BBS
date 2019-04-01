using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EBBS.Models;

namespace EBBS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
 
     

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