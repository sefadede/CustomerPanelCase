using System;
using System.Collections.Generic;
using System.Text;

namespace RannaCore.Exceptions
{
    public class ServiceNoPermissionException : Exception
    {
        public ServiceNoPermissionException()
            : base("Yetkiniz yoktur.")
        {

        }

    }
}
