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
        
        public RoleController()
        {
            _roleService = new RoleService();
        }
        
        // GET: Role
      
        public ActionResult Index(int page =1, int pageSize=5)
        {
            User user = GetUserSession();
            string userType = user.Role.roleName;
            ViewBag.userType = userType;
            //Show list of roles and list in descending order of the role id
            var roleList = _roleService.GetAllRoles().OrderBy(p=>p.rId).OrderByDescending(p=>p.rId);
            //mapping with the RoleViewModel to show the pagination on the view page
            var roleViewList = AutoMapper.Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(roleList);
            var model = new RoleVm();
            //Configure to show only  5 categories per page
            model.Roles = roleViewList.ToPagedList(page,pageSize);
            return View(model);
         }

       

        // GET: Role/Create
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public ActionResult Create(RoleViewModel data)

        {
            User user = GetUserSession(); //store to logged in user session into the user object
           
            Role obj = new Role();
            //if the enterd role details valid, allows to save the data in database
            if (ModelState.IsValid)
            {
                bool role = _roleService.UniqueRole(data.roleName.TrimEnd());
                //check whether the entered role is exisits in the database
                if (role == true)
                {
                    TempData["addUniqueMessage"] = "Record is Exist, Please Enter a New One";
                    return RedirectToAction("Create", "Role");
                }
                obj.roleName = data.roleName.TrimEnd();
                //save the entred role details
                _roleService.InsertRole(obj);//add the data
                //Show success message when entered details are correct
                TempData["message"] = "Success ! You have created a new record";
                //User redirects to the role Index page after successfull role creation
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
            //Find the role id of selected role from database to delete
            Role role = _roleService.RoleById(id);
            if (role == null)
            {
                return HttpNotFound();//if category is null show the http not found error
            }
            return View(role);
        }

        // POST: SecurityQuestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = _roleService.RoleById(id);//Find the role id of selected role from database to delete
            _roleService.DeleteRole(role);//delete the role
            //Show success message when deletion is succesfful
            TempData["deleteMessage"] = "Success ! You have deleted a record";
            //User redirects to the role Index page after successfull role deletion
            return RedirectToAction("Index");
        }


        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role dataEdit = _roleService.RoleById(id);//Find the role id of selected role from database to edit

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
                //check whether the entered role is exisits in the database
                if (uniquesRole == true)
                {
                    TempData["uniqueMessage"] = "Record is Exist, Please modify with a new user role";
                    return RedirectToAction("Edit", "Role");
                }

                _roleService.UpdateRole(editData);//modify the data
                TempData["editMessage"] = "Success ! You have modified the record"; //Show success message when deletion is succesfful
                return RedirectToAction("Index", "Role");//User redirects to the role Index page after successfull role deletion

            }

            return View(editData);
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
