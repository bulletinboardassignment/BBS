using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data;
using EBBS_1.Data.IDAO;
using EBBS_1.Data.DAO;
using EBBS_1.Service.IService;


namespace EBBS_1.Service.Service
{
   public class SettingService : ISettingService
   {
       private ISettingDAO settingDao;

       public SettingService()
       {
           settingDao = new SettingDAO();
       }

       public Settings GetSetting
       {
           get { return settingDao.GetSetting; }
       }

        public Settings Details(int? Id)
        {
            return settingDao.Details(Id);
        }

        public void Save(Settings setting)
        {
           settingDao.Save(setting);
        }
    }
}
