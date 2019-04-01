using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.IDAO
{
    public interface IRoleDAO
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