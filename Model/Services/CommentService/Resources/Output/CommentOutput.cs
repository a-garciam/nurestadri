using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources.Output
{
    [Serializable]
    public class CommentOutput
    {
        public CommentOutput(string userName, long userId, DateTime creationDate, string commentText)
        {
            UserName = userName;
            UserId = userId;
            CreationDate = creationDate;
            CommentText = commentText;
        }

        public string UserName { get; private set; }
        public long UserId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string CommentText { get; private set; }
    }
}