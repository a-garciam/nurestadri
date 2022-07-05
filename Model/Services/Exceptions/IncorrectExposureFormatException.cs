using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions
{
    [Serializable]
    public class IncorrectExposureFormatException : Exception
    {
        public IncorrectExposureFormatException(String balance)
            : base("Exposure value must follow the format: N/N ") { }
    }
}
