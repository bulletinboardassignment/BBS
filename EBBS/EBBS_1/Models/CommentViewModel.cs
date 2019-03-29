using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EBBS_1.Data;

namespace EBBS_1.Models
{
    public class CommentViewModel
    {

        public IEnumerable<Comments> Comments { get; set; }
        
        public int? Id { get; set; }

    }
 
}