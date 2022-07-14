using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources
{
    public class ImageBlock
    {
        public IList<ImageOutput> Images { get; private set; }
        public bool ExistMoreImages { get; private set; }

        public ImageBlock(IList<ImageOutput> images, bool existMoreImages)
        {
            this.Images = images;
            this.ExistMoreImages = existMoreImages;
        }
    }
}