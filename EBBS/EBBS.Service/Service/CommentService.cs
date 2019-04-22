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
    public class CommentService : ICommentService
    {
        private ICommentDAO commentDAO;
        public CommentService() {
            commentDAO = new CommentDAO();
        }

        public void Add(Comment comment)
        {
            commentDAO.Add(comment);
        }

        public void DeleteComment(int commentId)
        {
            commentDAO.DeleteComment(commentId);
        }

        public void EditComment(int oldCommentId, Comment newComment)
        {
            commentDAO.EditComment(oldCommentId, newComment);
        }

        public List<Comment> GetAllCommentsByUser(int userId)
        {
            return commentDAO.GetAllCommentsByUser(userId);
        }

        public List<Comment> GetAllCommentsForPost(int postId)
        {
            return commentDAO.GetAllCommentsForPost(postId);
        }

        public Comment GetComment(int id)
        {
            return commentDAO.GetComment(id);
        }
    }
}
