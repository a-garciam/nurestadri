﻿using System;
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
    public class InstanceNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="InstanceNotFoundException"/> class.
        /// </summary>
        /// <param name="instanceType"><c>type of the instance</c> that causes the error.</param>
        public InstanceNotFoundException(String instanceType)
            : base("The selected instance was not found => type = " + instanceType)
        {
            this.instanceType = instanceType;
        }

        /// <summary>
        /// Stores the User login name of the exception
        /// </summary>
        /// <value>The name of the login.</value>
        public String instanceType { get; private set; }

        #region Test Code Region. Uncomment for testing.

        //public static void Main(String[] args)
        //{

        //    try
        //    {

        //        throw new IncorrectPasswordException("jsmith");

        //    }
        //    catch (Exception e)
        //    {

        //        LogManager.RecordMessage("Message: " + e.Message +
        //            "  Stack Trace: " + e.StackTrace, MessageType.Info);

        //        Console.ReadLine();

        //    }
        //}

        #endregion
    }
}
