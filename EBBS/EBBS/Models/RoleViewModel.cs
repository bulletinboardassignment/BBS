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
    //[MetadataType(typeof(RoleMetaData))]
    public partial class RoleViewModel
    {
        [Key]
        [Display(Name = "Role ID")]
        public int rId { get; set; }

        [Required]
        [Display(Name = "Role")]
        [Index(IsUnique = true)]
        [StringLength(50,MinimumLength = 2)]
        public string roleName { get; set; }
    }

    //public class RoleMetaData
    //{
    //    [Remote("isRoleExists", "Role",ErrorMessage = "Role already exists")]
    //    public string roleName { get; set; }
    //}

    public class RoleVm
    {
        [Display(Name = "Role ID")]
        public int rId { get; set; }
        public PagedList.IPagedList<RoleViewModel> Roles { get; set; }
    }
}