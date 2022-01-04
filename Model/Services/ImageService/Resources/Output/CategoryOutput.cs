using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output
{
    [Serializable]
    public class CategoryOutput
    {
        public CategoryOutput(long categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }

        #region Properties

        public long CategoryId { get; private set; }
        public string Name { get; private set; }

        #endregion Properties
    }
}
