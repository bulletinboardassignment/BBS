using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Services.IService
{
    public interface ISecurityQuestionService
    {
        IList<EBBS.Data.SecurityQuestion> GetAllSecurityQuestions();
    }
}
