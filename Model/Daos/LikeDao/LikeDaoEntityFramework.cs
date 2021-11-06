using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Data.Common;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos.LikeDao
{
    public class LikeDaoEntityFramework : GenericDaoEntityFramework<Like, Int32>, ILikeDao
    {
        /// <summary>
        /// Public Constructor
        /// </summary>
        public LikeDaoEntityFramework()
        {
        }

        /// <summary>
        /// Finds a Like by a given imageId and userId
        /// </summary>
        /// <param name="imageId">the id of the image</param>
        /// <param name="userId">the id of the user</param>
        /// <returns>Boolean</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public Boolean ExistsLike(Int32 imageId, Int32 userId)
        {
            Like like = null;

            DbSet<Like> likes = Context.Set<Like>();
            
            var result =
                (from u in likes
                 where u.Image.imageId == imageId && u.User.usrId == userId
                 select u);

            like = result.FirstOrDefault();

            if (like == null)
                throw new InstanceNotFoundException(imageId,
                    typeof(Like).FullName);

            return like != null;
        }
    }
}