using EBBS.Data.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.DAO
{
    public class LogDAO : ILogDAO
    {
        private EbbSEntities context;
        public LogDAO() {
            context = new EbbSEntities();
        }

        public void Add(Logs log)
        {
            context.Logs.Add(log);
            context.SaveChanges();
        }

        public void Delete(int logId)
        {
            context.Logs.Remove(this.GetLog(logId));
        }

        public List<Logs> GetAllLogs()
        {
            return context.Logs.ToList();
        }

        public Logs GetLog(int id)
        {
            return context.Logs.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
