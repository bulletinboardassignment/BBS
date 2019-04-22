using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBBS.Models
{
    public class ReportModelView
    {
        public int postId { get; set; }
        public int reportedBy { get; set; }
        public string reason { get; set; }
    }
}