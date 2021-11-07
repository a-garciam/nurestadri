using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.LikeDao;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Model.Utils;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService
{
    public class ImageService : IImageService
    {
        [Inject]
        public IUserDao UserDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }
        
        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public ILikeDao LikeDao { private get; set; }

        [Transactional]
        public void DeleteImage(int imageId)
        {
            Image image = ImageDao.Find(imageId);
            foreach(Comment comment in CommentDao.FindByImageId(image.imageId))
            {
                CommentDao.Remove(comment.commentId);
            }
            foreach (Like like in LikeDao.FindByImageId(image.imageId))
            {
                CommentDao.Remove(like.likeId);
            }
            ImageDao.Remove(imageId);

        }

        public IList<ImageOutput> FindImagesByFilter(string keywords)
        {

            keywords = keywords.Trim();

            IList<Image> images = ImageDao.FindByFilter(keywords);

            IList<ImageOutput> imageOutputs = new List<ImageOutput>();
            foreach (Image image in images)
            {
                Category category = CategoryDao.Find(image.Category.categoryId);
                User user = UserDao.Find(image.User.usrId);
                imageOutputs.Add(new ImageOutput(
                    image.imageData,
                    image.title,
                    user.usrId,
                    category.categoryId,
                    image.likes
                ));
            }

            return imageOutputs;

        }

        public ImageOutput FindImageById(int imageId)
        {
            Image image = ImageDao.Find(imageId);
            Category category = CategoryDao.Find(image.Category.categoryId);
            User user = UserDao.Find(image.User.usrId);
            ImageOutput imageOutput = new ImageOutput(
                image.imageData,
                image.title,
                user.usrId,
                category.categoryId,
                image.likes
            );

            return imageOutput;
        }

        public int UploadImage(int userId, int categoryId, string title, string description, string aperture, byte[] imageData)
        {
            User user = UserDao.Find(userId);

            Category category = CategoryDao.Find(categoryId);

            if (!PropertyValidator.IsValidImageAperture(aperture)){
                throw new FormatException();
            }

            Image image = new Image() 
            { 
                User = user,
                Category = category,
                likes = 0,
                title = title,
                description = description,
                aperture = aperture,
                imageData = imageData
            };
            ImageDao.Create(image); 

            return image.imageId;
        }

    }
}
