using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using EBBS_1.Data;

namespace EBBS_1.Models
{
  public  class MyIdentity: IIdentity
    {
        public IIdentity Identity { get; set; }
        public Users User { get; set; }

        public MyIdentity(Users user)
        {
            Identity = new GenericIdentity(user.Email);
            User = user;
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }
    }
}

