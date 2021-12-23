using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions
{
    [Serializable]
    public class IncorrectBalanceFormatException : Exception
    {
        public IncorrectBalanceFormatException(String balance)
            : base("Balance value must be between 0 and 20000") { }
    }
}
