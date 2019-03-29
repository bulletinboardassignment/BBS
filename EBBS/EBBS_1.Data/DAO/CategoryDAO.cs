using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data.IDAO;

namespace EBBS_1.Data.DAO
{
    public class CategoryDAO : ICategoryDAO
    {
        private EBBSEntities context;

        public CategoryDAO()
        {
            context = new EBBSEntities();
        }

        public IEnumerable<Categories> CategoryIEnum
        {
            get
            {
                return context.Categories;
            }
        }

        public IQueryable<Categories> CategoryIList
        {
            get
            {
                return context.Categories.AsQueryable();
            }
        }

        public IEnumerable<Categories> LastCategory
        {
            get
            {
                return context.Categories.OrderByDescending(c => c.Create_time).Take(20);
            }
        }

        public Categories Delete(int? Id)
        {
            Categories dbEntry = context.Categories.Find(Id);
            if (dbEntry != null)
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Categories Details(int? Id)
        {
            Categories dbEntry = context.Categories.Find(Id);

            return dbEntry;
        }

        public void IncreaseFreqOne(int Id)
        {
            if (Id != 0)
            {
                Categories model = new Categories();
                model = context.Categories.Find(Id);
                model.Frequence = model.Frequence + 1;
                context.SaveChanges();
            }
        }


        public void DecreaseFreqOne(int Id)
        {
            Categories model = new Categories();
            if (Id != 0)
            {

                model = context.Categories.Find(Id);
                if (model.Frequence != 0)
                {
                    model.Frequence = model.Frequence - 1;

                    context.SaveChanges();
                }
            }
        }

        public void Save(Categories category)
        {


            if (category.CategoryId == 0)
            {

                context.Categories.Add(category);

                context.SaveChanges();



            }
            else
            {
                Categories dbEntry = context.Categories.Find(category.CategoryId);
                if (dbEntry != null)
                {
                    dbEntry.CategoryId = category.CategoryId;
                    dbEntry.CategoryName = category.CategoryName;
                    dbEntry.Create_time = category.Create_time;
                    context.SaveChanges();
                    category.CategoryId = dbEntry.CategoryId;
                }
            }



        }
    }
}
