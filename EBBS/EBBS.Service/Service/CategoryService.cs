using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;
using EBBS.Data.DAO;
using EBBS.Data.IDAO;
using EBBS.Service.IService;

namespace EBBS.Service.Service
{
    public class CategoryService : ICategoryService
    {
        private ICategoryDao _categoryDao;

        public CategoryService()
        {
            _categoryDao=new CategoryDao();
        }

        public void DeleteCategory(Category deleteRole)
        {
            _categoryDao.DeleteCategory(deleteRole);
        }

        public IList<Category> GetAllCategories()
        {
            return _categoryDao.GetAllCategories();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryDao.GetCategoryById(id);
        }

        public void InsertCategory(Category newRole)
        {
            _categoryDao.InsertCategory(newRole);
        }

        public bool UniqueCategory(string category)
        {
            return _categoryDao.UniqueCategory(category);
        }

        public IEnumerable<Category> LastCategory => _categoryDao.LastCategory; 

        public void IncreaseFreqOne(int id)
        {
            _categoryDao.IncreaseFreqOne(id);
        }

        public void DecreaseFreqOne(int id)
        {
            _categoryDao.DecreaseFreqOne(id);
        }

        public IQueryable<Category> SpecificRecordList => _categoryDao.SpecificRecordList; 
        

        public void UpdateCategory(Category editRole)
        {
            _categoryDao.UpdateCategory(editRole);
        }

        public bool AnybodyGotThisCategory(int categoryId)
        {
            return _categoryDao.AnybodyGotThisCategory(categoryId);
        }

        //public void AddEditCategory(Category addEditCategory)
        //{
        //    _categoryDao.AddEditCategory(addEditCategory);
        //}
    }
}
