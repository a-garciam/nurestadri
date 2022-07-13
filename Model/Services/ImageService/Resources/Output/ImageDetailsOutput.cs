using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output
{
    [Serializable]
    public class ImageDetailsOutput
    {
        public ImageDetailsOutput(long imageId, string imagePath, string title, string description, long userId, string userName, long categoryId,
            string categoryName, int likes, DateTime creationDate, string aperture, string balance, string exposure)
        {
            ImageId = imageId;
            ImagePath = imagePath;
            CreationDate = creationDate;
            Title = title;
            Description = description;
            CategoryId = categoryId;
            CategoryName = categoryName;
            UserId = userId;
            UserName = userName;
            Likes = likes;
            Aperture = aperture;
            Balance = balance;
            Exposure = exposure;
        }

        public long ImageId { get; private set; }
        public string ImagePath { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public long CategoryId { get; private set; }
        public string CategoryName { get; private set; }
        public long UserId { get; private set; }
        public string UserName { get; private set; }
        public int Likes { get; private set; }
        public string Aperture { get; private set; }
        public string Balance { get; private set; }
        public string Exposure { get; private set; }
    }
}