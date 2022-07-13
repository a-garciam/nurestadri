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
        long UploadImage(long userId, long categoryId, string title, string description, string aperture, string exposure, string balance, string imageFile);

        void DeleteImage(long imageId, long userId);

        IList<ImageOutput> FindImagesByFilter(string keywords);

        IList<ImageOutput> FindImagesByFilterAndCategory(string keywords, long categoryId);

        ImageDetailsOutput FindImageById(long imageId);

        IList<ImageOutput> FindImagesByUser(long userId);

        IList<CategoryOutput> FindCategories();

        IList<ImageOutput> FindAllImages();
    }
}