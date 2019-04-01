using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;
using EBBS.Data.DAO;
using EBBS.Data.IDAO;
using EBBS.Service.IService;

namespace EBBS.Service.Service
{
    public class SecurityQuestionService : ISecurityQuestionService
    {
        private ISecurityQuestionDAO _questionDao;

        public SecurityQuestionService()
        {
            _questionDao=new SecurityQuestionDAO();
        }

        public void DeleteSecurityQuestion(SecurityQuestion deleteSecurityQuestion)
        {
            _questionDao.DeleteSecurityQuestion(deleteSecurityQuestion);
        }


        public IList<SecurityQuestion> GetAllSecurityQuestions()
        {
            return _questionDao.GetAllSecurityQuestions();
        }

        public SecurityQuestion GetSecurityQuestionById(int id)
        {
            return _questionDao.GetSecurityQuestionById(id);
        }

        public void InsertSecurityQuestion(SecurityQuestion newSecurityQuestion)
        {
            _questionDao.InsertSecurityQuestion(newSecurityQuestion);
        }

        public bool UniqueRole(string secQuestion)
        {
            return _questionDao.UniqueRole(secQuestion);
        }

        public void UpdateSecurityQuestion(SecurityQuestion editSecurityQuestion)
        {
            _questionDao.UpdateSecurityQuestion(editSecurityQuestion);
        }
    }
}
