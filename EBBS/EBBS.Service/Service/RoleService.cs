using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;
using EBBS.Data.IDAO;
using EBBS.Data.DAO;
using EBBS.Service.IService;

namespace EBBS.Service.Service
{
    public class RoleService :IRoleService
    {
        private IRoleDAO roleDao;

        public RoleService()
        {
            roleDao = new RoleDAO();
        }

        public IList<Role> GetAllRoles()
        {
            return roleDao.GetAllRoles();
        }

        public Role RoleById(int id)
        {
            return roleDao.RoleById(id);
        }

 
        public bool UniqueRole(string roleName)
        {
            return roleDao.UniqueRole(roleName);
        }

        public void InsertRole(Role newRole)
        {
            roleDao.InsertRole(newRole);
        }

        public void UpdateRole(Role editRole)
        {
            roleDao.UpdateRole(editRole);
        }

        public void DeleteRole(Role deRole)
        {
            roleDao.DeleteRole(deRole);
        }

        //public void Save(Role role)
        //{
        //    roleDao.Save(role);
        //}
    }
}
