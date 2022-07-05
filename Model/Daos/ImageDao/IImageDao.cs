using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos
{
    public interface IImageDao : IGenericDao<Image, long>
    {
        IList<Image> FindByFilter(string keywords);

        IList<Image> FindByFilterAndCategory(string keywords, long categoryÏd);

        IList<Image> FindByUser(long userId);
        Image FindByTitle(string title);
    }
}
