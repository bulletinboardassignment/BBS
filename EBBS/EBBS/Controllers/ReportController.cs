﻿using EBBS.Service.IService;
using EBBS.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace EBBS.Controllers
{
    public class ReportController : Controller
    {
        private IReportService reportService;
        public ReportController() {
            reportService = new ReportService();
        }
        //In case admin lets a post stay and not be deleted.
        //Asynchronous method to handle this task
        [HttpPost]
        public JsonResult Stay(int id) {
            //set the isReported attribute back to false
            reportService.AllowReportedPost(id);

            string result = "You allowed the post to stay!";
            return Json(result);
        }
        //This method is used in case an admin decides to delete a reported post

        [HttpPost]
        public JsonResult DeletePost(int id) {

            reportService.DeleteReportedPost(id);

            string result = "You deleted the reported post!";
            return Json(result);
        }


        //Grab the reported posts and paginate them
        // GET: Report
        public ActionResult Index(int? page)
        {
            var result = reportService.GetAllReportedPosts().OrderByDescending(p => p.rId);
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        //Details of a reported post
        // GET: Report/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
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

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
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

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
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
