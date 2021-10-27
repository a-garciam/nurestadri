using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

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
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword =
            PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);
        }
    }
}