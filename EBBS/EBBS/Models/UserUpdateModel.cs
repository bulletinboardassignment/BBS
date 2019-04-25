using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBBS.Models
{
    public class UserUpdateModel
    {
        public UserUpdateModel() { }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(100, MinimumLength = 2)]
        public string firstname { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(100, MinimumLength = 2)]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Security Question is Required")]
        [DisplayName("Security Question")]
        public int qId { get; set; }

        [DisplayName("Answer")]
        [Required(ErrorMessage = "Answer is Required")]
        [StringLength(50, MinimumLength = 2)]
        public string answer { get; set; }
        [DisplayName("Profile Image")]
        public HttpPostedFileWrapper userImage { get; set; }



    }
}