using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions
{
    [Serializable]
    public class CouldNotConvertImageException : Exception
    {
        public CouldNotConvertImageException(String imageFile)
            : base("The provided image file could not be converted: " + imageFile)
        {
        }
    }
}
