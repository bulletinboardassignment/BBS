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
    public class PostService :IPostService
    {
        private IPostDAO postDao;

        public PostService()
        {
            postDao =new PostDAO();
        }

        public IEnumerable<Posts> PostIEnum
        {
            get { return postDao.PostIEnum; }
        }

        public IEnumerable<Posts> LastPost
        {
            get { return postDao.LastPost; }
        }

    

        public IQueryable<Posts> PostList
        {
            get { return postDao.PostList; }
        }

        public void DecreaseFreqOne(int Id)
        {
            postDao.DecreaseFreqOne(Id);
        }

        public Posts Delete(int? Id)
        {
            return postDao.Delete(Id);
        }

        public Posts Details(int? Id)
        {
            return postDao.Details(Id);
        }

        public void IncreaseFreqOne(int Id)
        {
            postDao.IncreaseFreqOne(Id);
        }

        public Task SaveAsync(Posts post)
        {
            return postDao.SaveAsync(post);
        }
    }
}
