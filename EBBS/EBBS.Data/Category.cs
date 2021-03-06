//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EBBS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.Post = new HashSet<Post>();
        }
        [Key]
        [Display(Name = "Category ID")]
        public int cId { get; set; }
        [Required(ErrorMessage = "Title Required")]
        [StringLength(50, ErrorMessage = "Title cannot be more than 50 characters"), MinLength(2)]
        [Index(IsUnique = true)]
        [Display(Name = "Title")]
        public string categoryName { get; set; }
        [Required(ErrorMessage = "Description Required")]
        [StringLength(100, ErrorMessage = "{0} cannot be longer than 100 characters"), MinLength(5)]
        [Display(Name = "Description")]
        public string description { get; set; }
        public Nullable<int> frequency { get; set; }
        [Display(Name = "Creator")]
        public Nullable<int> creatorId { get; set; }

        [Display(Name = "Created On")]
        public Nullable<System.DateTime> createTime { get; set; }
        

        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Post { get; set; }
    }
}
