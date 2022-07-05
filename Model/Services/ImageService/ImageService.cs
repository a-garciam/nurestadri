using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Model.Utils;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService
{
    public class ImageService : IImageService
    {
        private const string imagesDirectory = "images/";

        [Inject]
        public IUserDao UserDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }
        
        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }


        public ImageOutput FindImageById(long imageId)
        {
            Image image = ImageDao.Find(imageId);
            Category category = CategoryDao.Find(image.Category.categoryId);
            User user = UserDao.Find(image.User.usrId);
            ImageOutput imageOutput = new ImageOutput(
                image.imagePath,
                image.title,
                user.usrId,
                category.categoryId,
                category.name,
                image.likes
            );

            return imageOutput;
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
                    image.imagePath,
                    image.title,
                    user.usrId,
                    category.categoryId,
                    category.name, 
                    image.likes
                ));
            }

            return imageOutputs;

        }


        public IList<ImageOutput> FindImagesByFilterAndCategory(string keywords, long categoryId)
        {
            
            keywords = keywords.Trim();

            Category category = CategoryDao.Find(categoryId);

            IList<Image> images = ImageDao.FindByFilterAndCategory(keywords, categoryId);

            IList<ImageOutput> imageOutputs = new List<ImageOutput>();
            foreach (Image image in images)
            {
                User user = UserDao.Find(image.User.usrId);
                imageOutputs.Add(new ImageOutput(
                    image.imagePath,
                    image.title,
                    user.usrId,
                    category.categoryId,
                    category.name, 
                    image.likes
                ));
            }

            return imageOutputs;
        }

        public IList<ImageOutput> FindImagesByUser(long userId)
        {
            User user = UserDao.Find(userId);
            IList<Image> images = ImageDao.FindByUser(userId);

            IList<ImageOutput> imageOutputs = new List<ImageOutput>();
            foreach (Image image in images)
            {
                Category category = CategoryDao.Find(image.Category.categoryId);
                imageOutputs.Add(new ImageOutput(
                    image.imagePath,
                    image.title,
                    user.usrId,
                    category.categoryId,
                    category.name, 
                    image.likes
                ));
            }

            return imageOutputs;
        }

        [Transactional]
        public long UploadImage(long userId, long categoryId, string title, string description, string aperture, string exposure, string balance, string imageFile)
        {
            User user = UserDao.Find(userId);

            Category category = CategoryDao.Find(categoryId);

            if (!PropertyValidator.IsValidImageAperture(aperture))
            {
                throw new IncorrectApertureFormatException(aperture);
            }
            if (!PropertyValidator.IsValidImageExposure(exposure))
            {
                throw new IncorrectExposureFormatException(exposure);
            }
            if (!PropertyValidator.IsValidImageBalance(balance))
            {
                throw new IncorrectBalanceFormatException(balance);
            }
            if (ImageDao.FindByTitle(title) != null) {
                throw new ObjectAlreadyExistsException(title);
            }


            Image image = new Image() 
            { 
                User = user,
                Category = category,
                likes = 0,
                title = title,
                description = description,
                aperture = aperture,
                exposure = exposure,
                balance = balance,
                imagePath = imageFile
            };
            ImageDao.Create(image); 

            return image.imageId;
        }


        [Transactional]
        public void DeleteImage(long imageId)
        {
            Image image = ImageDao.Find(imageId);
            foreach (Comment comment in image.Comments)
            {
                CommentDao.Remove(comment.commentId);
            }
            ImageDao.Remove(imageId);

        }

        public IList<CategoryOutput> FindCategories()
        {

            IList<CategoryOutput> categoriesOutput = new List<CategoryOutput>();
            IList<Category> categoryList = CategoryDao.GetAllElements();
            if (categoryList.Count != 0) { 
                foreach (Category category in categoryList)
                {
                    categoriesOutput.Add(new CategoryOutput(
                        category.categoryId,
                        category.name
                    ));
                }
            }

            return categoriesOutput;
        }

    }
}
