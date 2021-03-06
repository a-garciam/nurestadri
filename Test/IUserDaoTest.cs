using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Ninject;
using System;
using System.Transactions;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IUserDaoProfileTest
    {
        private static IKernel kernel;
        private static IUserDao UserDao;
        private static User user;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";

        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        private TransactionScope transaction;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            UserDao = kernel.Get<IUserDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();

            user = new User();
            user.loginName = loginName;
            user.enPassword = PasswordEncrypter.Crypt(clearPassword);
            user.firstName = firstName;
            user.lastName = lastName;
            user.email = email;
            user.language = language;
            user.country = country;

            UserDao.Create(user);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion Additional test attributes

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [TestMethod()]
        public void DAO_FindByLoginNameTest()
        {
            try
            {
                User actual = UserDao.FindByLoginName(user.loginName);

                Assert.AreEqual(user, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
