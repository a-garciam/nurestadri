using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao
{
    /// <summary>
    /// Specific Operations for User
    /// </summary>
    public class UserDaoEntityFramework :
        GenericDaoEntityFramework<User, Int64>, IUserDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        /// <summary>
        /// Finds a User by his loginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public User FindByLoginName(string loginName)
        {
            User user = null;

            DbSet<User> users = Context.Set<User>();

            var result =
                (from u in users
                 where u.loginName == loginName
                 select u);

            user = result.FirstOrDefault();

            if (user == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(User).FullName);

            return user;
        }

        public IList<User> FindUserFollowers(long userId, int startIndex, int count)
        {
            DbSet<User> users = Context.Set<User>();

            var result =
                (from u in users
                 where u.usrId == userId
                 select new { Followers = u.Followers.OrderBy(f => f.loginName).Skip(startIndex).Take(count).ToList() });

            IList<User> user = result.FirstOrDefault().Followers;

            return user;
        }

        public IList<User> FindUserFollowed(long userId, int startIndex, int count)
        {
            DbSet<User> users = Context.Set<User>();

            var result =
                (from u in users
                 where u.usrId == userId
                 select new { Followed = u.Followed.OrderBy(f => f.loginName).Skip(startIndex).Take(count).ToList() });

            IList<User> user = result.FirstOrDefault().Followed;

            return user;
        }
    }
}