using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data.IDAO;

namespace EBBS_1.Data.DAO
{
    public class RoleDAO : IRoleDAO
    {
        private EBBSEntities context;

        public RoleDAO()
        {
            context = new EBBSEntities();
        }

        public IEnumerable<Roles> RoleIEnum
        {
            get
            {
                return context.Roles;
            }
        }
    }
}
