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
    public class DEncryptionService : IDEncryptionService
    {
        private IDEncryptionDAO dEncryptionDao;

        public DEncryptionService()
        {
            dEncryptionDao=new DEncryptionDAO();
        }

        public string Decrypt(string cipherText)
        {
            return dEncryptionDao.Decrypt(cipherText);
        }

        public string Encrypt(string clearText)
        {
            return dEncryptionDao.Encrypt(clearText);
        }
    }
}
