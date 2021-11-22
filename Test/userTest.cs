using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class UserTest
    {

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";

        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        [TestMethod]
        public void EqualsCompareSameUserTest()
        {

            User userA = new User();
            userA.usrId = 1;
            userA.loginName = loginName;
            userA.firstName = firstName;
            userA.lastName = lastName;
            userA.language = language;
            userA.country = country;
            userA.email = email;
            userA.enPassword = PasswordEncrypter.Crypt(clearPassword);

            User userB = new User();
            userB.usrId = 1;
            userB.loginName = loginName;
            userB.firstName = firstName;
            userB.lastName = lastName;
            userB.language = language;
            userB.country = country;
            userB.email = email;
            userB.enPassword = PasswordEncrypter.Crypt(clearPassword);

            Assert.IsTrue(userA.Equals(userB));

        }

        [TestMethod]
        public void EqualsCompareSameInstanceTest()
        {

            User userA = new User();
            userA.usrId = 1;
            userA.loginName = loginName;
            userA.firstName = firstName;
            userA.lastName = lastName;
            userA.language = language;
            userA.country = country;
            userA.email = email;
            userA.enPassword = PasswordEncrypter.Crypt(clearPassword);

            Assert.IsTrue(userA.Equals(userA));

        }

        [TestMethod]
        public void EqualsCompareDifferentUsersTest()
        {

            User userA = new User();
            userA.usrId = 1;
            userA.loginName = loginName;
            userA.firstName = firstName;
            userA.lastName = lastName;
            userA.language = language;
            userA.country = country;
            userA.email = email;
            userA.enPassword = PasswordEncrypter.Crypt(clearPassword);

            User userB = new User();
            userB.usrId = 2;
            userB.loginName = loginName;
            userB.firstName = firstName;
            userB.lastName = lastName;
            userB.language = language;
            userB.country = country;
            userB.email = email;
            userB.enPassword = PasswordEncrypter.Crypt(clearPassword);

            Assert.IsFalse(userA.Equals(userB));

        }

        [TestMethod]
        public void GetHashCodeTest()
        {

            User userA = new User();
            userA.usrId = 1;
            userA.loginName = loginName;
            userA.firstName = firstName;
            userA.lastName = lastName;
            userA.language = language;
            userA.country = country;
            userA.email = email;
            userA.enPassword = PasswordEncrypter.Crypt(clearPassword);

            User userB = new User();
            userB.usrId = 1;
            userB.loginName = loginName;
            userB.firstName = firstName;
            userB.lastName = lastName;
            userB.language = language;
            userB.country = country;
            userB.email = email;
            userB.enPassword = PasswordEncrypter.Crypt(clearPassword);

            Assert.AreEqual(userA.GetHashCode(), userB.GetHashCode());

        }

        [TestMethod]
        public void ToStringTest()
        {
            User userA = new User();
            userA.usrId = 1;
            userA.loginName = loginName;
            userA.firstName = firstName;
            userA.lastName = lastName;
            userA.language = language;
            userA.country = country;
            userA.email = email;
            userA.enPassword = PasswordEncrypter.Crypt(clearPassword);

            String toStringOutput = userA.ToString();

            Assert.IsNotNull(toStringOutput);

            Assert.IsTrue(toStringOutput.Contains(Convert.ToString(userA.usrId)));
            Assert.IsTrue(toStringOutput.Contains(userA.loginName));
            Assert.IsTrue(toStringOutput.Contains(userA.firstName));
            Assert.IsTrue(toStringOutput.Contains(userA.lastName));
            Assert.IsTrue(toStringOutput.Contains(userA.language));
            Assert.IsTrue(toStringOutput.Contains(userA.country));
            Assert.IsTrue(toStringOutput.Contains(userA.email));
            Assert.IsTrue(toStringOutput.Contains(userA.enPassword));

        }
    }
}
