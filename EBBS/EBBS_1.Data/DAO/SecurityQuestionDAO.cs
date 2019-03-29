using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data.IDAO;

namespace EBBS_1.Data.DAO
{
    public class SecurityQuestionDAO : ISecurityQuestionDAO
    {
        private EBBSEntities context;

        public SecurityQuestionDAO()
        {
            context = new EBBSEntities();
        }
        public IEnumerable<SecurityQuestions> SQIEnum
        {
            get { return context.SecurityQuestions; }
        }
    }
}
