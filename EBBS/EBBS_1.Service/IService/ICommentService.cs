using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data;

namespace EBBS_1.Service.IService
{
   public interface ICommentService
    {
        void Save(Comments commnet);
        IEnumerable<Comments> CommentIEnum { get; }
        IEnumerable<Comments> Last10Comment { get; }

        Comments Details(int? Id);
        IQueryable<Comments> CommentList { get; }

        Comments Delete(int? Id);
    }
}
