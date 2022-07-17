using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources
{
    public class CommentBlock
    {
        public CommentBlock(IList<CommentOutput> comments, bool existMore)
        {
            Comments = comments;
            ExistMore = existMore;
        }

        public IList<CommentOutput> Comments { get; private set; }
        public bool ExistMore { get; private set; }
    }
}