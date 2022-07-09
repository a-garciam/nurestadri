//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public long commentId { get; set; }
        public string text { get; set; }
        public long imageId { get; set; }
        public long usrId { get; set; }
        public System.DateTime creationDate { get; set; }
    
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_ImageComment
        /// </summary>
        public virtual Image Image { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_UserComment
        /// </summary>
        public virtual User User { get; set; }
    
    	/// <summary>
    	/// A hash code for this instance, suitable for use in hashing algorithms and data structures 
    	/// like a hash table. It uses the Josh Bloch implementation from "Effective Java"
        /// Primary key of entity is not included in the hash calculation to avoid errors
    	/// with Entity Framework creation of key values.
    	/// </summary>
    	/// <returns>
    	/// Returns a hash code for this instance.
    	/// </returns>
    	public override int GetHashCode()
    	{
    	    unchecked
    	    {
    			int multiplier = 31;
    			int hash = GetType().GetHashCode();
    
    			hash = hash * multiplier + (text == null ? 0 : text.GetHashCode());
    			hash = hash * multiplier + imageId.GetHashCode();
    			hash = hash * multiplier + usrId.GetHashCode();
    			hash = hash * multiplier + creationDate.GetHashCode();
    
    			return hash;
    	    }
    
    	}
        
        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
        /// <remarks>See http://www.loganfranken.com/blog/687/overriding-equals-in-c-part-1/ for detailed info </remarks>
    	public override bool Equals(object obj)
    	{
    
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type? 
    
            Comment target = obj as Comment;
    
    		return true
               &&  (this.commentId == target.commentId )       
               &&  (this.text == target.text )       
               &&  (this.imageId == target.imageId )       
               &&  (this.usrId == target.usrId )       
               &&  (this.creationDate == target.creationDate )       
               ;
    
        }
    
    
    	public static bool operator ==(Comment  objA, Comment  objB)
        {
            // Check if the objets are the same Comment entity
            if(Object.ReferenceEquals(objA, objB))
                return true;
      
            return objA.Equals(objB);
    }
    
    
    	public static bool operator !=(Comment  objA, Comment  objB)
        {
            return !(objA == objB);
        }
    
    
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
    	public override String ToString()
    	{
    	    StringBuilder strComment = new StringBuilder();
    
    		strComment.Append("[ ");
           strComment.Append(" commentId = " + commentId + " | " );       
           strComment.Append(" text = " + text + " | " );       
           strComment.Append(" imageId = " + imageId + " | " );       
           strComment.Append(" usrId = " + usrId + " | " );       
           strComment.Append(" creationDate = " + creationDate + " | " );       
            strComment.Append("] ");    
    
    		return strComment.ToString();
        }
    
    
    }
}
