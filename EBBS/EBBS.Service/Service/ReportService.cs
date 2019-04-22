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
    public class ReportService : IReportService
    {
        private IReportDAO reportDAO;

        public ReportService() {
            reportDAO = new ReportDAO();
        }

        public void Add(Reports report)
        {
            reportDAO.Add(report);
        }

        public void AllowReportedPost(int id)
        {
            reportDAO.AllowReportedPost(id);
        }

        public void Delete(int reportId)
        {
            reportDAO.Delete(reportId);
        }

        public void DeleteReportedPost(int id)
        {
            reportDAO.DeleteReportedPost(id);
        }

        public void EditReport(int oldReportId, Reports newReport)
        {
            reportDAO.EditReport(oldReportId, newReport);
        }

        public List<Reports> GetAllReportedPosts()
        {
            return reportDAO.GetAllReportedPosts();
        }

        public List<Reports> GetAllReports()
        {
            return reportDAO.GetAllReports();
        }

        public Reports GetReport(int reportId)
        {
            return reportDAO.GetReport(reportId);
        }
    }
}
