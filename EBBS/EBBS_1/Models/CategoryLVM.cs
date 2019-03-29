using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EBBS_1.Data;

namespace EBBS_1.Models
{
    public class CategoryLVM
    {
        public IEnumerable<Posts> Posts { get; set; }
        public int CurrentCategory { get; set; }
    }
}