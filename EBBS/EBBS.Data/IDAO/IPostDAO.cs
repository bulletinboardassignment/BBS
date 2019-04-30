using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.IDAO
{
    public interface IPostDAO
    {
        void Add(Post post);
        Post GetPost(int postId);
        List<Post> GetAllPosts();
        void Delete(int postId);
        void Edit(int oldPostId, Post newPost);
        void LikePost(Like like);
        void DislikePost(Like like);
        void ReportPost(int postId);
        List<Post> GetAllPostsByUser(int userId);
        List<Post> GetAllPostsInCategory(int id);
        int? GetNumberOfLikes(int pId);
        int? GetNumberOfDislikes(int pId);
        void DeleteCommentsForPost(int id);
        int AllPostsInThisMonthAndYear(string month, string year);
        int? GetNumberOfComments(int pId);
    }
}
