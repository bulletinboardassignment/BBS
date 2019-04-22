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
   
    public partial class CategoryViewModel
    {
        [Key]
        [Display(Name = "Category ID")]
        public int cId { get; set; }

        
        [Required(ErrorMessage = "Title Required")]
        [StringLength(50,ErrorMessage = "Title cannot be more than 50 characters"),MinLength(2)]
        [Index(IsUnique = true)]
        [Display(Name = "Title")]
        public string categoryName { get; set; }


        [Required(ErrorMessage = "Description Required")]
        [StringLength(100, ErrorMessage = "{0} cannot be longer than 100 characters"), MinLength(5)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Creator")]
        public Nullable<int> creatorId { get; set; }

        [Display(Name = "Created On")]
        public Nullable<System.DateTime> createTime { get; set; }


        public virtual User User { get; set; }


        //[Display(Name = "Frequency")]
        //public Nullable<int> frequency { get; set; }

    }



    public class CategoryVm
    {
        [Display(Name = "Category ID")]
        public int cId { get; set; }
        [Display(Name = "Title")]
        public string categoryName { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Creator")]
        public Nullable<int> creatorId { get; set; }

        [Display(Name = "Created On")]
        public Nullable<System.DateTime> createTime { get; set; }
        [Display(Name = "Frequency")]
        public Nullable<int> frequency { get; set; }

        public virtual User User { get; set; }
        public PagedList.IPagedList<CategoryViewModel> Category { get; set; }
    }
}