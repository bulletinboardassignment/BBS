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
        private EbbSEntities _context;

        public CategoryDao()
        {
            _context = new EbbSEntities();
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
        public bool AnybodyGotThisCategory(int categoryId)
        {
            List<Post> posts = _context.Post.ToList();
            foreach (var post in posts)
            {
                if (post.categoryId == categoryId)
                {
                    return true;
                }
            }
            return false;
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
                category.creatorId = newCategory.creatorId;
                category.createTime = DateTime.Now;
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
                category.creatorId = editCategory.creatorId;
                category.createTime = DateTime.Now;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}
