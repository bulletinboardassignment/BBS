using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data;

namespace EBBS_1.Service.IService
{
    public interface IUserService
    {
        bool Save(Users user);
        Users Details(int? Id);
        IEnumerable<Users> UserIEmum { get; }
        IQueryable<Users> UserList { get; }
        Users Delete(int? Id);

        bool UniqueEmail(string email);
        bool ValidateUser(string username, string password);
    }
}
