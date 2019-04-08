using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;
namespace EBBS.Service.IService
{
    public interface IUserService
    {
        bool Save(User user);
        User Details(int? Id);
        IEnumerable<User> UserIEmum { get; }
        IQueryable<User> UserList { get; }
        User Delete(int? Id);

        bool UniqueEmail(string email);


        //===============================


        bool ValidateUser(string username, string password);

        User Authenticate(string username, string password);
        bool Logout();
        string Encrypt(string clearText);
        string Decrypt(string cipherText);


    }
}
