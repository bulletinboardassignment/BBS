using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS_1.Data.IDAO
{
    public interface IAuthenticationDAO
    {
        Users Authenticate(string username, string password);
        bool Logout();

    }
}
