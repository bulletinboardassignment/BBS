using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.IDAO
{
    public interface ILogDAO
    {
        void Add(Logs log);
        List<Logs> GetAllLogs();
        Logs GetLog(int id);
        void Delete(int logId);
    }
}
