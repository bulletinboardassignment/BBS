using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS_1.Data.IDAO
{
  public  interface ICommentDAO
    {
        void Save(Comments commnet);
        IEnumerable<Comments> CommentIEnum { get; }
        IEnumerable<Comments> Last10Comment { get; }

        Comments Details(int? Id);
        IQueryable<Comments> CommentList { get; }

        Comments Delete(int? Id);

    }
}
