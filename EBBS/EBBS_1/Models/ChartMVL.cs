using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EBBS_1.Data;

namespace EBBS_1.Models
{
    public class ChartMVL
    {
     public   IEnumerable<Posts> Posts { get; set; }
     public   IEnumerable<Comments> Comments { get; set; }

    }

    public class ChartList3Felid
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public int Month { get; set; }
    }
}