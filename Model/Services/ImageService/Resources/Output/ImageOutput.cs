using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output
{
    [Serializable]
    public class ImageOutput
    {
        public ImageOutput(string imagePath, string title, long userId, long categoryId, int likes)
        {
            ImagePath = imagePath;
            Title = title;
            CategoryId = categoryId;
            UserId = userId;
            Likes = likes;
        }

        public string ImagePath { get; private set; }
        public string Title { get; private set; }
        public long CategoryId { get; private set; }
        public long UserId { get; private set; }
        public int Likes { get; private set; }
    }
}
