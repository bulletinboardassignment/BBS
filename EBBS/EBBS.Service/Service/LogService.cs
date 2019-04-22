using EBBS.Data;
using EBBS.Data.DAO;
using EBBS.Data.IDAO;
using EBBS.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Service.Service
{
    public class LogService : ILogService
    {
        private ILogDAO logDAO;
        public LogService() {
            logDAO = new LogDAO();
        }


        public void Add(Logs log)
        {
            logDAO.Add(log);
        }

        public void Delete(int logId)
        {
            logDAO.Delete(logId);
        }

        public List<Logs> GetAllLogs()
        {
            return logDAO.GetAllLogs();
        }

        public Logs GetLog(int id)
        {
            return logDAO.GetLog(id);
        }

    }
}
