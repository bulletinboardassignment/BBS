using EBBS.Data.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.DAO
{
    public class ReportDAO : IReportDAO
    {
        EbbSEntities context;
        public ReportDAO() {
            context = new EbbSEntities();
        }

        public void Add(Reports report)
        {
            context.Reports.Add(report);
            context.SaveChanges();
        }

        public void Delete(int reportId)
        {
            context.Reports.Remove(GetReport(reportId));
        }

        public void EditReport(int oldReportId, Reports newReport)
        {
            Reports oldReport = GetReport(oldReportId);
            oldReport.reason = newReport.reason;
            context.SaveChanges();
        }

        
        public List<Reports> GetAllReports()
        {
            return context.Reports.ToList();
        }

        public Reports GetReport(int reportId)
        {
            return context.Reports.Where(x => x.rId == reportId).FirstOrDefault();
        }

        public List<Reports> GetAllReportedPosts()
        {
            return context.Reports.ToList();
        }

        public void AllowReportedPost(int id)
        {

            Reports report = context.Reports.Where(x => x.postId == id).FirstOrDefault();
            context.Reports.Remove(report);

            Post post = context.Post.Where(x => x.pId == id).FirstOrDefault();
            post.isReported = false;
            context.SaveChanges();
        }

        public void DeleteReportedPost(int id)
        {
            Reports report = context.Reports.Where(x => x.postId == id).FirstOrDefault();
            context.Reports.Remove(report);

            Post post = context.Post.Where(x => x.pId == id).FirstOrDefault();
            context.Post.Remove(post);

            context.SaveChanges();
        }
    }
}
