using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBBS.Models
{
    public class UserUpdateModel
    {
        public UserUpdateModel() { }

        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public int qId { get; set; }
        public string answer { get; set; }
        public HttpPostedFileWrapper userImage { get; set; }
    }
}