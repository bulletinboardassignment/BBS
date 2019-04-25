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
    public class RoleController : Controller
    {

        private readonly IRoleService _roleService;

        //public object Mapper { get; set; }

        public RoleController()
        {
            _roleService = new RoleService();
        }
        
        // GET: Role
        //[Authorize(Roles = "Admin")]
        public ActionResult Index(int page =1, int pageSize=5)
        {

            var roleList = _roleService.GetAllRoles().OrderBy(p=>p.rId).OrderByDescending(p=>p.rId);
            var roleViewList = AutoMapper.Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(roleList);
            var model = new RoleVm();
            model.Roles = roleViewList.ToPagedList(page,pageSize);
            return View(model);
         }

       

        // GET: Role/Create
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public ActionResult Create(RoleViewModel data)

        {
            Role obj = new Role();
            if (ModelState.IsValid)
            {
                bool role = _roleService.UniqueRole(data.roleName.TrimEnd());
                if (role == true)
                {

                    TempData["addUniqueMessage"] = "Record is Exist, Please Enter a New One";
                    return RedirectToAction("Create", "Role");
                }
                obj.roleName = data.roleName.TrimEnd();
                _roleService.InsertRole(obj);
                TempData["message"] = "Success ! You have created a new record";
                return RedirectToAction("Index", "Role");

            }

            return View();
        }

        


        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _roleService.RoleById(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: SecurityQuestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = _roleService.RoleById(id);
            _roleService.DeleteRole(role);
            TempData["deleteMessage"] = "Success ! You have deleted a record";
            return RedirectToAction("Index");
        }


        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role dataEdit = _roleService.RoleById(id);

            if (dataEdit == null)
            {
                return HttpNotFound();
            }
            return View(dataEdit);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(Role editData)

        {
            if (ModelState.IsValid)
            {
                bool uniquesRole = _roleService.UniqueRole(editData.roleName.TrimEnd());
                if (uniquesRole == true)
                {

                    TempData["uniqueMessage"] = "Record is Exist, Please modify with a new user role";
                    return RedirectToAction("Edit", "Role");
                }

                _roleService.UpdateRole(editData);
                TempData["editMessage"] = "Success ! You have modified the record";
                return RedirectToAction("Index", "Role");

            }

            return View(editData);
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
