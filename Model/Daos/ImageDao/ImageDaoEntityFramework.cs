using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos
{
    public class ImageDaoEntityFramework : GenericDaoEntityFramework<Image, long>, IImageDao
    {
        public IList<Image> FindByFilter(string keywords, int startIndex, int count)
        {
            IList<Image> filteredImages = null;

            DbSet<Image> imageContext = Context.Set<Image>();

            var result = imageContext.Where(i => i.title.ToLower().Contains(keywords.ToLower())
                || i.description.ToLower().Contains(keywords.ToLower())).OrderByDescending(i => i.creationDate).Skip(startIndex).Take(count).ToList();

            filteredImages = result.ToList();

            return filteredImages;
        }

        public IList<Image> FindByFilterAndCategory(string keywords, long categoryId, int startIndex, int count)
        {
            IList<Image> filteredImages = null;

            DbSet<Image> imageContext = Context.Set<Image>();

            var result = imageContext.Where(i => i.categoryId == categoryId && (i.title.ToLower().Contains(keywords.ToLower())
                || i.description.ToLower().Contains(keywords.ToLower()))).OrderByDescending(i => i.creationDate).Skip(startIndex).Take(count).ToList();

            filteredImages = result.ToList();

            return filteredImages;
        }

        public IList<Image> FindByUser(long userId, int startIndex, int count)
        {
            IList<Image> resultImages = null;

            DbSet<Image> imageContext = Context.Set<Image>();

            var result = imageContext.Where(i => i.usrId == userId).OrderByDescending(i => i.creationDate).Skip(startIndex).Take(count).ToList();

            resultImages = result.ToList();

            return resultImages;
        }

        public Image FindByTitle(string title)
        {
            DbSet<Image> imageContext = Context.Set<Image>();

            var result = imageContext.Where(i => i.title == title);

            return result.FirstOrDefault();
        }

        public IList<Image> FindAll(int startIndex, int count)
        {
            IList<Image> resultImages = null;

            DbSet<Image> imageContext = Context.Set<Image>();

            var result = imageContext.OrderByDescending(i => i.creationDate).Skip(startIndex).Take(count).ToList();

            resultImages = result.ToList();

            return resultImages;
        }
    }
}