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
        private ISecurityQuestionDao _questionDao;

        public SecurityQuestionService()
        {
            _questionDao=new SecurityQuestionDao();
        }

        public IEnumerable<SecurityQuestion> SqIeNum => _questionDao.SqIeNum;

        public void DeleteSecurityQuestion(SecurityQuestion deleteSecurityQuestion)
        {
            _questionDao.DeleteSecurityQuestion(deleteSecurityQuestion);
        }


    

        public SecurityQuestion GetSecurityQuestionById(int id)
        {
            return _questionDao.GetSecurityQuestionById(id);
        }

        public void InsertSecurityQuestion(SecurityQuestion newSecurityQuestion)
        {
            _questionDao.InsertSecurityQuestion(newSecurityQuestion);
        }

        public bool UniqueSecurityQuestion(string secQuestion)
        {
            return _questionDao.UniqueSecurityQuestion(secQuestion);
        }

        public void UpdateSecurityQuestion(SecurityQuestion editSecurityQuestion)
        {
            _questionDao.UpdateSecurityQuestion(editSecurityQuestion);
        }

        public List<SecurityQuestion> GetMySQs()
        {
            return _questionDao.GetMySQs();
        }

        public bool AnybodyGotThisSecurityQuestion(int sqId)
        {
            return _questionDao.AnybodyGotThisSecurityQuestion(sqId);
        }
 
    }
}
