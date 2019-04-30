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
            //ViewBag.show = "true";
            
            User user = GetUserSession();
            string userType = user.Role.roleName;
            ViewBag.userType = userType;
            //Show list of categories and list in descending order of the category id
            var categoryList = _categoryService.GetAllCategories().OrderBy(p => p.cId).OrderByDescending(p => p.cId);
            //mapping with the CategoryViewModel to show the pagination on the view page
            var categoryViewList = AutoMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categoryList);
            var model = new CategoryVm();
            //Configure to show only  5 categories per page
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
            User user = GetUserSession(); //store to logged in user session into the user object
            Category obj = GetCategorySession();//store to category session into the category object
            //if the enterd category details valid, allows to save the data in database
            if (ModelState.IsValid)
            {
                obj.cId = data.cId;
                bool uniquesCategory = _categoryService.UniqueCategory(data.categoryName.TrimEnd());
                //check whether the entered category is exisits in the database
                if (uniquesCategory == true)
                {
                    TempData["addUniqueMessage"] = "Record is Exist, Please Enter a new category";
                    return RedirectToAction("Create", "Category");
                }
                obj.categoryName = data.categoryName;
                obj.description = data.description;
                obj.creatorId = user.userId;
                obj.createTime = DateTime.Now;
                obj.frequency = 0;
                //save the entred category details
                _categoryService.InsertCategory(obj);//add the data
                //Show success message when entered details are correct
                TempData["message"] = "Success ! You have created a new record";
                //User redirects to the Category Index page after successfull category creation
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
            //Find the category id of selected category from database to edit
            Category category = _categoryService.GetCategoryById(id);
            //if category is null show the http not found error
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
            User user = GetUserSession(); //store to logged in user session into the user object
            Category obj = GetCategorySession();//store to category session into the category object
            //if the modified category details valid, allows to save the data in database
            if (ModelState.IsValid)
            {
                obj.cId = editData.cId;
                obj.categoryName = editData.categoryName;
                obj.description = editData.description;
                obj.creatorId = user.userId;
                obj.createTime = DateTime.Now;
                obj.frequency = 0;
                _categoryService.UpdateCategory(obj);//modify the data
                //Show success message when modification is succesfful
                TempData["editMessage"] = "Success ! You have modified the record";
                //User redirects to the Category Index page after successfull category modeification
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
            //Find the category id of selected category from database to delete
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
        { //Find the category id of selected category from database to delete
            Category findCategory = _categoryService.GetCategoryById(id);
            _categoryService.DeleteCategory(findCategory);
            //Show success message when deletion is succesfful
            TempData["deleteMessage"] = "Success ! You have deleted a record";
            //User redirects to the Category Index page after successfull category deletion
            return RedirectToAction("Index", "Category");
        }

        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Find the category id of selected category from database to view details
            Category category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        //configuration for store the category session
        private Category GetCategorySession()
        {
            if (Session["category"] == null)
            {
                Session["category"] = new Category();
            }
            return (Category)Session["category"];
        }

        //configuration for store the user session
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
