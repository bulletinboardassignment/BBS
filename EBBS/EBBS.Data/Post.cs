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
    
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            this.Comment = new HashSet<Comment>();
            this.Reports = new HashSet<Reports>();
        }
    
        public int pId { get; set; }
        public string postTitle { get; set; }
        public string postContent { get; set; }
        public string mediaPath { get; set; }
        public Nullable<int> creatorId { get; set; }
        public Nullable<int> categoryId { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public Nullable<System.DateTime> updateTime { get; set; }
        public Nullable<int> frequency { get; set; }
        public Nullable<bool> isReported { get; set; }
        public string mediaType { get; set; }

        public Nullable<int> nLikes { get; set; }
        public Nullable<int> nDislikes { get; set; }

        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reports> Reports { get; set; }
        public virtual User User { get; set; }
    }
}
