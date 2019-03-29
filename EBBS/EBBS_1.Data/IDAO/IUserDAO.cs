using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EBBS_1.Data.IDAO
{
    public interface IUserDAO
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
