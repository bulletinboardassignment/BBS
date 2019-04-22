using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;

namespace EBBS.Service.IService
{
    public interface ICommentService
    {
        List<Comment> GetAllCommentsForPost(int postId);
        void Add(Comment comment);
        List<Comment> GetAllCommentsByUser(int userId);
        Comment GetComment(int id);
        void EditComment(int oldCommentId, Comment newComment);
        void DeleteComment(int commentId);
    }
}
