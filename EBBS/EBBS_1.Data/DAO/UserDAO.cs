using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data.IDAO;

namespace EBBS_1.Data.DAO
{
   public class UserDAO : IUserDAO
   {
       private EBBSEntities context;

       public UserDAO()
       {
           context=new EBBSEntities();
       }

        public IEnumerable<Users> UserIEmum
        {
            get
            {
                return context.Users;
            }
        }

        public IQueryable<Users> UserList
        {
            get
            {
                return context.Users.AsQueryable();
            }
        }

        public bool Save(Users user)
        {
           

            if (user.UserId == 0)
            {
                Users _user = new Users();
               _user.FName = user.FName;
                _user.LName = user.LName;
                _user.Email = user.Email;

                _user.Password = user.Password;

                _user.Create_time = user.Create_time;
                _user.Update_Time = user.Update_Time;
                _user.Last_Login = user.Last_Login;
                _user.RoleId = user.RoleId;
                context.Users.Add(_user);
                user.UserId = _user.UserId;
               
                int res = context.SaveChanges();

                if (res > 0)

                {
                   return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                
                Users dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.UserId = user.UserId;
                    dbEntry.FName = user.FName;
                    dbEntry.LName = user.LName;
                    dbEntry.Email = user.Email;
                    dbEntry.Password = user.Password;
                    dbEntry.Create_time = user.Create_time;
                    dbEntry.Update_Time = user.Update_Time;
                    dbEntry.Last_Login = user.Last_Login;
                    dbEntry.RoleId = user.RoleId;

                    

                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                  
                }
            }


            return true;
        }

        public Users Details(int? Id)
        {
            Users dbEntry = context.Users.Find(Id);

            return dbEntry;
        }

        public Users Delete(int? Id)
        {
            Users dbEntry = context.Users.Find(Id);
            if (dbEntry != null)
            {
                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool UniqueEmail(string email)
        {
            var result = true;
            var res = context.Users.FirstOrDefault(x => x.Email == email);
            if (res != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool ValidateUser(string username, string password)
        {
            // string HashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
            Users _user = context.Users.Where(a => a.Email.Equals(username) && a.Password.Equals(password)).FirstOrDefault();
            if (_user != null)
            {
                return true;
            }
            return false;
        }
    }
}
