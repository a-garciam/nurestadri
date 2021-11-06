using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos.LikeDao
{


    // Creo que no hace falta este Dao, ya que nunca hay que sacar los likes si ya tenemos
    // guardado en Image el like counter (aunque en el caso de tener que comprobar si el user dio like si)



    public interface ILikeDao : IGenericDao<Like, Int32>
    {
        /// <summary>
        /// Finds a Like by a given imageId and userId
        /// </summary>
        /// <param name="imageId">the id of the image</param>
        /// <param name="userId">the id of the user</param>
        /// <returns>Boolean</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Boolean ExistsLike(Int32 imageId, Int32 userId);
    }
}
