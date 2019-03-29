using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EBBS_1.Data;

namespace EBBS_1.Models
{
    public class DashbordVM
    {
        public Users User { get; set; }
        public int NumberOfNewPost { get; set; }
        public int NumberOfNewComment { get; set; }
        public int NumberOfNewCategory { get; set; }
        public int NumberOfNewUser { get; set; }
        public int NumberOfCommentNeedApprove { get; set; }
    }
}