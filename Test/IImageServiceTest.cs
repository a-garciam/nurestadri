using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IImageServiceTest
    {
        private const string imageDirectory = "images/";

        private const string title = "Imagen 1";
        private const string description = "Descripcion";
        private const string aperture = "f/5.0";
        private const string balance = "4000";
        private const string exposure = "1/90";
        private const string imagePath = "image.png";

        private Category category = new Category()
        {
            name = "Paisajes"
        };

        private Category category2 = new Category()
        {
            name = "Animales"
        };

        private User user1 = new User()
        {
            loginName = "user1",
            enPassword = "password",
            firstName = "name",
            lastName = "lastName",
            email = "user@udc.es",
            country = "españa",
            language = "español"
        };

        private User user2 = new User()
        {
            loginName = "user2",
            enPassword = "password",
            firstName = "name",
            lastName = "lastName",
            email = "user2@udc.es",
            country = "españa",
            language = "español"
        };

        private Image image1 = new Image()
        {
            title = title,
            description = description,
            aperture = aperture,
            balance = balance,
            exposure = exposure,
            imagePath = imagePath,
            creationDate = DateTime.Now,
        };

        private Image image2 = new Image()
        {
            title = "Imagen 2",
            description = description,
            aperture = aperture,
            balance = balance,
            exposure = exposure,
            imagePath = "image2.png",
            creationDate = DateTime.Now,
        };

        private static IKernel kernel;
        private static IImageService imageService;
        private static IImageDao imageDao;
        private static IUserDao userDao;
        private static ICategoryDao categoryDao;

        private TransactionScope transactionScope;

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userDao = kernel.Get<IUserDao>();
            imageDao = kernel.Get<IImageDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            imageService = kernel.Get<IImageService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        #endregion Additional test attributes

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

                string iPath = imageDirectory + user1.usrId.ToString() + imagePath;
                long imageId = imageService.UploadImage(user1.usrId, category.categoryId, title, description, aperture, exposure, balance, iPath);

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
                Assert.AreEqual(iPath, image.imagePath);
            }
        }

        [TestMethod]
        public void TestFilterImage()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);

                imageDao.Create(image1);

                ImageBlock images = imageService.FindImagesByFilter("imagen", 0, 2);

                Assert.IsTrue(images.Images.First().Title.ToLower().Contains("imagen"));
            }
        }

        [TestMethod]
        public void TestFilterImageAndCategory()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                categoryDao.Create(category2);
                userDao.Create(user1);
                userDao.Create(user2);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);
                image2.User = userDao.Find(user2.usrId);
                image2.Category = categoryDao.Find(category2.categoryId);

                imageDao.Create(image1);
                imageDao.Create(image2);

                ImageBlock images = imageService.FindImagesByFilterAndCategory("imagen", category.categoryId, 0, 2);

                Assert.AreEqual(1, images.Images.Count());
                Assert.IsTrue(images.Images.First().Title.ToLower().Contains("imagen"));
                Assert.AreEqual("Imagen 1", images.Images[0].Title);
            }
        }

        [TestMethod]
        public void TestFindImageById()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);
                image1.usrId = user1.usrId;
                user1.Images.Add(image1);
                imageDao.Create(image1);

                ImageDetailsOutput image = imageService.FindImageById(image1.imageId);

                Assert.AreEqual(image1.title, image.Title);
                Assert.AreEqual(user1.usrId, image.UserId);
                Assert.AreEqual(category.categoryId, image.CategoryId);
                Assert.AreEqual(category.name, image.CategoryName);
                Assert.AreEqual(image1.imagePath, image.ImagePath);
            }
        }

        [TestMethod]
        public void TestFindImageByUser()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);

                imageDao.Create(image1);

                ImageBlock images = imageService.FindImagesByUser(user1.usrId, 0, 2);
                ImageOutput image = images.Images.First();

                Assert.AreEqual(image1.title, image.Title);
                Assert.AreEqual(user1.usrId, image.UserId);
                Assert.AreEqual(category.categoryId, image.CategoryId);
                Assert.AreEqual(category.name, image.CategoryName);
                Assert.AreEqual(image1.imagePath, image.ImagePath);
            }
        }

        [TestMethod]
        public void TestFindAllImages()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                categoryDao.Create(category2);
                userDao.Create(user1);
                userDao.Create(user2);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);
                image2.User = userDao.Find(user2.usrId);
                image2.Category = categoryDao.Find(category2.categoryId);

                imageDao.Create(image1);
                imageDao.Create(image2);

                ImageBlock images = imageService.FindAllImages(0, 10);
                Assert.AreEqual(2, images.Images.Count());
                Assert.AreEqual("Imagen 2", images.Images[0].Title); //Orden de más reciente a más antiguo, la primera imagen deberia se la numero 2
                Assert.AreEqual("Imagen 1", images.Images[1].Title);
            }
        }

        [TestMethod]
        public void TestDeleteImage()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);
                imageDao.Create(image1);
                Image image = imageDao.Find(image1.imageId);

                Assert.AreEqual(0, image.likes);
                Assert.AreEqual(title, image.title);
                Assert.AreEqual(description, image.description);
                Assert.AreEqual(aperture, image.aperture);
                Assert.AreEqual(balance, image.balance);
                Assert.AreEqual(exposure, image.exposure);

                Assert.IsTrue(imageDao.GetAllElements().Contains(image1));
                imageService.DeleteImage(image.imageId, user1.usrId);
                Assert.IsFalse(imageDao.GetAllElements().Contains(image1));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OperationNotAllowedException))]
        public void TestDeleteImageUnauthorizedUser()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);
                imageDao.Create(image1);
                Image image = imageDao.Find(image1.imageId);

                Assert.IsTrue(imageDao.GetAllElements().Contains(image1));
                imageService.DeleteImage(image.imageId, user2.usrId);
            }
        }

        [TestMethod]
        public void TestFindCategories()
        {
            categoryDao.Create(category);
            Assert.AreEqual(category.name, imageService.FindCategories()[0].Name);
            Assert.AreEqual(category.categoryId, imageService.FindCategories()[0].CategoryId);
        }
    }
}