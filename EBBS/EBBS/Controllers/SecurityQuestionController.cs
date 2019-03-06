using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBS.Controllers
{
    public class SecurityQuestionController : Controller
    {

        private EBBS.Services.IService.ISecurityQuestionService securityQuestionsService;

        public SecurityQuestionController() {
            securityQuestionsService = new EBBS.Services.Service.SecurityQuestionService();
        }


        public ActionResult SecurityQuestions() {
            return View(securityQuestionsService.GetAllSecurityQuestions());
        }

        // GET: SecurityQuestion
        public ActionResult Index()
        {
            return View();
        }

        // GET: SecurityQuestion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SecurityQuestion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecurityQuestion/Create
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

        // GET: SecurityQuestion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SecurityQuestion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SecurityQuestion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SecurityQuestion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
