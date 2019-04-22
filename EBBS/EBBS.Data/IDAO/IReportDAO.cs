using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBBS.Data.IDAO
{
    public interface IReportDAO
    {
        void Add(Reports report);
        Reports GetReport(int reportId);
        void Delete(int reportId);
        List<Reports> GetAllReports();
        void EditReport(int oldReportId, Reports newReport);

        List<Reports> GetAllReportedPosts();
        void AllowReportedPost(int id);
        void DeleteReportedPost(int id);
    }
}
