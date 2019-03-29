using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data.IDAO;

namespace EBBS_1.Data.DAO
{
    public class SettingDAO : ISettingDAO
    {
        private EBBSEntities context;

        public SettingDAO()
        {
            context = new EBBSEntities();
        }

        public Settings GetSetting
        {
            get { return context.Settings.FirstOrDefault<Settings>(); }
        }

        public Settings Details(int? Id)
        {
            Settings dbEntry = context.Settings.Find(Id);
            return dbEntry;
        }

        public void Save(Settings setting)
        {
            if (setting.Id == 0)
            {

                context.Settings.Add(setting);
                context.SaveChanges();
            }
            else
            {
                Settings dbEntry = context.Settings.Find(setting.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = setting.Id;
                    dbEntry.HomeImage = setting.HomeImage;
                    dbEntry.HomeImageText = setting.HomeImageText;
                    dbEntry.NumberOfLastPost = setting.NumberOfLastPost;
                    dbEntry.NumberOfCategory = setting.NumberOfCategory;
                    dbEntry.PostNumberInPage = setting.PostNumberInPage;
                    dbEntry.NumberOfTopPost = setting.NumberOfTopPost;
                    dbEntry.Update_Time = setting.Update_Time;
                    dbEntry.UserId = setting.UserId;
                    context.SaveChanges();


                }
            }
        }
    }
}
