using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data;
using EBBS_1.Data.IDAO;


namespace EBBS_1.Data.DAO
{
   public class ImageDAO :IImageDAO
    {
        private EBBSEntities context;

        public ImageDAO()
        {
            context = new EBBSEntities();
        }

        public IEnumerable<Images> ImageIEnum
        {
            get
            {
                return context.Images;
            }


        }

        public IQueryable<Images> ImageList
        {
            get
            {
                return context.Images.AsQueryable();
            }


        }

        public Images Details(int? Id)
        {
            Images dbEntry = context.Images.Find(Id);

            return dbEntry;
        }
        public Images Delete(int? Id)
        {
            Images dbEntry = context.Images.Find(Id);
            if (dbEntry != null)
            {
                context.Images.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool Save(Images image)
        {
           

            if (image.Id == 0)
            {

                context.Images.Add(image);
                if (context.SaveChanges() > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }


            }
            else
            {
                Images dbEntry = context.Images.Find(image.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = image.Id;
                    dbEntry.ImagePath = image.ImagePath;
                    dbEntry.Size = image.Size;
                    dbEntry.Create_time = image.Create_time;
                    dbEntry.UserId = image.UserId;

                    if (context.SaveChanges() > 0)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                    //image.Id = dbEntry.Id;
                }
            }


            return true;

        }
    }
}
