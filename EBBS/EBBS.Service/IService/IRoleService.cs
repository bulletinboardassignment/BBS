using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;


namespace EBBS.Service.IService
{
    public interface IRoleService
    {
        IList<Role> GetAllRoles();

        Role RoleById(int id);

        void InsertRole(Role newRole);

        void UpdateRole(Role editRole);

        void DeleteRole(Role deRole);

        bool UniqueRole(string roleName);
        //void Save(Role role);

    }
}
