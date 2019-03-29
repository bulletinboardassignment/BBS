
using EBBS_1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBBS_1.Models
{
    public class PostViewModel
    {
        public IEnumerable<Images> Images { get; set; }
        public IEnumerable<Users> Users { get; set; }
        public IEnumerable<Posts> Posts { get; set; }
        public IEnumerable<Categories> Categories { get; set; }
        public IEnumerable<Comments> Comments { get; set; }
       public Posts Post { get; set; }
       virtual public IEnumerable<Users> UserDetails{get;set;}

      
        
        public int? CurrentCategory { get; set; }
        public string HomeImage { get; set; }
        public string HomeImageText { get; set; }
        public string DisplayLastCategory { get; set; }
        public string DisplayLastPost { get; set; }
       
    }
}