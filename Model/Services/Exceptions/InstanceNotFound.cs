using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Log;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    [Serializable]
    public class InstanceNotFound : Exception
    {
        public String instanceType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="InstanceNotFoundException"/> class.
        /// </summary>
        /// <param name="instanceType"><c>type of the instance</c> that causes the error.</param>
        public InstanceNotFound(String instanceType)
            : base("The selected instance was not found => type = " + instanceType)
        {
            this.instanceType = instanceType;
        }


    }
}