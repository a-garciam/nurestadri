using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos
{
    public interface IImageDao : IGenericDao<Image, long>
    {
        IList<Image> FindByFilter(string keywords, int startIndex, int count);

        IList<Image> FindByFilterAndCategory(string keywords, long categoryÏd, int startIndex, int count);

        IList<Image> FindByUser(long userId, int startIndex, int count);

        Image FindByTitle(string title);

        IList<Image> FindAll(int startIndex, int count);
    }
}