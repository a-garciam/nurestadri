using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions
{
    [Serializable]
    public class IncorrectApertureFormatException : Exception
    {
        public IncorrectApertureFormatException(String balance)
            : base("Aperture value must follow the format: f/N.N") { }
    }
}
