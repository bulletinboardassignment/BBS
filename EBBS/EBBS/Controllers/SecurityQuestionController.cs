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
    public class SecurityQuestionController : Controller
    {
        private readonly ISecurityQuestionService _securityQuestionService;

        public SecurityQuestionController()
        {
            _securityQuestionService = new SecurityQuestionService();
        }
        // GET: SecurityQuestion
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var sqList = _securityQuestionService.GetMySQs().OrderBy(p=>p.qId).OrderByDescending(p=>p.qId);
            var sqViewList = AutoMapper.Mapper.Map<IEnumerable<SecurityQuestion>, IEnumerable<SecurityQuestionViewModel>>(sqList);
            var model = new SecurityQuestionVm();
            model.Question = sqViewList.ToPagedList(page, pageSize);
            return View(model);
        }

        
        // GET: SecurityQuestion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecurityQuestion/Create
        [HttpPost]
        public ActionResult Create(SecurityQuestionViewModel data)

        {
            SecurityQuestion obj = new SecurityQuestion();

            if (ModelState.IsValid)
            {
                bool uniqueQuestion = _securityQuestionService.UniqueRole(data.question.TrimEnd());

                if (uniqueQuestion == true)
                {

                    TempData["addUniqueMessage"] = "Record is Exist, Please Enter New One";
                    return RedirectToAction("Create", "SecurityQuestion");
                }
                obj.question = data.question.TrimEnd();
                _securityQuestionService.InsertSecurityQuestion(obj);
                TempData["message"] = "Success ! You have created a new record";
                return RedirectToAction("Index", "SecurityQuestion");

            }

            return View();
        }


        // GET: SecurityQuestion/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityQuestion dataEdit = _securityQuestionService.GetSecurityQuestionById(id);

            if (dataEdit == null)
            {
                return HttpNotFound();
            }
            return View(dataEdit);
        }

        // POST: SecurityQuestion/Edit/5
        [HttpPost]
        public ActionResult Edit(SecurityQuestion editData)

        {
            if (ModelState.IsValid)
            {
                bool uniquesQuestion = _securityQuestionService.UniqueRole(editData.question.TrimEnd());
                if (uniquesQuestion == true)
                {

                    TempData["uniqueMessage"] = "Record is Exist, Please modify with a new security question";
                    return RedirectToAction("Edit", "SecurityQuestion");
                }

                _securityQuestionService.UpdateSecurityQuestion(editData);
                TempData["editMessage"] = "Success ! You have modified the record";
                return RedirectToAction("Index", "SecurityQuestion");

            }

            return View(editData);
        }

        // GET: SecurityQuestion/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityQuestion data = _securityQuestionService.GetSecurityQuestionById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: SecurityQuestion/Delete/5
       [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SecurityQuestion data = _securityQuestionService.GetSecurityQuestionById(id);
            _securityQuestionService.DeleteSecurityQuestion(data);
            TempData["deleteMessage"] = "Success ! You have deleted the record.";
            return RedirectToAction("Index");
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
