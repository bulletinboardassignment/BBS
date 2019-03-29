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
    public class CategoryService : ICategoryService
    {
        private ICategoryDAO categoryDao;

        public CategoryService()
        {
            categoryDao=new CategoryDAO();
        }

        public IEnumerable<Categories> LastCategory
        {
            get { return categoryDao.LastCategory; }
        }

        public IEnumerable<Categories> CategoryIEnum
        {
            get { return categoryDao.CategoryIEnum; }
        }

        public IQueryable<Categories> CategoryIList
        {
            get { return categoryDao.CategoryIList; }
        }

        public void DecreaseFreqOne(int Id)
        {
            
          categoryDao.DecreaseFreqOne(Id); 
            
        }

        public Categories Delete(int? Id)
        {
           return categoryDao.Delete(Id);
        }

        public Categories Details(int? Id)
        {
            return categoryDao.Details(Id);
        }

        public void IncreaseFreqOne(int Id)
        {
            categoryDao.IncreaseFreqOne(Id);
        }

        public void Save(Categories category)
        {
            categoryDao.Save(category);
        }
    }
}
