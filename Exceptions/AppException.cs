using System;

namespace MMT.CustomerOrder.Exceptions
{
    public class AppException:Exception
    {
        public AppException()
        {

        }
        public AppException(string message):base(message)
        {

        }
    }
    public class UrlMissingException : AppException
    {
        public UrlMissingException():base("Base url is not configured")
        {

        }
    }
}
