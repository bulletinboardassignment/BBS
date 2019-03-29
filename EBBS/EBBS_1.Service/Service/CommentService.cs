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
    public class CommentService : ICommentService
    {
        private ICommentDAO commentDao;

        public CommentService()
        {
            commentDao = new CommentDAO();
        }

        public IEnumerable<Comments> CommentIEnum
        {
            get { return commentDao.CommentIEnum; }
        }

        public IEnumerable<Comments> Last10Comment

        {
            get { return commentDao.Last10Comment; }
        }

        public IQueryable<Comments> CommentList
        {
            get { return commentDao.CommentList; }
        }

        public Comments Delete(int? Id)
        {
            return commentDao.Delete(Id);
        }

        public Comments Details(int? Id)
        {
            return commentDao.Details(Id);
        }

        public void Save(Comments commnet)
        {
            commentDao.Save(commnet);
        }
    }
}
