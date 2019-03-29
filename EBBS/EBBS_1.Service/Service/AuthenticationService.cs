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
    public class AuthenticationService : IAuthenticationService
    {
        private IAuthenticationDAO authenticationDao;

        public AuthenticationService()
        {
            authenticationDao = new AuthenticationDAO();
        }

        public Users Authenticate(string username, string password)
        {
            return authenticationDao.Authenticate(username, password);
        }

        public bool Logout()
        {
            return authenticationDao.Logout();
        }
    }
}
