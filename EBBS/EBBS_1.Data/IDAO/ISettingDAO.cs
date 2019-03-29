using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS_1.Data.IDAO
{
   public  interface ISettingDAO
    {
        Settings GetSetting { get; }
        void Save(Settings setting);
        Settings Details(int? Id);

    }
}
