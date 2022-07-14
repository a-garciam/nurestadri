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
        public UserFollows(long userId, string userName, IList<KeyValuePair<string, long>> followList, int numberFollows)
        {
            UserId = userId;
            UserName = userName;
            NumberFollows = numberFollows;
            FollowList = followList;
        }

        public long UserId { get; private set; }
        public string UserName { get; private set; }
        public int NumberFollows { get; private set; }
        public IList<KeyValuePair<string, long>> FollowList { get; private set; }
    }
}