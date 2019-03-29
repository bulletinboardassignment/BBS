using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data.IDAO;

namespace EBBS_1.Data.DAO
{
    public class CommentDAO : ICommentDAO
    {
        private EBBSEntities context;

        public CommentDAO()
        {
            context = new EBBSEntities();
        }

        public IEnumerable<Comments> CommentIEnum
        {
            get { return context.Comments; }
        }

        public IQueryable<Comments> CommentList
        {
            get { return context.Comments.AsQueryable(); }
        }

        public IEnumerable<Comments> Last10Comment
        {
            get { return context.Comments.OrderByDescending(c => c.Create_time).Take(10); }
        }

        public Comments Details(int? Id)
        {
            Comments dbEntry = context.Comments.Find(Id);

            return dbEntry;
        }

        public Comments Delete(int? Id)
        {
            Comments dbEntry = context.Comments.Find(Id);
            if (dbEntry != null)
            {
                context.Comments.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public void Save(Comments commnet)
        {


            if (commnet.CommentId == 0)
            {
                Comments _Comment = new Comments();
                _Comment.Comment_Content = commnet.Comment_Content;
                _Comment.Create_time = commnet.Create_time;
                _Comment.Update_time = commnet.Update_time;

                _Comment.UserId = commnet.UserId;
                _Comment.PostId = commnet.PostId;
                _Comment.Publish = commnet.Publish;

                context.Comments.Add(_Comment);

                context.SaveChanges();



            }
            else
            {
                Comments dbEntry = context.Comments.Find(commnet.CommentId);
                if (dbEntry != null)
                {
                    dbEntry.CommentId = commnet.CommentId;
                    dbEntry.Comment_Content = commnet.Comment_Content;
                    dbEntry.Update_time = commnet.Update_time;
                    dbEntry.UserId = commnet.UserId;
                    dbEntry.PostId = commnet.PostId;
                    dbEntry.Publish = commnet.Publish;
                    context.SaveChanges();
                    commnet.CommentId = dbEntry.CommentId;
                }
            }

        }
    }
}