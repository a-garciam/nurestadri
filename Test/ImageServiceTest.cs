using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ImageServiceTest
    {
        private const int imageId = 1;
        private const string title = "Imagen 1";
        private const string description = "Descripcion";
        private const string aperture = "f/5.0";
        private const string balance = "4000";
        private const string exposure = "1/200";
        private const byte[] imageData = null;

        private Category category = new Category()
        {
            name = "Paisajes"
        };

        private User user1 = new User()
        {
            loginName = "user1",
            enPassword = "password",
            firstName = "name",
            lastName = "lastName",
            email = "user@udc.es"
        };

        private Image image1 = new Image()
        {
            title = title,
            description = description,
            aperture = aperture,
            balance = balance,
            exposure = exposure,
            imageData = imageData
        };


        private static IKernel kernel;
        private static IImageService imageService;
        private static IImageDao imageDao;
        private static IUserDao userDao;
        private static ICategoryDao categoryDao;

        private TransactionScope transactionScope;

       

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }



        [TestMethod]
        public void TestUploadImage()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);

                long imageId = imageService.UploadImage(user1.usrId,category.categoryId,title,description,aperture,exposure,balance,imageData);

                Image image = imageDao.Find(imageId);

                Assert.AreEqual(imageId, image.imageId);
                Assert.AreEqual(0, image.likes);
                Assert.AreEqual(user1.usrId, image.User.usrId);
                Assert.AreEqual(category.categoryId, image.Category.categoryId);
                Assert.AreEqual(title, image.title);
                Assert.AreEqual(description, image.description);
                Assert.AreEqual(aperture, image.aperture);
                Assert.AreEqual(balance, image.balance);
                Assert.AreEqual(exposure, image.exposure);
                Assert.AreEqual(imageData, image.imageData);

            }
        }

        [TestMethod]
        public void TestFilterImage()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(1);
                image1.Category = categoryDao.Find(1);
                imageDao.Create(image1);

                IList<ImageOutput> images = imageService.FindImagesByFilter("imagen");

                Assert.AreEqual(1, images.Count());
            }
        }

        [TestMethod]
        public void TestDeleteImage()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(1);
                image1.Category = categoryDao.Find(1);
                imageDao.Create(image1);
                Image image = imageDao.Find(imageId);


                Assert.AreEqual(imageId, image.imageId);
                Assert.AreEqual(0, image.likes);
                Assert.AreEqual(title, image.title);
                Assert.AreEqual(description, image.description);
                Assert.AreEqual(aperture, image.aperture);
                Assert.AreEqual(balance, image.balance);
                Assert.AreEqual(exposure, image.exposure);
                Assert.AreEqual(imageData, image.imageData);

                imageService.DeleteImage(image.imageId);
                //Probar comentarios y likes
                Assert.IsNull(imageDao.Find(image.imageId));

            }
        }
    }
}
