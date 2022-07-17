using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Data.Entity.Validation;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources.Output;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ICommentServiceTest
    {
        private const string txt = "Gran fotografía crack!! :)";

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

        private const string title = "Imagen 1";
        private const string description = "Descripcion";
        private const string aperture = "f/5.0";
        private const string balance = "4000";
        private const string exposure = "1/90";
        private const string imagePath = "image.png";

        private Image image1 = new Image()
        {
            title = title,
            description = description,
            aperture = aperture,
            balance = balance,
            likes = 0,
            exposure = exposure,
            imagePath = imagePath
        };

        private Category category = new Category()
        {
            name = "Paisajes"
        };

        private Comment comment1 = new Comment()
        {
            text = "Nuevo comentario",
            creationDate = System.DateTime.Now
        };

        private static IKernel kernel;
        private static IImageService imageService;
        private static IUserService userService;
        private static IImageDao imageDao;
        private static IUserDao userDao;
        private static ICommentDao commentDao;
        private static ICommentService commentService;
        private static ICategoryDao categoryDao;

        private TransactionScope transactionScope;

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userDao = kernel.Get<IUserDao>();
            userService = kernel.Get<IUserService>();
            imageDao = kernel.Get<IImageDao>();
            imageService = kernel.Get<IImageService>();
            commentDao = kernel.Get<ICommentDao>();
            commentService = kernel.Get<ICommentService>();
            categoryDao = kernel.Get<ICategoryDao>();
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
        public void TestLikeImage()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);

                imageDao.Create(image1);

                Assert.IsTrue(image1.likes == 0);
                int likes = commentService.LikeImage(image1.imageId, user1.usrId);
                Assert.AreEqual(1, likes);
                likes = commentService.LikeImage(image1.imageId, user1.usrId);
                Assert.AreEqual(0, likes);

                //likes = commentService.LikeImage(image1.imageId, user1.usrId);
                //Assert.AreEqual(1, likes);
            }
        }

        [TestMethod]
        public void TestFindCommentsByImage()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);

                imageDao.Create(image1);

                comment1.Image = image1;
                comment1.User = image1.User;
                commentDao.Create(comment1);

                CommentBlock comments = commentService.FindCommentsByImage(image1.imageId, 0, 10);

                CommentOutput comment = comments.Comments.FirstOrDefault();
                Assert.AreEqual(comment1.text, comment.CommentText);
                Assert.AreEqual(comment1.creationDate, comment.CreationDate);
                Assert.AreEqual(comment1.usrId, comment.UserId);
            }
        }

        [TestMethod]
        public void TestCommentImage()
        {
            using (var scope = new TransactionScope())
            {
                categoryDao.Create(category);
                userDao.Create(user1);
                image1.User = userDao.Find(user1.usrId);
                image1.Category = categoryDao.Find(category.categoryId);

                imageDao.Create(image1);

                long commentId = commentService.CommentImage(user1.usrId, image1.imageId, "Nuevo comentario hola hola");
                Comment comment = commentDao.Find(commentId);
                Assert.AreEqual("Nuevo comentario hola hola", comment.text);
                Assert.AreEqual(user1.usrId, comment.usrId);

                commentId = commentService.CommentImage(user1.usrId, image1.imageId, "Nuevo comentario 2 hola hola");
                comment = commentDao.Find(commentId);
                Assert.AreEqual("Nuevo comentario 2 hola hola", comment.text);
                Assert.AreEqual(user1.usrId, comment.usrId);
            }
        }
    }
}