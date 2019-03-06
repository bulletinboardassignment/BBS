using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.IDAO
{
    public interface ISecurityQuestionDAO
    {

        IList<EBBS.Data.SecurityQuestion> GetAllSecurityQuestions();
    }
}
