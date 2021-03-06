using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Data.Common;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos.CommentDao
{
    public class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, long>, ICommentDao
    {
        /// <summary>
        /// Public Constructor
        /// </summary>
        public CommentDaoEntityFramework()
        {
        }

        /// <summary>
        /// Finds a Like by a given imageId and userId
        /// </summary>
        /// <param name="imageId">the id of the image</param>
        /// <param name="userId">the id of the user</param>
        /// <returns>Boolean</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public IList<Comment> FindByImageId(long imageId, int startIndex, int count)
        {
            IList<Comment> commentsList = null;

            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from comment in comments
                 where comment.Image.imageId == imageId
                 orderby comment.creationDate descending
                 select comment).Skip(startIndex).Take(count);

            commentsList = result.ToList();

            return commentsList;
        }

        public bool FindLike(long userId, long imageId)
        {
            DbSet<Image> image = Context.Set<Image>();

            var result = (from i in image
                          where i.imageId == imageId && i.UserLikes.Any(u => u.usrId == userId)
                          select i).Any();

            return result;
        }
    }
}