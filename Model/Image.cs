//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Image
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Image()
        {
            this.Comment = new HashSet<Comment>();
            this.Like = new HashSet<Like>();
        }
    
        public int imageId { get; set; }
        public int likes { get; set; }
        public int usrId { get; set; }
        public int commentId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string aperture { get; set; }
        public string balance { get; set; }
        public string exposure { get; set; }
        public int categoryId { get; set; }
        public string imageUrl { get; set; }
    
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Like { get; set; }
    }
}
