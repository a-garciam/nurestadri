using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// This is a test class for IUserServiceTest and is intended to contain all IUserServiceTest
    /// Unit Tests
    /// </summary>
    [TestClass]
    public class IUserServiceTest
    {
        // Variables used in several tests are initialized here
        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;
        private static IKernel kernel;
        private static IUserService userService;
        private static IUserDao UserDao;

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

        private TransactionScope transaction;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestInitialize()]
        public void testInit()
        {
            transaction = new TransactionScope();
        }

        [TestCleanup()]
        public void testClean()
        {
            transaction.Dispose();
        }

        /// <summary>
        /// A test for RegisterUser
        /// </summary>
        [TestMethod]
        public void RegisterUserTest()
        {
            // Register user and find profile
            var userId =
                userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, lastName, email, language, country));

            var User = UserDao.Find(userId);

            // Check data
            Assert.AreEqual(userId, User.usrId);
            Assert.AreEqual(loginName, User.loginName);
            Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), User.enPassword);
            Assert.AreEqual(firstName, User.firstName);
            Assert.AreEqual(lastName, User.lastName);
            Assert.AreEqual(email, User.email);
            Assert.AreEqual(language, User.language);
            Assert.AreEqual(country, User.country);

            // transaction.Complete() is not called, so Rollback is executed.
        }

        /// <summary>
        /// A test for registering a user that already exists in the database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))] //DuplicateInstanceException
        public void RegisterDuplicatedUserTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, lastName, email, language, country));

                // Register the same user
                userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, lastName, email, language, country));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod]
        public void LoginClearPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, lastName, email, language, country));

                var expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with clear password
                var actual =
                    userService.Login(loginName,
                        clearPassword, false);

                // Check data
                Assert.AreEqual(expected, actual);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod]
        public void LoginEncryptedPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, lastName, email, language, country));

                var expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with encrypted password
                var obtained =
                    userService.Login(loginName,
                        PasswordEncrypter.Crypt(clearPassword), true);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, lastName, email, language, country));

                // Login with incorrect (clear) password
                var actual =
                    userService.Login(loginName, clearPassword + "X", false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))] //InstanceNotFoundException
        public void LoginNonExistingUserTest()
        {
            // Login for a user that has not been registered
            var actual =
                userService.Login(loginName, clearPassword, false);
        }

        /// <summary>
        /// A test for FindUserDetails
        /// </summary>
        [TestMethod]
        public void FindUserDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                var expected =
                    new UserDetails(loginName, firstName, lastName, email, language, country);

                var userId =
                    userService.RegisterUser(loginName, clearPassword, expected);

                var obtained =
                    userService.FindUserDetails(userId);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))] //InstanceNotFoundException
        public void FindUserDetailsForNonExistingUserTest()
        {
            userService.FindUserDetails(NON_EXISTENT_USER_ID);
        }

        /// <summary>
        /// A test for UpdateUserDetails
        /// </summary>
        [TestMethod]
        public void UpdateUserDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                UserDetails usDet = new UserDetails(loginName, firstName, lastName, email, language, country);
                // Register user and update profile details
                var userId = userService.RegisterUser(loginName, clearPassword, usDet);

                var expected =
                    new UserDetails(loginName, firstName + "X", lastName + "X", email + "X",
"XX", "XX");

                userService.UpdateUserDetails(userId, expected);

                var obtained =
                    userService.FindUserDetails(userId);

                // Check changes
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for UpdateUserDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))] //InstanceNotFoundException
        public void UpdateUserDetailsForNonExistingUserTest()
        {
            using (var scope = new TransactionScope())
            {
                userService.UpdateUserDetails(NON_EXISTENT_USER_ID,
                    new UserDetails(loginName, firstName, lastName, email, language, country));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for ChangePassword
        /// </summary>
        [TestMethod]
        public void ChangePasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, lastName, email, language, country));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword, newClearPassword);

                // Try to login with the new password. If the login is correct, then the password
                // was successfully changed.
                userService.Login(loginName, newClearPassword, false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for ChangePassword entering a wrong old password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserDetails(loginName, firstName, lastName, email, language, country));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for ChangePassword when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))] //InstanceNotFoundException
        public void ChangePasswordForNonExistingUserTest()
        {
            userService.ChangePassword(NON_EXISTENT_USER_ID,
                clearPassword, clearPassword + "X");
        }

        [TestMethod]
        public void FollowUserTest()
        {
            UserDao.Create(user1);
            UserDao.Create(user2);

            userService.FollowUser(user1.usrId, user2.usrId);
            Assert.IsTrue(user2.Followers.Contains(user1));
            Assert.IsTrue(user1.Followed.Contains(user2));

            userService.FollowUser(user2.usrId, user1.usrId);
            Assert.IsTrue(user2.Followed.Contains(user1));
            Assert.IsTrue(user1.Followers.Contains(user2));
        }

        [TestMethod]
        public void UnfollowUserTest()
        {
            UserDao.Create(user1);
            UserDao.Create(user2);

            userService.FollowUser(user1.usrId, user2.usrId);
            userService.FollowUser(user1.usrId, user2.usrId);
            Assert.IsFalse(user2.Followed.Contains(user1));
            Assert.IsFalse(user1.Followers.Contains(user2));
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            UserDao = kernel.Get<IUserDao>();
            userService = kernel.Get<IUserService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        #endregion Additional test attributes
    }
}