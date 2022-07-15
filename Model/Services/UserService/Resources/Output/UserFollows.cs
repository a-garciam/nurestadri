using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Resources.Output
{
    [Serializable]
    public class UserFollows
    {
        public UserFollows(long userId, string userName, IList<KeyValuePair<string, long>> followList, bool existMore)
        {
            UserId = userId;
            UserName = userName;
            FollowList = followList;
            ExistMore = existMore;
        }

        public long UserId { get; private set; }
        public string UserName { get; private set; }
        public bool ExistMore { get; private set; }
        public IList<KeyValuePair<string, long>> FollowList { get; private set; }
    }
}