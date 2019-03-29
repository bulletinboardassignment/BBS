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
    public class UserService : IUserService
    {
        private IUserDAO userDao;

        public UserService()
        {
            userDao = new UserDAO();
        }

        public IEnumerable<Users> UserIEmum
        {
            get { return userDao.UserIEmum; }
        }

        public IQueryable<Users> UserList
        {
            get { return userDao.UserList; }
        }

        public Users Delete(int? Id)
        {
            
               return userDao.Delete(Id); 
            
        }

        public Users Details(int? Id)
        {
            return userDao.Details(Id);
        }

        public bool Save(Users user)
        {
            return userDao.Save(user);
        }

        public bool UniqueEmail(string email)
        {
            return userDao.UniqueEmail(email);
        }

        public bool ValidateUser(string username, string password)
        {
            return userDao.ValidateUser(username,password);
        }
    }
}
