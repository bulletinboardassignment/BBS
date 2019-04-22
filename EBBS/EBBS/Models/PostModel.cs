using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBBS.Models
{
    public class PostModel
    {
        public PostModel() { }

        public int pId { get; set; }
        public string postTitle { get; set; }
        public string postContent { get; set; }
        public int categoryId { get; set; }
        public HttpPostedFileWrapper postImage { get; set; }
    }
}