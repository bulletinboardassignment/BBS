using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;

namespace EBBS.Service.IService
{
   public interface ISecurityQuestionService
    {
        IList<SecurityQuestion> GetAllSecurityQuestions();

        SecurityQuestion GetSecurityQuestionById(int id);

        void InsertSecurityQuestion(SecurityQuestion newSecurityQuestion);

        void UpdateSecurityQuestion(SecurityQuestion editSecurityQuestion);

        void DeleteSecurityQuestion(SecurityQuestion deleteSecurityQuestion);

        bool UniqueRole(string secQuestion);
    }
}
