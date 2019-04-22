using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBBS.Models
{
    public class LikeViewModel
    {
        public string vote { get; set; }
        public int likedBy { get; set; }
        public int postId { get; set; }
    }
}