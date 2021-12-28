﻿using Es.Udc.DotNet.PracticaMaD.Model;
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

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class CommentServiceTest
    {

        private const string txt = "Gran fotografía crack!! :)";

        public User user1 = new User();

        private Image image1 = new Image();

        private static IKernel kernel;
        private static IImageService imageService;
        private static IUserService userService;
        private static IImageDao imageDao;
        private static IUserDao userDao;
        private static ICommentDao commentDao;
        private static ICommentService commentService;

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
        public void TestCommentImage()
        {
            using (var scope = new TransactionScope())
            {

                user1.loginName = "user1";
                user1.enPassword = "password";
                user1.firstName = "name";
                user1.lastName = "lastName";
                user1.email = "user@udc.es";

                image1.title = "Imagen 1";
                image1.description = "Descripcion";
                image1.aperture = "f/5.0";
                image1.balance = "4000";
                image1.exposure = "1/200";
                image1.imageData = null;
                imageDao.Create(image1);
                userDao.Create(user1);

                int commentId = (int) commentService.CommentImage(user1.usrId, image1.imageId, txt);

                Comment saved = commentDao.Find(commentId);

                Assert.AreEqual(commentId, saved.commentId);
                Assert.AreEqual(saved.text, txt);
                Assert.AreEqual(saved.Image.imageId, image1.imageId);
                Assert.AreEqual(saved.User.usrId, user1.usrId);

            }
        }

        //[TestMethod]
        //public void TestDeleteComment()
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        userDao.Create(user1);
        //        imageDao.Create(image1);

        //        long commentId = commentService.CommentImage(user1, image1, txt);

        //        Comment saved = commentDao.Find(commentId);

        //        Assert.AreEqual(commentId, saved.commentId);
        //        Assert.AreEqual(saved.text, txt);

        //        long deletedId = commentService.DeleteComment(commentId);

        //        Assert.AreEqual(saved.commentId, deletedId);
        //        Assert.isNull(commentDao.Find(commentId));

        //    }
        //}

        //[TestMethod]
        //public void TestEditComment()
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        userDao.Create(user1);
        //        imageDao.Create(image1);

        //        long commentId = commentService.CommentImage(user1, image1, txt);

        //        Comment saved = commentDao.Find(commentId);

        //        Assert.AreEqual(commentId, saved.commentId);
        //        Assert.AreEqual(saved.text, txt);
        //        Assert.AreEqual(saved.Image.imageId, image1.imageId);
        //        Assert.AreEqual(saved.User.usrId, user1.usrId);

        //    }
        //}

        //[TestMethod]
        //public void TestGetComments()
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        userDao.Create(user1);
        //        imageDao.Create(image1);

        //        Image image = imageService.Find(image1.imageId);

        //        IList<Comment> comments = commentService.DisplayComments(image);
        //        Assert.AreEqual(0, comments.Count());

        //        commentService.CommentImage(user1, image1, txt);
        //        IList<Comment> comments = commentService.DisplayComments(image);
        //        Assert.AreEqual(1, comments.Count());
        //    }
        //}

        //[TestMethod]
        //public void TestLikeImage()
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        userDao.Create(user1);
        //        imageDao.Create(image1);

        //        Image image = imageService.Find(image1.imageId);
        //        User user = userService.Find(user1.usrId);

        //        // Verify that image has no likes 
        //        Assert.AreEqual(0, image.likes);

        //        // Check if the like is added correctly
        //        int likeId = commentService.LikeImage(image, user);
        //        Assert.AreEqual(1, image.likes);
        //        Assert.AreEqual(likeId, likeDao.Find(likeId).likeId);

        //        //Check if the like is removed when it was already created
        //        commentService.LikeImage(image, user);
        //        Assert.AreEqual(0, image.likes);
        //        Assert.IsNull(likeDao.Find(likeId));
        //    }
        //}


    }
}
