using EBBS.Data.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.DAO
{
    public class SecurityQuestionDAO : ISecurityQuestionDAO
    {

        private EbbsEntities _context;

        public SecurityQuestionDAO() {
            _context = new EbbsEntities();
        }

        public IList<SecurityQuestion> GetAllSecurityQuestions()
        {
            IQueryable<SecurityQuestion> securityQuestions = from catQuestion
                                                             in _context.SecurityQuestion
                                                             select catQuestion;

            return securityQuestions.ToList<SecurityQuestion>();
           
        }
    }
}
