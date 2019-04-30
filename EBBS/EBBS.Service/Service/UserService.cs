using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;
using EBBS.Data.IDAO;
using EBBS.Data.DAO;
using EBBS.Service.IService;

namespace EBBS.Service.Service
{
    public class UserService :IUserService
    {
        private IUserDao _userDao;

        public UserService()
        {
            _userDao = new UserDao();
        }

        public bool Save(User user)
        {
           return _userDao.Save(user);
        }
        
        //public User Delete(int? id)
        //{
        //    return _userDao.Delete(id);
        //}

   
        public User Details(int? id)
        {
            return _userDao.Details(id);
        }

        public IEnumerable<User> UserIEmum
        {
            get {return _userDao.UserIEmum; }
            
        }
    
        public IQueryable<User> UserList
        {
            get { return _userDao.UserList; }

        }

        public User Authenticate(string username, string password)
        {
           return _userDao.Authenticate(username, password);
        }

        public bool Logout()
        {
            return _userDao.Logout();
        }

        public string Encrypt(string clearText)
        {
            return _userDao.Encrypt(clearText);
        }

        public string Decrypt(string cipherText)
        {
            return _userDao.Decrypt(cipherText);
        }

        public bool ValidateUser(string username, string password)
        {
           return _userDao.ValidateUser(username, password);
        }

        public bool UniqueEmail(string email)
        {
            return _userDao.UniqueEmail(email);
        }

        public List<User> GetAllUsersExceptMe(int currentUserId)
        {
            return _userDao.GetAllUsersExceptMe(currentUserId);
        }

        public User GetUser(int userId)
        {
            return _userDao.GetUser(userId);
        }

        //public void DeleteUser(int userId)
        //{
        //    _userDao.Delete(userId);
        //}

        public void EditUser(int oldUserId, User newUser)
        {
            _userDao.EditUser(oldUserId, newUser);
        }

        public string GetUserPassword(int userId)
        {
            return _userDao.GetUserPassword(userId);
        }

        public void ChangeUserPassword(int userId, string newPassword)
        {
            _userDao.ChangeUserPassword(userId, newPassword);
        }

        public int AreResetCredentialsTrue(string username, int sqId, string answer)
        {
            return _userDao.AreResetCredentialsTrue(username, sqId, answer);
        }

        public User UserById(int id)
        {
            return _userDao.UserById(id);
        }

        public void DeleteUser(User deleteUser)
        {
            _userDao.DeleteUser(deleteUser);
        }

        public int NoAllUsers()
        {
            return _userDao.NoAllUsers();
        }

        public int UsersInLastMonth()
        {
            return _userDao.UsersInLastMonth();
        }

        public void PromoteUser(int userId, int userType)
        {
            _userDao.PromoteUser(userId, userType);
        }
    }
}
