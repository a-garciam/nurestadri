using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions
{
    [Serializable]
    public class EmptyCommentException : Exception
    {
        public EmptyCommentException(string message) : base(message)
        {
        }
    }
}