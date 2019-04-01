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

        private readonly IRoleService roleService;

        //public object Mapper { get; set; }

        public RoleController()
        {
            roleService = new RoleService();
        }
        
        // GET: Role
        //[Authorize(Roles = "Admin")]
        public ActionResult Index(int page =1, int pageSize=5)
        {
            //IEnumerable<Role> roleList = roleService.GetAllRoles();
            //PagedList<Role> model = new PagedList<Role>(roleList, page, pageSize);
            //return View(model);
           
            var roleList = roleService.GetAllRoles();
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
                bool RoleUniqe = roleService.UniqueRole(data.roleName.TrimEnd());
                if (RoleUniqe == true)
                {

                    TempData["UniqeMessage"] = "Record is Exist, Please Enter New One";
                    return RedirectToAction("Create", "Role");
                }
                obj.roleName = data.roleName.TrimEnd();
                roleService.InsertRole(obj);
                TempData["message"] = "Success ! You have created a new record";
                return RedirectToAction("Index", "Role");

            }

            return View(data);
        }

        
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = roleService.RoleById(id);
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
            Role role = roleService.RoleById(id);
            roleService.DeleteRole(role);
            TempData["deleteMessage"] = "Success ! You have deleted a record";
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role roleEdit = roleService.RoleById(id);

            if (roleEdit == null)
            {
                return HttpNotFound();
            }
            return View(roleEdit);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(Role editData)

        {
            if (ModelState.IsValid)
            {
                bool RoleUniqe = roleService.UniqueRole(editData.roleName.TrimEnd());
                if (RoleUniqe == true)
                {

                    TempData["UniqeMessage"] = "Record is Exist, Please Enter New One";
                    return RedirectToAction("Edit", "Role");
                }

                    roleService.UpdateRole(editData);
                    TempData["editMessage"] = "Success ! You have modified the record";
                    return RedirectToAction("Index", "Role");
                
            }

            return View(editData);
        }

        
        //private Users GetUserSession()
        //{
        //    if (Session["user"] == null)
        //    {
        //        Session["user"] = new Users();
        //    }
        //    return (Users)Session["user"];
        //}


    }
}
