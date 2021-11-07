using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService
{
    public interface IImageService
    {
        int UploadImage(int userId, int categoryId, string title, string description, string aperture, byte[] image);

        void DeleteImage(int imageId);

        IList<ImageOutput> FindImagesByFilter(string keywords);

        ImageOutput FindImageById(int imageId);
    }
}
