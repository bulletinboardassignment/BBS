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
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        //// GET: Category
        public ActionResult Index(int page = 1, int pageSize = 5)
        {

            var categoryList = _categoryService.GetAllCategories().OrderBy(p => p.cId)
                .OrderByDescending(p => p.cId);
            var categoryViewList = AutoMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categoryList);
            var model = new CategoryVm();
            model.Category = categoryViewList.ToPagedList(page, pageSize);
            return View(model);
        }


        //// GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Category/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cId,categoryName,description,creatorId,createTime,frequency")] Category data)
        {
            //var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            //if (identity == null)
            //{
            //    throw new ArgumentNullException("");
            //}
            //else
            //{
                //int currentUserId = Convert.ToInt32(identity.User.userId);
            //}
           

            Category obj = GetCategorySession();
       
            if (ModelState.IsValid)
            {
                obj.cId = data.cId;
                bool uniquesCategory = _categoryService.UniqueCategory(data.categoryName.TrimEnd());
                if (uniquesCategory == true)
                {
                    TempData["addUniqueMessage"] = "Record is Exist, Please Enter a new category";
                    return RedirectToAction("Create", "Category");
                }
                obj.categoryName = data.categoryName;
               
                obj.description = data.description;
                obj.frequency = 0;
                _categoryService.InsertCategory(obj);
                TempData["message"] = "Success ! You have created a new record";
                return RedirectToAction("Index", "Category");
            }
           
            return View(data);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        //// POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Category editData)

        {
            if (ModelState.IsValid)
            {
               
                _categoryService.UpdateCategory(editData);
                TempData["editMessage"] = "Success ! You have modified the record";
                return RedirectToAction("Index", "Category");

            }

            return View(editData);
        }




        //// GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }

            Category category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //// POST: Category/Delete/5
        /// 
        [HttpPost, ActionName("Delete")]
        //[Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int id)
        {
            Category findCategory = _categoryService.GetCategoryById(id);
            _categoryService.DeleteCategory(findCategory);
            TempData["deleteMessage"] = "Success ! You have deleted a record";
            return RedirectToAction("Index", "Category");
        }

        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        ////[AllowAnonymous]
        ////[ChildActionOnly]

        //public ActionResult LastCategory()
        //{
        //    Setting _NumOfCategory;
        //    _NumOfCategory = repositorySetting.GetSetting;
        //    IEnumerable<Category> Model;
        //    Model = repositoryICategory.CategoryIList.Take(_NumOfCategory.NumberOfCategory).Distinct(); ;

        //    return PartialView("_LastCategory", Model);
        //}

        private Category GetCategorySession()
        {
            if (Session["category"] == null)
            {
                Session["category"] = new Category();
            }
            return (Category)Session["category"];
        }
    }
}
