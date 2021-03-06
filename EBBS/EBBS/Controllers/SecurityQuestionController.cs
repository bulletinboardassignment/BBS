﻿using System;
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
            User user = GetUserSession();//set the user session
            var sqList = _securityQuestionService.GetMySQs().OrderBy(p=>p.qId).OrderByDescending(p=>p.qId);//"Security Questions"sorted descending by the "question Id" 
            var sqViewList = AutoMapper.Mapper.Map<IEnumerable<SecurityQuestion>, IEnumerable<SecurityQuestionViewModel>>(sqList);
            var model = new SecurityQuestionVm();
            model.Question = sqViewList.ToPagedList(page, pageSize);// five "Security Questions" records display per page
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
                bool uniqueQuestion = _securityQuestionService.UniqueSecurityQuestion(data.question.TrimEnd());
                //verify the entered security question is exisists or not
                if (uniqueQuestion == true)
                {
                    //if security question is exists, the validation message will display on the view
                    TempData["addUniqueMessage"] = "Record is Exist, Please Enter a new security question";
                    return RedirectToAction("Create", "SecurityQuestion");
                }
                //if security question is not exists, creates new security question 
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
            //get the selected security question id
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
                bool uniquesQuestion = _securityQuestionService.UniqueSecurityQuestion(editData.question.TrimEnd());
                //if security question is exists, the validation message will display on the view

                if (uniquesQuestion == true)
                {

                    TempData["uniqueMessage"] = "Record is Exist, Please modify with a new security question";
                    return RedirectToAction("Edit", "SecurityQuestion");
                }
                //if security question is not exists, update the security question 

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
            //get the selected security question id
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
            //if security question is use for user, the validation message will display on the view

            if (_securityQuestionService.AnybodyGotThisSecurityQuestion(id))
            {
                TempData["deleteMessage"] = "Sorry ! You cannot delete this security question.";
            }
            else {
                //if security question is not use for user, delete the security question 

                _securityQuestionService.DeleteSecurityQuestion(data);
                TempData["deleteMessage"] = "Success ! You have deleted the record.";
            }            
            return RedirectToAction("Index");
        }

        //grab the current user data
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
