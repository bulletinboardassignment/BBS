using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBBS.Data;
using AutoMapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EBBS.Models
{
    
    public partial class MyViewModel
    {
    
        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is Required")]
        [Index(IsUnique = true)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string username { get; set; }
        
        [Required(ErrorMessage = "Security Question is Required")]
        [DisplayName("Security Question")]
        public Nullable<int> questionId { get; set; }


        [DisplayName("Answer")]
        [Required(ErrorMessage = "Answer is Required")]
        [StringLength(50, MinimumLength = 2)]
        public string answer { get; set; }

    }

}