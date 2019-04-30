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
        //User Delete(int? Id);

        void DeleteUser(User deleteUser);

        bool UniqueEmail(string email);
        User UserById(int id);

        //===============================


        bool ValidateUser(string username, string password);

        User Authenticate(string username, string password);
        bool Logout();
        List<User> GetAllUsersExceptMe(int currentUserId);
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
        string GetUserPassword(int userId);
        User GetUser(int userId);
        //void DeleteUser(int userId);
        void ChangeUserPassword(int userId, string newPassword);
        void PromoteUser(int userId, int userType);
        void EditUser(int oldUserId, User newUser);
        int AreResetCredentialsTrue(string username, int sqId, string answer);
        int NoAllUsers();
        int UsersInLastMonth();
    }
}
