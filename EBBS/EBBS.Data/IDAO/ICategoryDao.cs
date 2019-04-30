using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EBBS.Data.IDAO
{
   public interface ICategoryDao
    {
        IList<Category> GetAllCategories();

        Category GetCategoryById(int id);
        void InsertCategory(Category newCategory);

        void UpdateCategory(Category editCategory);

        void DeleteCategory(Category deleteCategory);

        bool UniqueCategory(string category);

        
        IEnumerable<Category> LastCategory { get; }
        
        void IncreaseFreqOne(int id);
        void DecreaseFreqOne(int id);
        IQueryable<Category> SpecificRecordList { get; }

        //void AddEditCategory(Category addEditCategory);
        bool AnybodyGotThisCategory(int categoryId);

    }
}
