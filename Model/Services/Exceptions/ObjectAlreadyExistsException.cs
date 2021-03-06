using System;
using System.Runtime.Serialization;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions
{
    [Serializable]
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException()
        {
        }

        public ObjectAlreadyExistsException(string message) : base("Object already exists exception => " + message)
        {
        }
    }
}