using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data.IDAO;

namespace EBBS_1.Data.DAO
{
   public class AuthenticationDAO : IAuthenticationDAO
   {
       private EBBSEntities context;

       public AuthenticationDAO()
       {
           context = new EBBSEntities();
       }

        public Users Authenticate(string username, string password)
        {
            Users result = context.Users.FirstOrDefault(u => u.Email == username &&
                                                             u.Password == password);
            return result;

        }

        public bool Logout()
        {
            return true;
        }
    }
}
