using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBBS_1.Models
{
    public class AdvancedSettings
    {
        [DisplayName("Last Category")]
        public bool DisplayLastCategory { get; set; }
        [DisplayName("Last Post")]
        public bool DisplayLastPost { get; set; }


    }
}