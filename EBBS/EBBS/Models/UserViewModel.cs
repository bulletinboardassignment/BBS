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
    
    public partial class UserViewModel
    {
        [Key]
        [DisplayName("Membership ID")]
        public int userId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(100, MinimumLength = 2)]
        public string firstname { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(100, MinimumLength = 2)]
        public string lastName { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is Required")]
        [Index(IsUnique = true)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string username { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [DisplayName("Create Time")]
        public Nullable<System.DateTime> createTime { get; set; }

        [DisplayName("Updated Time")]
        public Nullable<System.DateTime> updateTime { get; set; }

        [DisplayName("Last Logged in")]
        public Nullable<System.DateTime> lastLogin { get; set; }

        [DisplayName("Role")]

        public Nullable<int> userType { get; set; }

        [Required(ErrorMessage = "Security Question is Required")]
        [DisplayName("Security Question")]
        public Nullable<int> questionId { get; set; }


        [DisplayName("Answer")]
        [Required(ErrorMessage = "Answer is Required")]
        [StringLength(50, MinimumLength = 2)]
        public string answer { get; set; }

        [DisplayName("Profile Image")]
        public string userImage { get; set; }

        public Nullable<double> imageSize { get; set; }

    }


    //public class UserVm
    //{
    //    public int userId { get; set; }
    //    //public string firstName { get; set; }
    //    //public string lastName { get; set; }
    //    //public string username { get; set; }
    //    //public string password { get; set; }
    //    //public Nullable<System.DateTime> createTime { get; set; }
    //    //public Nullable<System.DateTime> updateTime { get; set; }
    //    //public Nullable<System.DateTime> lastLogin { get; set; }
    //    //public Nullable<int> userType { get; set; }
    //    //public Nullable<int> questionId { get; set; }
    //    //public string answer { get; set; }
    //    //public string userImage { get; set; }
        
    //    public PagedList.IPagedList<UserViewModel> Users { get; set; }
    //}
}