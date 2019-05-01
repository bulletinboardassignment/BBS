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
    
        bool UniqueEmail(string email);

        void DeleteUser(User deleteUser);

        User UserById(int id);

        bool ValidateUser(string username, string password);

        User Authenticate(string username, string password);
       
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
        List<User> GetAllUsersExceptMe(int currentUserId);

        User GetUser(int userId);
      
        void EditUser(int oldUserId, User newUser);
        string GetUserPassword(int userId);
        void ChangeUserPassword(int userId, string newPassword);
        int AreResetCredentialsTrue(string username, int sqId, string answer);
        int NoAllUsers();
        int UsersInLastMonth();
        void PromoteUser(int userId, int userType);
    }
}
