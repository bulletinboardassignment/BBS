using EBBS.Data.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.DAO
{
    public class CommentDAO : ICommentDAO
    {
        private EbbSEntities context;
        public CommentDAO() {
            context = new EbbSEntities();
        }

        public void Add(Comment comment)
        {            
              context.Comment.Add(comment);
              context.SaveChanges();
          
           
        }

        public void DeleteComment(int commentId)
        {
            context.Comment.Remove(this.GetComment(commentId));
            context.SaveChanges();
        }

        public void EditComment(int oldCommentId, Comment newComment)
        {
            Comment oldComment = this.GetComment(oldCommentId);
            oldComment.commentText = newComment.commentText;
            oldComment.updateTime = DateTime.Now;
            context.SaveChanges();
        }

        public List<Comment> GetAllCommentsByUser(int userId)
        {
            return context.Comment.Where(x => x.commentedBy == userId).OrderByDescending(x => x.createTime).ToList();
        }

        public List<Comment> GetAllCommentsForPost(int postId)
        {
            return context.Comment.Where(x => x.postId == postId).ToList();
        }

        public Comment GetComment(int id)
        {
            return context.Comment.Where(x => x.commentId == id).FirstOrDefault();
        }
    }
}
