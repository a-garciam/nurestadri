﻿using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos
{
    class ImageDaoEntityFramework : GenericDaoEntityFramework<Image, long>, IImageDao
    {
        public IList<Image> FindByFilter(string keywords)
        {
            IList<Image> filteredImages = null;

            DbSet<Image> imageContext = Context.Set<Image>();

            var result = imageContext.Where(i => i.title.ToLower().Contains(keywords.ToLower())
                || i.description.ToLower().Contains(keywords.ToLower())).ToList();

            filteredImages = result.ToList();

            return filteredImages;
        }
    }
}
