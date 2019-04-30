using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.IDAO
{
    public interface IRoleDao
    {
        IList<Role> GetAllRoles();

        Role RoleById(int id);

        void InsertRole(Role newRole);

        void UpdateRole(Role editRole);

        void DeleteRole(Role deleteRole);

        bool UniqueRole(string uniqueRole);

        IEnumerable<Role> RoleIeNum { get; }

        //void Save(Role role);

        bool AnybodyGotThisUserType(int roleId);

    }
}