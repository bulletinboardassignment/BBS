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
        List<SecurityQuestion> GetMySQs();

        SecurityQuestion GetSecurityQuestionById(int id);

        void InsertSecurityQuestion(SecurityQuestion newSecurityQuestion);

        void UpdateSecurityQuestion(SecurityQuestion editSecurityQuestion);

        void DeleteSecurityQuestion(SecurityQuestion deleteSecurityQuestion);

        bool UniqueSecurityQuestion(string secQuestion);

        IEnumerable<SecurityQuestion> SqIeNum { get; }
    }
}
