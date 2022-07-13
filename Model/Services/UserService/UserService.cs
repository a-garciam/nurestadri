using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserDao UserDao { private get; set; }

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangePassword(long userId, string oldClearPassword,
            string newClearPassword)
        {
            if (!UserDao.Exists(userId))
            {
                throw new InstanceNotFoundException(userId, "user");
            }
            User user = UserDao.Find(userId);
            String storedPassword = user.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(user.loginName);
            }

            user.enPassword =
            PasswordEncrypter.Crypt(newClearPassword);

            UserDao.Update(user);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserDetails FindUserDetails(long userId)
        {
            User user = UserDao.Find(userId);

            UserDetails userDetails =
                new UserDetails(user.loginName, user.firstName, user.lastName,
                user.email, user.language, user.country);

            return userDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            User user = UserDao.FindByLoginName(loginName);

            String storedPassword = user.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(user.usrId, user.firstName,
                storedPassword, user.language, user.country);
        }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(string loginName, string clearPassword,
            UserDetails userDetails)
        {
            try
            {
                UserDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(User).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                User user = new User();

                user.loginName = loginName;
                user.enPassword = encryptedPassword;
                user.firstName = userDetails.FirstName;
                user.lastName = userDetails.LastName;
                user.email = userDetails.Email;
                user.language = userDetails.Language;
                user.country = userDetails.Country;

                UserDao.Create(user);

                return user.usrId;
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserDetails(long userId,
            UserDetails userDetails)
        {
            User user =
                UserDao.Find(userId);

            user.firstName = userDetails.FirstName;
            user.lastName = userDetails.LastName;
            user.email = userDetails.Email;
            user.language = userDetails.Language;
            user.country = userDetails.Country;
            UserDao.Update(user);
        }

        public bool UserExists(string loginName)
        {
            try
            {
                User user = UserDao.FindByLoginName(loginName);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }

            return true;
        }

        public void ViewUserProfile(string loginName)
        {
            User user = UserDao.FindByLoginName(loginName);
            if (user.Images.Count > 0)
                Console.WriteLine("Number of posts: {user.Images.Count}");
            if (user.ImageLikes.Count > 0)
                foreach (Image image in user.ImageLikes)
                {
                    User likeUser = UserDao.Find(image.User.usrId);
                    string name = likeUser.loginName;
                    Console.WriteLine("You gave a like to: {name}", name);
                }
            Console.WriteLine("user: {user.loginName}");
            Console.WriteLine("Name: {user.firstName} {user.lastName}");
        }
    }
}