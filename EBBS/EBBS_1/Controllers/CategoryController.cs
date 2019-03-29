using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EBBS_1.Data;
using EBBS_1.Service.IService;
using EBBS_1.Service.Service;

namespace EBBS_1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ISettingService settingService;

        public CategoryController()
        {
            categoryService=new CategoryService();
            settingService=new SettingService();
        }
        // GET: Category
        public ActionResult Index()
        {
            IEnumerable<Categories> model = categoryService.CategoryIList.OrderBy(p => p.CategoryId)
                .OrderByDescending(p => p.Create_time); ;

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult NewCategory()
        {
            Categories Model = new Categories();
            Model.CategoryId = 0;
            return View(Model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult NewCategory(Categories data)
        {

            Categories obj = GetCategorySession();
            if (ModelState.IsValid)
            {
                obj.CategoryId = data.CategoryId;
                obj.CategoryName = data.CategoryName;
                if (obj.CategoryId == 0)
                {
                    obj.Create_time = DateTime.Now;//New Category , need new date
                }
                else
                {
                    obj.Create_time = data.Create_time;//Just read same last date no need change
                }
                obj.Frequence = 0;//If not 0 will be Null on DB , we cant  do Null +1 .

                categoryService.Save(obj);
                if (obj != null)
                {
                    if (data.CategoryId == 0)
                    {
                        TempData["message"] = string.Format("{0} was Added Successfully", obj.CategoryName);
                    }
                    else
                    {
                        TempData["message"] = string.Format("{0} was Edited Successfully", obj.CategoryName);
                    }
                }
                return RedirectToAction("Index", "Category");
            }

            return View(data);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories model = categoryService.Details(Id);

            if (model == null)
            {
                return HttpNotFound();
            }
            //Send you to NewPost.chtml to save copy same page 
            return View("NewCategory", model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Categories category = categoryService.Details(Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int? Id)
        {
            Categories _Category = categoryService.Details(Id);
            if (_Category.Frequence != 0)
            {
                string CommnetError = string.Format("You cannot delete this Post , Because its has {0} Posts", _Category.Frequence.ToString());
                //  ModelState.AddModelError("", CommnetError);
                TempData["message"] = CommnetError;
                return View(_Category);
            }
            Categories category = categoryService.Delete(Id);
            if (category != null)
            {
                TempData["message"] = string.Format("{0} was deleted", category.CategoryName);
            }
            return RedirectToAction("Index", "Category");
        }

        [AllowAnonymous]
        [ChildActionOnly]

        public ActionResult LastCategory()
        {
            Settings _NumOfCategory;
            _NumOfCategory = settingService.GetSetting;
            IEnumerable<Categories> Model;
            Model = categoryService.CategoryIList.Take(_NumOfCategory.NumberOfCategory.GetValueOrDefault()).Distinct(); ;

            return PartialView("_LastCategory", Model);
        }

        ///Sessions
        ///
        private Categories GetCategorySession()
        {
            if (Session["categoy"] == null)
            {
                Session["categoy"] = new Categories();
            }
            return (Categories)Session["categoy"];
        }

    }
}