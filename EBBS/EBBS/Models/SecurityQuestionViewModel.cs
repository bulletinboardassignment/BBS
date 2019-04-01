using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBBS.Data;
using AutoMapper;

namespace EBBS.Models
{
  public partial class SecurityQuestionViewModel
    {
        [Key]
        [Display(Name = "Question ID")]
        public int qId { get; set; }

        [Required]
        [Display(Name = "Security Question")]
        public string question { get; set; }
    }


    public class SecurityQuestionVm
    {
        [Display(Name = "Question ID")]
        public int qId { get; set; }
       
        public PagedList.IPagedList<SecurityQuestionViewModel> Question { get; set; }
    }
}