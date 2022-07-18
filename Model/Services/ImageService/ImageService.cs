using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources;
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

        public ImageDetailsOutput FindImageById(long imageId)
        {
            Image image = ImageDao.Find(imageId);
            Category category = CategoryDao.Find(image.Category.categoryId);
            User user = UserDao.Find(image.User.usrId);
            ImageDetailsOutput imageOutput = new ImageDetailsOutput(
                image.imageId,
                image.imagePath,
                image.title,
                image.description,
                user.usrId,
                user.loginName,
                category.categoryId,
                 category.name,
                 likes: image.likes,
                 image.creationDate,
                 image.aperture,
                 image.balance,
                 image.exposure);

            return imageOutput;
        }

        public ImageBlock FindImagesByFilter(string keywords, int startIndex, int count)
        {
            keywords = keywords.Trim();

            IList<Image> images = ImageDao.FindByFilter(keywords, startIndex, count + 1);

            bool existMore = (images.Count == count + 1);

            if (existMore)
                images.RemoveAt(count);

            IList<ImageOutput> imageOutputs = new List<ImageOutput>();
            foreach (Image image in images)
            {
                Category category = CategoryDao.Find(image.Category.categoryId);
                User user = UserDao.Find(image.User.usrId);
                imageOutputs.Add(new ImageOutput(
                    image.imageId,
                    image.imagePath,
                    image.title,
                    user.usrId,
                    user.loginName,
                    category.categoryId,
                     category.name,
                     image.likes,
                     image.creationDate));
            }

            return new ImageBlock(imageOutputs, existMore);
        }

        public ImageBlock FindImagesByFilterAndCategory(string keywords, long categoryId, int startIndex, int count)
        {
            keywords = keywords.Trim();

            Category category = CategoryDao.Find(categoryId);

            IList<Image> images = ImageDao.FindByFilterAndCategory(keywords, categoryId, startIndex, count + 1);

            bool existMore = (images.Count == count + 1);

            if (existMore)
                images.RemoveAt(count);

            IList<ImageOutput> imageOutputs = new List<ImageOutput>();
            foreach (Image image in images)
            {
                User user = UserDao.Find(image.User.usrId);
                imageOutputs.Add(new ImageOutput(
                    image.imageId,
                    image.imagePath,
                    image.title,
                    user.usrId,
                    user.loginName,
                    category.categoryId,
                     category.name,
                     image.likes,
                     image.creationDate));
            }

            return new ImageBlock(imageOutputs, existMore);
        }

        public ImageBlock FindImagesByUser(long userId, int startIndex, int count)
        {
            User user = UserDao.Find(userId);
            IList<Image> images = ImageDao.FindByUser(userId, startIndex, count + 1);

            bool existMore = (images.Count == count + 1);

            if (existMore)
                images.RemoveAt(count);

            IList<ImageOutput> imageList = new List<ImageOutput>();
            foreach (Image image in images)
            {
                Category category = CategoryDao.Find(image.Category.categoryId);
                imageList.Add(new ImageOutput(
                    image.imageId,
                    image.imagePath,
                    image.title,
                    user.usrId,
                    user.loginName,
                    category.categoryId,
                    category.name,
                    image.likes,
                    image.creationDate));
            }

            return new ImageBlock(imageList, existMore);
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
            if (ImageDao.FindByTitle(title) != null)
            {
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
                imagePath = imageFile,
                creationDate = System.DateTime.Now
            };
            ImageDao.Create(image);

            return image.imageId;
        }

        [Transactional]
        public void DeleteImage(long imageId, long userId)
        {
            Image image = ImageDao.Find(imageId);
            if (image.User.usrId != userId)
            {
                throw new OperationNotAllowedException();
            }
            ImageDao.Remove(imageId);
        }

        public IList<CategoryOutput> FindCategories()
        {
            IList<CategoryOutput> categoriesOutput = new List<CategoryOutput>();
            IList<Category> categoryList = CategoryDao.GetAllElements();
            if (categoryList.Count != 0)
            {
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

        public ImageBlock FindAllImages(int startIndex, int count)
        {
            IList<Image> images = ImageDao.FindAll(startIndex, count + 1);

            bool existMore = (images.Count == count + 1);

            if (existMore)
                images.RemoveAt(count);

            IList<ImageOutput> imageOutputs = new List<ImageOutput>();
            foreach (Image image in images)
            {
                Category category = CategoryDao.Find(image.Category.categoryId);
                User user = UserDao.Find(image.User.usrId);
                imageOutputs.Add(new ImageOutput(
                    image.imageId,
                    image.imagePath,
                    image.title,
                    user.usrId,
                    user.loginName,
                    category.categoryId,
                     category.name,
                     image.likes,
                     image.creationDate));
            }

            return new ImageBlock(imageOutputs, existMore);
        }
    }
}