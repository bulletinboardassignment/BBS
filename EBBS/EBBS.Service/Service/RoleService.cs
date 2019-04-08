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
        private IRoleDao _roleDao;

        public IEnumerable<Role> RoleIeNum => _roleDao.RoleIeNum;

        public RoleService()
        {
            _roleDao = new RoleDao();
        }

        public IList<Role> GetAllRoles()
        {
            return _roleDao.GetAllRoles();
        }

        public Role RoleById(int id)
        {
            return _roleDao.RoleById(id);
        }

 
        public bool UniqueRole(string uniqueRole)
        {
            return _roleDao.UniqueRole(uniqueRole);
        }

        public void InsertRole(Role newRole)
        {
            _roleDao.InsertRole(newRole);
        }

        public void UpdateRole(Role editRole)
        {
            _roleDao.UpdateRole(editRole);
        }

        public void DeleteRole(Role deleteRole)
        {
            _roleDao.DeleteRole(deleteRole);
        }
        
    }
}
