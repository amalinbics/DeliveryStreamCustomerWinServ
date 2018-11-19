using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliveryStreamCustomerWinServ
{
    /// <summary>
    /// ApplicationConstants class
    /// </summary>
    public static class ApplicationConstants
    {
        public struct Connection
        {
            public const String ConnectionString = "DeliveryStreamCustomer";
        }

        public struct Errors
        {
            public const String FunctionError = "Error occured. Function : in {0}, Class : {1}, Error Message : {2} Stack Trace : {3}";
            public const String ConnectionString = "Could not find {0} connection string in config file";            

        }

        public struct Logging
        {
            public const String Log = "DeliveryStream";
            public const String Source = "CustomerWinServ";
        }
    }
}
