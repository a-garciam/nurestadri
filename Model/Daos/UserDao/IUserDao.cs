using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Daos
{
    public interface IUserDao : IGenericDao<User, Int64>
    {
        /// <summary>
        /// Finds a User by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        User FindByLoginName(String loginName);
    }
}
