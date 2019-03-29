using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data.IDAO;

namespace EBBS_1.Data.DAO
{
   public class PostDAO :IPostDAO
    {
        private EBBSEntities context;

        public PostDAO()
        {
            context = new EBBSEntities();
        }

        public IEnumerable<Posts> LastPost
        {
            get
            {
                context.Database.Log = s => Debug.WriteLine(s);
                return context.Posts.OrderByDescending(c => c.Create_time).Take(10);
            }
        }

        public IEnumerable<Posts> PostIEnum
        {
            get
            {
                return context.Posts;
            }
        }

        public IQueryable<Posts> PostList
        {

            get
            {
                context.Database.Log = s => Debug.WriteLine(s);
                return context.Posts.AsQueryable();

            }
        }

        public Posts Delete(int? Id)
        {
            Posts dbEntry = context.Posts.Find(Id);
            if (dbEntry != null)
            {
                context.Posts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Posts Details(int? Id)
        {
            Posts dbEntry = context.Posts.Find(Id);
            return dbEntry;
        }

        public void IncreaseFreqOne(int Id)
        {
            if (Id != 0)
            {
                Posts model = new Posts();
                model = context.Posts.Find(Id);
                model.Frequence = model.Frequence + 1;
                context.SaveChanges();
            }
        }
        public void DecreaseFreqOne(int Id)
        {
            Posts model = new Posts();
            if (Id != 0)
            {


                model = context.Posts.Find(Id);
                if (model.Frequence != 0)
                {
                    model.Frequence = model.Frequence - 1;
                }
                context.SaveChanges();
            }
        }

        public async Task SaveAsync(Posts post)
        {

            if (post.PostId == 0)
            {
                var _post = new Posts();//To get Post ID From AddPost to use it for Details
                _post.Title = post.Title;
                _post.Post_Content = post.Post_Content;
                _post.Create_time = post.Create_time;
                _post.Update_time = post.Update_time;
                _post.UserId = post.UserId;
               _post.CategoryId = post.CategoryId;
                _post.FeaturedImage = post.FeaturedImage;
                context.Posts.Add(_post);
                await context.SaveChangesAsync();
                post.PostId = _post.PostId;
            }
            else
            {
                Posts dbEntry = context.Posts.Find(post.PostId);
                if (dbEntry != null)
                {
                    dbEntry.PostId = post.PostId;
                    dbEntry.Title = post.Title;
                    dbEntry.Post_Content = post.Post_Content;
                    dbEntry.Create_time = post.Create_time;
                    dbEntry.Update_time = post.Update_time;
                    dbEntry.CategoryId = post.CategoryId;
                    dbEntry.FeaturedImage = post.FeaturedImage;
                    await context.SaveChangesAsync();

                }
            }
        }
    }
}
