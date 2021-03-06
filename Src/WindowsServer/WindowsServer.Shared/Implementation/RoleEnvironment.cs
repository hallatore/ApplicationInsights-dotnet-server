﻿namespace Microsoft.ApplicationInsights.WindowsServer.Implementation
{
    using System;

    /// <summary>
    /// Provides information about the configuration, endpoints, and status of running role instances. 
    /// </summary>
    internal class RoleEnvironment : RuntimeBindingObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleEnvironment"/> class.
        /// </summary>
        public RoleEnvironment()
            : base(TypeHelpers.GetLoadedType("Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment", "Microsoft.WindowsAzure.ServiceRuntime"))
        {
        }

        /// <summary>
        /// Gets a value indicating whether the role instance is running in the Windows Azure environment. 
        /// </summary>
        public bool IsAvailable
        {
            get
            {
                if (this.TargetType == null)
                {
                    return false;
                }

                try
                {
                    return (bool)this.GetProperty("IsAvailable");
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the unique identifier of the deployment in which the role instance is running. 
        /// </summary>
        public string DeploymentId
        {
            get
            {
                if (this.TargetType == null)
                {
                    throw new NotSupportedException();
                }

                return (string)this.GetProperty("DeploymentId");
            }
        }

        /// <summary>
        /// Gets a RoleInstance object that represents the role instance in which the code is currently running. 
        /// </summary>
        public RoleInstance CurrentRoleInstance
        {
            get
            {
                if (this.TargetType == null)
                {
                    throw new NotSupportedException();
                }

                object currentRoleInstance = this.GetProperty("CurrentRoleInstance");
                return new RoleInstance(currentRoleInstance);
            }
        }

        /// <summary>
        /// Gets the target object instance.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="activationArgs">The activation arguments.</param>
        /// <returns>
        /// The activated instance is one is required.
        /// </returns>
        protected override object GetTargetObjectInstance(Type targetType, object[] activationArgs)
        {
            // RoleEnvironment is a "static" object in the Azure Runtime. As such, no activation is required.
            return null;
        }
    }
}
