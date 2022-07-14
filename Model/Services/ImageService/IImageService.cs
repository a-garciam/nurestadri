using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources;
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

        ImageBlock FindImagesByFilter(string keywords, int startIndex, int count);

        ImageBlock FindImagesByFilterAndCategory(string keywords, long categoryId, int startIndex, int count);

        ImageDetailsOutput FindImageById(long imageId);

        ImageBlock FindImagesByUser(long userId, int startIndex, int count);

        IList<CategoryOutput> FindCategories();

        ImageBlock FindAllImages(int startIndex, int count);
    }
}