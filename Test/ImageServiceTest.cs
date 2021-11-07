using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
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

        private const string loginName = "loginNameTest";
        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";


        private static IKernel kernel;
        private static IImageService imageService;
        private static IImageDao imageDao;

        private TransactionScope transaction;


        [TestMethod]
        public void TestUploadImage()
        {
            using (var scope = new TransactionScope())
            {
                // Register user and find profile
                var imageId = imageService.UploadImage();

                var imageProfile = imageDao.Find(imageId);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }
    }
}
