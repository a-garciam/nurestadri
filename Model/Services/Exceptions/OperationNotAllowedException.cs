using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions
{
    [Serializable]
    public class OperationNotAllowedException : Exception
    {
        public OperationNotAllowedException()
        {
        }

        public OperationNotAllowedException(string message) : base(message)
        {
        }
    }
}