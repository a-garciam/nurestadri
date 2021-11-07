using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos
{
    class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, long>, ICategoryDao
    {
    }
}
