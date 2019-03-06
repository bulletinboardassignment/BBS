using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;
using EBBS.Data.DAO;
using EBBS.Data.IDAO;

namespace EBBS.Services.Service
{
    public class SecurityQuestionService : EBBS.Services.IService.ISecurityQuestionService
    {
        private ISecurityQuestionDAO securityQuestionDAO;

        public SecurityQuestionService() {
            securityQuestionDAO = new SecurityQuestionDAO();
        }


        public IList<SecurityQuestion> GetAllSecurityQuestions()
        {
            return securityQuestionDAO.GetAllSecurityQuestions();
        }
    }
}
