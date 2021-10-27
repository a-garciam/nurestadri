using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Common;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos
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

        #region IUserDao Members. Specific Operations

        /// <summary>
        /// Finds a User by his loginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public UserProfile FindByLoginName(string loginName)
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
}
