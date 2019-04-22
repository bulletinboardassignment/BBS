using EBBS.Data;
using EBBS.Data.DAO;
using EBBS.Data.IDAO;
using EBBS.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Service.Service
{
    public class PostService : IPostService
    {
        private IPostDAO postDao;
        public PostService() {
            postDao = new PostDAO();
        }

        public void Add(Post post)
        {
            postDao.Add(post);
        }

        public void Delete(int postId)
        {
            postDao.Delete(postId);
        }

        public void DeleteCommentsForPost(int id)
        {
            postDao.DeleteCommentsForPost(id);
        }

        public void DislikePost(Like like)
        {
            postDao.DislikePost(like);
        }

        public void Edit(int oldPostId, Post newPost)
        {
            postDao.Edit(oldPostId, newPost);
        }

        public List<Post> GetAllPosts()
        {
            return postDao.GetAllPosts();
        }

        public List<Post> GetAllPostsByUser(int userId)
        {
            return postDao.GetAllPostsByUser(userId);
        }

        public List<Post> GetAllPostsInCategory(int id)
        {
            return postDao.GetAllPostsInCategory(id);
        }

        public int? GetNumberOfDislikes(int pId)
        {
            return postDao.GetNumberOfDislikes(pId);
        }

        public int? GetNumberOfLikes(int pId)
        {
            return postDao.GetNumberOfLikes(pId);
        }

        public Post GetPost(int postId)
        {
            return postDao.GetPost(postId);
        }

        public void LikePost(Like like)
        {
            postDao.LikePost(like);
        }

        public void ReportPost(int postId)
        {
            postDao.ReportPost(postId);
        }
    }
}
