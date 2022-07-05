using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Utils
{
    public static class PropertyValidator
    {
        public static int maxBalance = 20000;

        public static bool IsValidImageAperture(string aperture)
        {
            if (string.IsNullOrWhiteSpace(aperture) || aperture.Length <= 2)
            {
                return false;
            }

            bool isAperture = aperture.StartsWith("f/");
            return isAperture && float.Parse(aperture.Substring(2)) > 0;
        }

        public static bool IsValidImageExposure(string exposure)
        {
            if (string.IsNullOrWhiteSpace(exposure) || !exposure.Contains("/"))
            {
                return false;
            }
            string[] cadena = exposure.Split('/');
            try
            {
                int.Parse(cadena[0]);
                int.Parse(cadena[1]);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidImageBalance(string balance)
        {
            if (string.IsNullOrWhiteSpace(balance))
            {
                return false;
            }
            int balanceNum = int.Parse(balance);

            return balanceNum > 0 && balanceNum < maxBalance;
        }
    }
}