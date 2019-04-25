using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;
using EBBS.Data.IDAO;


namespace EBBS.Data.DAO
{
    public class UserDao : IUserDao
    {
        private EbbSEntities context;

        public UserDao()
        {
            context = new EbbSEntities();

        }

        public IEnumerable<User> UserIEmum
        {
            get { return context.User; }
        }

        public IQueryable<User> UserList
        {
            get { return context.User.AsQueryable(); }
        }


        public bool Save(User addEditUser)
        {
            if (addEditUser.userId == 0)
            {
                User user = new User();

                user.firstName = addEditUser.firstName;
                user.lastName = addEditUser.lastName;
                user.username = addEditUser.username;
                user.password = addEditUser.password;
                user.createTime = DateTime.Now;
                user.updateTime = DateTime.Now;
                user.lastLogin = DateTime.Now;
                //user.questionId = addEditUser.questionId;
                //user.answer = addEditUser.answer;
                //user.userType = addEditUser.userType;
                this.context.User.Add(addEditUser);
                int result = this.context.SaveChanges();

                if (result > 0)
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
                User dbEntry = context.User.Find(addEditUser.userId);
                if (dbEntry != null)
                {
                    dbEntry.userId = addEditUser.userId;
                    dbEntry.firstName = addEditUser.firstName;
                    dbEntry.lastName = addEditUser.lastName;
                    //dbEntry.password = addEditUser.password;
                    dbEntry.createTime = DateTime.Now;
                    dbEntry.updateTime = DateTime.Now;
                    dbEntry.lastLogin = DateTime.Now;
                    dbEntry.questionId = addEditUser.questionId;
                    dbEntry.answer = addEditUser.answer;
                    dbEntry.userType = addEditUser.userType;
                    //dbEntry.userImage = addEditUser.userImage;
                    //dbEntry.imageSize = addEditUser.imageSize;
                    addEditUser.userId = dbEntry.userId;
                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public User Authenticate(string username, string password)
        {
            User result = context.User.FirstOrDefault(u => u.username == username &&
                                                           u.password == password);

            return result;

        }

        public bool Logout()
        {
            return true;
        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }

        public User Details(int? Id)
        {
            User dbEntry = context.User.Find(Id);

            return dbEntry;
        }

        public User Delete(int? id)
        {
            User dbEntry = context.User.Find(id);
            if (dbEntry != null)
            {
                context.User.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;

        }

        public bool UniqueEmail(string email)
        {
            var result = true;
            var userByUnique = context.User.Where(x => x.username == email).FirstOrDefault();

            if (userByUnique != null)
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

            User _user = context.User.Where(a => a.username.Equals(username) && a.password.Equals(password))
                .FirstOrDefault();
            if (_user != null)
            {
                return true;
            }

            return false;
        }

        public List<User> GetAllUsersExceptMe(int currentUserId)
        {
            return context.User.Where(x => x.userId != currentUserId).ToList();
        }

        public User GetUser(int userId)
        {
            return context.User.Where(x => x.userId == userId).FirstOrDefault();
        }

        public void DeleteUser(int userId)
        {
            context.User.Remove(this.GetUser(userId));
            context.SaveChanges();
        }

        public void EditUser(int oldUserId, User newUser)
        {
            User oldUser = this.GetUser(oldUserId);
            oldUser.answer = newUser.answer;
            oldUser.firstName = newUser.firstName;
            oldUser.lastName = newUser.lastName;
            oldUser.questionId = newUser.questionId;
            //oldUser.username = newUser.username; 
            oldUser.userImage = newUser.userImage; 

            context.SaveChanges();

        }

        public string GetUserPassword(int userId)
        {
            return this.GetUser(userId).password;
        }

        public void ChangeUserPassword(int userId, string newPassword)
        {
            User user = this.GetUser(userId);
            user.password = newPassword;
            context.SaveChanges();
        }

        public int AreResetCredentialsTrue(string username, int sqId, string answer)
        {
            User user = context.User.Where(x => x.username == username && x.SecurityQuestion.qId == sqId && x.answer == answer).FirstOrDefault();
            if (user != null)
            {
                return user.userId;
            }
            else {
                return -1;
            }
        }
    }

}
