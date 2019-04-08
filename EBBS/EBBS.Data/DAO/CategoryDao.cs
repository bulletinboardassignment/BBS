using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;
using EBBS.Data.IDAO;

namespace EBBS.Data.DAO
{
    public class CategoryDao : ICategoryDao
    {
        private EBBSEntities _context;

        public CategoryDao()
        {
            _context = new EBBSEntities();
        }

        public IEnumerable<Category> LastCategory
        {
            get
            {
                return _context.Category.OrderByDescending(c => c.cId).Take(20);

            }
        }

        public IQueryable<Category> SpecificRecordList =>  _context.Category.AsQueryable(); 
        

        public void DecreaseFreqOne(int id)
        {
            Category category = new Category();

            if (id != 0)
            {
                
                category = _context.Category.Find(id);
                if (category.frequency != 0)
                {
                    category.frequency = category.frequency - 1;
                }

                _context.SaveChanges();
            }
        }

        public void DeleteCategory(Category deleteCategory)
        {
            try
            {
                if (_context.Category == null)
                {
                    throw new ArgumentNullException("deleteCategory");
                }

                Category category = GetCategoryById(deleteCategory.cId);

                this._context.Category.Remove(category);

                this._context.SaveChanges();
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public IList<Category> GetAllCategories()
        {
            IQueryable<Category> categoryList;
            categoryList = from category 
                          in _context.Category
                          select category;
            return categoryList.ToList<Category>();
        }

        public Category GetCategoryById(int id)
        {
            Category singleCategory = _context.Category.SingleOrDefault(x => x.cId == id);
            return singleCategory;
        }

        public void IncreaseFreqOne(int id)
        {
            Category category = new Category();
            category = _context.Category.Find(id);
            category.frequency = category.frequency + 1;
            _context.SaveChanges();
        }

        public void InsertCategory(Category newCategory)
        {
            Category category = new Category();
            try
            {
                if (_context.Category == null)
                {
                    throw new ArgumentNullException("newCategory");
                }

                category.cId = newCategory.cId;
                category.categoryName = newCategory.categoryName;
                category.description = newCategory.description;
                category.frequency = newCategory.frequency;
                this._context.Category.Add(newCategory);
              
                this._context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public bool UniqueCategory(string category)
        {
            var result = true;

            var uniqueCategory = _context.Category.FirstOrDefault(x => x.categoryName == category);

            if (uniqueCategory != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public void UpdateCategory(Category editCategory)
        {
            try
            {
                if (_context.Category == null)
                {
                    throw new ArgumentNullException("editCategory");
                }

                Category category = GetCategoryById(editCategory.cId);
                category.categoryName = editCategory.categoryName;
                category.description = editCategory.description;
                category.frequency = editCategory.frequency;
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        //public void AddEditCategory(Category addEditCategory)
        //{

        //    if (addEditCategory.cId == 0)
        //    {
        //        Category category = new Category();

        //        category.cId = addEditCategory.cId;
        //        category.categoryName = addEditCategory.categoryName;
        //        category.description = addEditCategory.description;
        //        category.creatorId = addEditCategory.creatorId;
        //        category.createTime = DateTime.Now;
        //        category.frequency = addEditCategory.frequency;
        //        this._context.Category.Add(addEditCategory);
        //        this._context.SaveChanges();
        //    }
        //    else
        //    {
        //        Category dbEntry = _context.Category.Find(addEditCategory.cId);
        //        if (dbEntry != null)
        //        {
        //            dbEntry.cId = addEditCategory.cId;
        //            dbEntry.categoryName = addEditCategory.categoryName;
        //            dbEntry.description = addEditCategory.description;
        //            dbEntry.creatorId = addEditCategory.creatorId;
        //            dbEntry.createTime = DateTime.Now;
        //            dbEntry.frequency = addEditCategory.frequency;
        //            this._context.SaveChanges();
        //            addEditCategory.cId = dbEntry.cId;
        //        }
        //    }

        //}
    }
}
