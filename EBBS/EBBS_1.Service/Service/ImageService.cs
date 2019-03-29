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
    public class ImageService : IImageService
    {
        private IImageDAO imageDao;

        public ImageService()
        {
            imageDao = new ImageDAO();
        }

        public IEnumerable<Images> ImageIEnum
        {
            get { return imageDao.ImageIEnum; }
        }

        public IQueryable<Images> ImageList
        {
            get { return imageDao.ImageList; }
        }

        public Images Delete(int? Id)
        {
            return imageDao.Delete(Id);
        }

        public Images Details(int? Id)
        {
            return imageDao.Details(Id);
        }

        public bool Save(Images image)
        {
            return Save(image);
        }
    }
}
