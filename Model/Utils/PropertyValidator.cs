using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Utils
{
    public static class PropertyValidator
    {
        public static bool IsValidImageAperture(string aperture)
        {
            if (string.IsNullOrWhiteSpace(aperture) && aperture.Length != 8)
            {
                return false;
            }

            bool isAperture = aperture.StartsWith("f/");
            return isAperture && float.Parse(aperture.Substring(2)) > 0;
        }
    }
}
