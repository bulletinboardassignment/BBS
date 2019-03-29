using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data;
using EBBS_1.Data.IDAO;
using EBBS_1.Data.DAO;
using EBBS_1.Service.IService;

namespace EBBS_1.Service.Service
{
    public class RoleService : IRoleService

    {
        private IRoleDAO roleDao;

        public RoleService()
        {
            roleDao = new RoleDAO();
        }

        public IEnumerable<Roles> RoleIEnum
        {
            get { return roleDao.RoleIEnum; }
        }
    }
}
