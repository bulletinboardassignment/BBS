using EBBS.Data.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EBBS.Data.DAO
{
    public class PostDAO : IPostDAO
    {
        EbbSEntities context;
        private ICommentDAO commentDao;
        public PostDAO() {
            context = new EbbSEntities();
            commentDao = new CommentDAO();
        }

        public void Add(Post post)
        {
            context.Post.Add(post);
            context.SaveChanges();
        }

        public void Delete(int postId)
        {
            context.Post.Remove(GetPost(postId));
            context.SaveChanges();
        }





        public void Edit(int oldPostId, Post newPost)
        {
            Post oldPost = GetPost(oldPostId);
            oldPost.categoryId = newPost.categoryId;
            oldPost.mediaPath = newPost.mediaPath;
            oldPost.postTitle = newPost.postTitle;
            oldPost.postContent = newPost.postContent;
            oldPost.isReported = newPost.isReported;
            oldPost.mediaType = newPost.mediaType;
            oldPost.updateTime = newPost.updateTime;
            context.SaveChanges();
        }

        public List<Post> GetAllPosts()
        {
            return context.Post.Where(x=> x.isReported != true).OrderByDescending(x=> x.createTime).ToList();
        }

        public Post GetPost(int postId)
        {
            return context.Post.Where(x => x.pId == postId).FirstOrDefault();
        }
        

        public void LikePost(Like like)
        {
            Like likeBefore = context.Like.Where(x => x.likedBy == like.likedBy && x.postId == like.postId).FirstOrDefault();
            if (likeBefore == null)
            {
                context.Like.Add(like);

            }
            else {
                if (likeBefore.vote == "dislike") {
                    likeBefore.vote = "like";
                    likeBefore.likedOn = like.likedOn;
                }
            }
            context.SaveChanges();
        }

        public void DislikePost(Like like)
        {
            Like likeBefore = context.Like.Where(x => x.likedBy == like.likedBy && x.postId == like.postId).FirstOrDefault();
            if (likeBefore == null)
            {
                context.Like.Add(like);

            }
            else
            {
                if (likeBefore.vote == "like")
                {
                    likeBefore.vote = "dislike";
                    likeBefore.likedOn = like.likedOn;
                }
            }
            context.SaveChanges();
        }

        public void ReportPost(int postId)
        {
            Post post = GetPost(postId);
            post.isReported = true;
            context.SaveChanges();
        }

        public List<Post> GetAllPostsByUser(int userId)
        {
            return context.Post.Where(x => x.creatorId == userId).OrderByDescending(x => x.createTime).ToList();
        }

        public List<Post> GetAllPostsInCategory(int id)
        {
            return context.Post.Where(x => x.categoryId == id).OrderByDescending(x => x.createTime).ToList();
        }

        public int? GetNumberOfLikes(int pId)
        {
            return context.Like.Where(x => x.postId == pId && x.vote == "like").Count();
        }

        public int? GetNumberOfDislikes(int pId)
        {
            return context.Like.Where(x => x.postId == pId && x.vote == "dislike").Count();
        }

        public void DeleteCommentsForPost(int id)
        {
            List<Comment> comments = commentDao.GetAllCommentsForPost(id);
            foreach(var comment in comments) {
                commentDao.DeleteComment(comment.commentId);
            }

            context.SaveChanges();
        }

        public int AllPostsInThisMonthAndYear(string month, string year)
        {
            return context.Post.Where(x => x.createTime.Value.Month.ToString().Equals(month) && x.createTime.Value.Year.ToString().Equals(year)).Count();
        }
    }
}
